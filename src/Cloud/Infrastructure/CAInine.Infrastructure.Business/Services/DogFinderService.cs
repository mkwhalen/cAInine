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

    /// <summary>
    /// Business logic for searching for dogs
    /// </summary>
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
                if (record != null)
                {

                    return new SuccessResult<IEnumerable<Animal>>(record);
                }
                return new SuccessResult<IEnumerable<Animal>>(new List<Animal>());
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
                    return new SuccessResult<IEnumerable<Animal>>(record);
                }
                return new SuccessResult<IEnumerable<Animal>>(new List<Animal>());
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
                var breeds = await _petProvider.GetAvailableBreedsAsync("dog");
                if (breeds != null)
                {
                    return new SuccessResult<IEnumerable<string>>(breeds);
                }
                return new SuccessResult<IEnumerable<string>>(new List<string>());
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

                    return new SuccessResult<IEnumerable<Shelter>>(record);
                }
                return new SuccessResult<IEnumerable<Shelter>>(new List<Shelter>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UnexpectedResult<IEnumerable<Shelter>>();
            }

        }

        public async Task<Result<IEnumerable<Shelter>>> GetSheltersByBreed(string breed, int skip, int take)
        {
            try
            {
                var record = await _petProvider.GetSheltersByBreed("dog", breed, skip, take);
                if (record != null)
                {

                    return new SuccessResult<IEnumerable<Shelter>>(record);
                }
                return new SuccessResult<IEnumerable<Shelter>>(new List<Shelter>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UnexpectedResult<IEnumerable<Shelter>>();
            }
        }
    }
}
