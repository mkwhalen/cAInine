using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CAInine.Core.Interfaces.Providers;
using CAInine.Core.Models.Configuration;
using CAInine.Core.Models.Transfer.PetFinder;
using Microsoft.Extensions.Options;

namespace CAInine.Infrastructure.Data.Providers
{
    /// <summary>
    /// Data provider for communicating the the petfinder.com api
    /// </summary>
    public class PetFinderDataProvider : IPetSearchProvider
    {

        private readonly IOptions<PetFinder> _petFinderSettings;
        private readonly HttpClient _client;
        public PetFinderDataProvider(HttpClient client, IOptions<PetFinder> petFinderSettings)
        {
            _client = client;
            _petFinderSettings = petFinderSettings;
        }

        public async Task<petfinderBreedList> GetAvailableBreedsAsync(string animal)
        {
            return await MakePetFinderRequestAsync<petfinderBreedList>($"{_petFinderSettings.Value.Url}breed.list?key={_petFinderSettings.Value.ApiKey}&animal={animal}");
        }

        public async Task<petfinderPetRecordList> GetPetsAtShelter(string shelterId)
        {
            return await MakePetFinderRequestAsync<petfinderPetRecordList>($"{_petFinderSettings.Value.Url}shelter.getPets?key={_petFinderSettings.Value.ApiKey}&id={shelterId}");
        }

        public async Task<petfinderPetRecordList> GetPetsByBreedAsync(string animal, string breed, string location)
        {
            return await MakePetFinderRequestAsync<petfinderPetRecordList>($"{_petFinderSettings.Value.Url}pet.find?key={_petFinderSettings.Value.ApiKey}&animal={animal}&breed={breed}&location={location}");
        }

        public async Task<petfinderPetRecord> GetRandomPetAsync(string animal)
        {
            return await MakePetFinderRequestAsync<petfinderPetRecord>($"{_petFinderSettings.Value.Url}pet.getRandom?key={_petFinderSettings.Value.ApiKey}&animal={animal}");
        }

        public async Task<petfinderShelterRecordList> GetSheltersByBreed(string animal, string breed, int skip, int take)
        {
            return await MakePetFinderRequestAsync<petfinderShelterRecordList>($"{_petFinderSettings.Value.Url}shelter.listByBreed?key={_petFinderSettings.Value.ApiKey}&animal={animal}&breed={breed}&offset={skip}&count={take}");
        }

        public async Task<petfinderShelterRecordList> GetSheltersByLocation(string location)
        {
            return await MakePetFinderRequestAsync<petfinderShelterRecordList>($"{_petFinderSettings.Value.Url}shelter.find?key={_petFinderSettings.Value.ApiKey}&location={location}");
        }

        private async Task<T> MakePetFinderRequestAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(responseBody))
                {
                    var result = (T)serializer.Deserialize(reader);
                    return result;
                }
            }

            throw new Exception(responseBody);
        }
    }
}
