using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CAInine.Core.Interfaces.Providers;
using CAInine.Core.Models.Configuration;
using CAInine.Core.Models.Transfer;
using CAInine.Core.Models.Transfer.PetFinder;
using CAInine.Core.Models.Transfer.PetFinder.Enums;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public async Task<List<string>> GetAvailableBreedsAsync(string animal)
        {
            var jobject = await MakePetFinderRequestAsync($"{_petFinderSettings.Value.Url}breed.list?format=json&key={_petFinderSettings.Value.ApiKey}&animal={animal}");
            var breeds = (JArray)jobject["petfinder"]?["breeds"]?["breed"];
            return breeds?.Select(j => j["$t"].Value<string>()).ToList();
        }

        public async Task<List<Animal>> GetPetsAtShelter(string shelterId)
        {
            var jobject = await MakePetFinderRequestAsync($"{_petFinderSettings.Value.Url}shelter.getPets?format=json&key={_petFinderSettings.Value.ApiKey}&id={shelterId}");
            
            var pets = (JArray)jobject["petfinder"]?["pets"]?["pet"];

            return pets.Select(jp => new Animal
            {
                Id = jp["id"]?["$t"]?.Value<string>(),
                ShelterId = jp["shelterId"]?["$t"]?.Value<string>(),
                ShelterPetId = jp["shelterPetId"]?["$t"]?.Value<string>(),
                Name = jp["name"]?["$t"]?.Value<string>(),
                AnimalType = jp["animal"]?["$t"]?.Value<string>(),
                Breeds = GetBreedsFromAnimal(jp),
                IsMixBreed = ConvertStringToBool(jp["mix"]?["$t"]?.Value<string>()),
                Description = jp["description"]?["$t"]?.Value<string>(),
                Gender = ConvertStringToGender(jp["sex"]?["$t"]?.Value<string>()),
                Age = ConvertStringToAge(jp["age"]?["$t"]?.Value<string>()),
                Size = ConvertStringToSize(jp["size"]?["$t"]?.Value<string>())

            }).ToList();
        }

        public async Task<List<Animal>> GetPetsByBreedAsync(string animal, string breed, string location)
        {
            var jobject = await MakePetFinderRequestAsync($"{_petFinderSettings.Value.Url}pet.find?format=json&key={_petFinderSettings.Value.ApiKey}&animal={animal}&breed={breed}&location={location}");

            var pets = (JArray)jobject["petfinder"]?["pets"]?["pet"];
          
            return pets.Select(jp => new Animal
            {
                Id = jp["id"]?["$t"]?.Value<string>(),
                ShelterId = jp["shelterId"]?["$t"]?.Value<string>(),
                ShelterPetId = jp["shelterPetId"]?["$t"]?.Value<string>(),
                Name = jp["name"]?["$t"]?.Value<string>(),
                AnimalType = jp["animal"]?["$t"]?.Value<string>(),
                Breeds = GetBreedsFromAnimal(jp),
                IsMixBreed = ConvertStringToBool(jp["mix"]?["$t"]?.Value<string>()),
                Description = jp["description"]?["$t"]?.Value<string>(),
                Gender = ConvertStringToGender(jp["sex"]?["$t"]?.Value<string>()),
                Age = ConvertStringToAge(jp["age"]?["$t"]?.Value<string>()),
                Size = ConvertStringToSize(jp["size"]?["$t"]?.Value<string>())
            }).ToList();
        }

        public async Task<Animal> GetRandomPetAsync(string animal)
        {
            var jobject = await MakePetFinderRequestAsync($"{_petFinderSettings.Value.Url}pet.getRandom?format=json&key={_petFinderSettings.Value.ApiKey}&animal={animal}");

            
            var pet = jobject["petfinder"]?["pet"];



            return new Animal
            {

                Id = pet["id"]?["$t"]?.Value<string>(),
                ShelterId = pet["shelterId"]?["$t"]?.Value<string>(),
                ShelterPetId = pet["shelterPetId"]?["$t"]?.Value<string>(),
                Name = pet["name"]?["$t"]?.Value<string>(),
                AnimalType = pet["animal"]?["$t"]?.Value<string>(),
                Breeds = GetBreedsFromAnimal(pet),
                IsMixBreed = ConvertStringToBool(pet["mix"]?["$t"]?.Value<string>()),
                Description = pet["description"]?["$t"]?.Value<string>(),
                Gender = ConvertStringToGender(pet["sex"]?["$t"]?.Value<string>()),
                Age = ConvertStringToAge(pet["age"]?["$t"]?.Value<string>()),
                Size = ConvertStringToSize(pet["size"]?["$t"]?.Value<string>())
            };

           
        }

        public async Task<List<Shelter>> GetSheltersByBreed(string animal, string breed, int skip, int take)
        {
            var jobject = await MakePetFinderRequestAsync($"{_petFinderSettings.Value.Url}shelter.listByBreed?format=json&key={_petFinderSettings.Value.ApiKey}&animal={animal}&breed={breed}&offset={skip}&count={take}");
          
            var shelters = (JArray)jobject["petfinder"]?["shelters"]?["shelter"];

            return shelters?.Select(jp => new Shelter
            {
                Id = jp["id"]?["$t"]?.Value<string>(),
                Name = jp["name"]?["$t"]?.Value<string>(),
                Address1 = jp["address1"]?["$t"]?.Value<string>(),
                Address2 = jp["address2"]?["$t"]?.Value<string>(),
                City = jp["city"]?["$t"]?.Value<string>(),
                State = jp["state"]?["$t"]?.Value<string>(),
                ZipCode = jp["zip"]?["$t"]?.Value<string>(),
                Country = jp["country"]?["$t"]?.Value<string>(),
                Latitude = ConvertStringToDecimal(jp["latitude"]?["$t"]?.Value<string>()),
                Longitude = ConvertStringToDecimal(jp["longitude"]?["$t"]?.Value<string>()),
                PhoneNumber = jp["phone"]?["$t"]?.Value<string>(),
                Email = jp["email"]?["$t"]?.Value<string>()
            }).ToList();

       
        }

        public async Task<List<Shelter>> GetSheltersByLocation(string location)
        {
            var jobject = await MakePetFinderRequestAsync($"{_petFinderSettings.Value.Url}shelter.find?format=json&key={_petFinderSettings.Value.ApiKey}&location={location}");

            var shelters = (JArray)jobject["petfinder"]?["shelters"]?["shelter"];

            return shelters.Select(jp => new Shelter
            {
                Id = jp["id"]?["$t"]?.Value<string>(),
                Name = jp["name"]?["$t"]?.Value<string>(),
                Address1 = jp["address1"]?["$t"]?.Value<string>(),
                Address2 = jp["address2"]?["$t"]?.Value<string>(),
                City = jp["city"]?["$t"]?.Value<string>(),
                State = jp["state"]?["$t"]?.Value<string>(),
                ZipCode = jp["zip"]?["$t"]?.Value<string>(),
                Country = jp["country"]?["$t"]?.Value<string>(),
                Latitude = ConvertStringToDecimal(jp["latitude"]?["$t"]?.Value<string>()),
                Longitude = ConvertStringToDecimal(jp["longitude"]?["$t"]?.Value<string>()),
                PhoneNumber = jp["phone"]?["$t"]?.Value<string>(),
                Email = jp["email"]?["$t"]?.Value<string>()
            }).ToList();

            
        }

        private async Task<JObject> MakePetFinderRequestAsync(string url)
        {
            var response = await _client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JObject.Parse(responseBody);
            }

            throw new Exception(responseBody);
        }
        private bool ConvertStringToBool(string yesOrNo)
        {
            if (yesOrNo == "yes")
                return true;

            return false;
        }
        private PetGender ConvertStringToGender(string genderCode)
        {
            if (genderCode == "F")
                return PetGender.Female;

            return PetGender.Male;
        }
        private PetAgeType ConvertStringToAge(string ageCode)
        {
            Enum.TryParse(ageCode, out PetAgeType ageType);

            return ageType;
        }
        private PetSize ConvertStringToSize(string sizeCode)
        {
            switch (sizeCode)
            {
                case "S":
                    return PetSize.Small;
                case "M":
                    return PetSize.Medium;
                case "L":
                    return PetSize.Large;
                case "XL":
                    return PetSize.ExtraLarge;
            }

            return PetSize.Unknown;
        }

        private List<string> GetBreedsFromAnimal(JToken jsonAnimal)
        {
            var multiBreeds = (jsonAnimal["breeds"]?["breed"] as JArray)?
                                 .Select(jjp => jjp["$t"]?.Value<string>()).ToList();
            if(multiBreeds == null)
            {
                var singleBreed = jsonAnimal["breeds"]?["breed"]?["$t"]?.Value<string>();
                multiBreeds = new List<string> { singleBreed };
            }

            return multiBreeds;
        }

        private decimal ConvertStringToDecimal(string latOrLongCode)
        {
            decimal.TryParse(latOrLongCode, out decimal latOrLongDecimal);
            return latOrLongDecimal;
        }
    }
}
