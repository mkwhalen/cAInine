using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAInine.Core.Models.Transfer;
using CAInine.Core.Models.Transfer.PetFinder;

namespace CAInine.Core.Interfaces.Providers
{
    /// <summary>
    /// Pet search provider. Allows for finding breeds, shelters, and pets that are available for adoption
    /// </summary>
    public interface IPetSearchProvider
    {
        /// <summary>
        /// Gets the available breeds async.
        /// </summary>
        /// <returns>The available breeds async.</returns>
        /// <param name="animal">Animal.</param>
        Task<List<string>> GetAvailableBreedsAsync(string animal);

        /// <summary>
        /// Gets the random pet async.
        /// </summary>
        /// <returns>The random pet async.</returns>
        /// <param name="animal">Animal.</param>
        Task<Animal> GetRandomPetAsync(string animal);

        /// <summary>
        /// Gets the pets by breed async.
        /// </summary>
        /// <returns>The pets by breed async.</returns>
        /// <param name="animal">Animal.</param>
        /// <param name="breed">Breed.</param>
        /// <param name="location">Either Zip or City,State.</param>
        Task<List<Animal>> GetPetsByBreedAsync(string animal, string breed, string location);

        /// <summary>
        /// Gets the shelters by breeds they have available.
        /// </summary>
        /// <returns>The shelters by breed.</returns>
        /// <param name="animal">Animal.</param>
        /// <param name="breed">Breed.</param>
        /// <param name="skip">Skip.</param>
        /// <param name="take">Take.</param>
        Task<List<Shelter>> GetSheltersByBreed(string animal, string breed, int skip, int take);

        /// <summary>
        /// Gets the shelters by location.
        /// </summary>
        /// <returns>The shelters by location.</returns>
        /// <param name="location">Either zip or City,State.</param>
        Task<List<Shelter>> GetSheltersByLocation(string location);

        /// <summary>
        /// Gets the pets at shelter.
        /// </summary>
        /// <returns>The pets at shelter.</returns>
        /// <param name="shelterId">Shelter identifier.</param>
        Task<List<Animal>> GetPetsAtShelter(string shelterId);
    }
}
