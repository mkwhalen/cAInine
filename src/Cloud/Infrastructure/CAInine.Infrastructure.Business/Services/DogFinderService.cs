using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAInine.Core.Interfaces.Providers;
using CAInine.Core.Interfaces.Services;
using CAInine.Core.Models.Results;
using CAInine.Core.Models.Transfer;
using CAInine.Core.Models.Transfer.PetFinder;
using CAInine.Core.Models.Transfer.PetFinder.Enums;

namespace CAInine.Infrastructure.Business.Services
{
    public class DogFinderService : IPetFinderService
    {
        private readonly IPetSearchProvider _petProvider;
        public DogFinderService(IPetSearchProvider petProvider)
        {
            _petProvider = petProvider;
        }

        public async Task<Result<IEnumerable<Animal>>> GetAnimalsByBreedAsync(string breed, string location)
        {
            try
            {
                var record = await _petProvider.GetPetsByBreedAsync("dog", breed, location);
                if(record != null)
                {
                    var animals = record.pet.Select(p => CreateAnimalFromRecord(p));
                    return new SuccessResult<IEnumerable<Animal>>(animals);
                }
                return new UnexpectedResult<IEnumerable<Animal>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UnexpectedResult<IEnumerable<Animal>>();
            }
        }

        public async Task<Result<IEnumerable<Animal>>> GetAnimalsByShelterAsync(string shelterId)
        {
            try
            {
                var record = await _petProvider.GetPetsAtShelter(shelterId);
                if (record != null)
                {
                    var animals = record.pet.Select(p => CreateAnimalFromRecord(p));
                    return new SuccessResult<IEnumerable<Animal>>(animals);
                }
                return new UnexpectedResult<IEnumerable<Animal>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UnexpectedResult<IEnumerable<Animal>>();
            }   
        }

        public async Task<Result<IEnumerable<string>>> GetBreedsAsync()
        {
            try
            {
                var record = await _petProvider.GetAvailableBreedsAsync("dog");
                if (record != null)
                {
                    var breeds = record.breed.Select(b => b.ToString());
                    return new SuccessResult<IEnumerable<string>>(breeds);
                }
                return new UnexpectedResult<IEnumerable<string>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UnexpectedResult<IEnumerable<string>>();
            }          
        }

        public async Task<Result<IEnumerable<Shelter>>> GetSheltersByLocation(string location)
        {
            try
            {
                var record = await _petProvider.GetSheltersByLocation(location);
                if (record != null)
                {
                    var shelters = record.shelter.Select(s => CreateShelterFromRecord(s));
                    return new SuccessResult<IEnumerable<Shelter>>(shelters);
                }
                return new UnexpectedResult<IEnumerable<Shelter>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UnexpectedResult<IEnumerable<Shelter>>();
            }          
        }

        private Animal CreateAnimalFromRecord(petfinderPetRecord record)
        {
            return new Animal
            {
                Id = record.id,
                ShelterId = record.shelterId,
                ShelterPetId = record.shelterPetId,
                Name = record.name,
                Description = record.name,
                AnimalType = record.animal.ToString(),
                Breeds = record.breeds.breed.Select(b => b.ToString()).ToList(),
                IsMixBreed = record.mix == petfinderPetRecordMix.yes,
                Gender = (PetGender)record.sex,
                Age = (PetAgeType)record.age,
                Size = (PetSize)record.size
            };
        }

        private Shelter CreateShelterFromRecord(petfinderShelterRecord record)
        {
            return new Shelter
            {
                Id = record.id,
                Name = record.name,
                Address1 = record.address1,
                Address2 = record.address2,
                City = record.city,
                State = record.state,
                ZipCode = record.zip,
                Country = record.country,
                Latitude = record.latitude,
                Longitude = record.longitude,
                PhoneNumber = record.phone,
                Email = record.email
            };
        }
    }
}
