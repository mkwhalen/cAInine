using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAInine.Core.Models.Results;
using CAInine.Core.Models.Transfer;

namespace CAInine.Core.Interfaces.Services
{
    public interface IPetFinderService
    {
        Task<Result<IEnumerable<string>>> GetBreedsAsync();
        Task<Result<IEnumerable<Animal>>> GetAnimalsByShelterAsync(string shelterId);
        Task<Result<IEnumerable<Shelter>>> GetSheltersByLocation(string location);
        Task<Result<IEnumerable<Animal>>> GetAnimalsByBreedAsync(string breed, string location);
        Task<Result<IEnumerable<Shelter>>> GetSheltersByBreed(string breed, int skip, int take);
    }
}
