using System;
using System.Threading.Tasks;
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
        Task<petfinderBreedList> GetAvailableBreedsAsync(string animal);

        /// <summary>
        /// Gets the random pet async.
        /// </summary>
        /// <returns>The random pet async.</returns>
        /// <param name="animal">Animal.</param>
        Task<petfinderPetRecord> GetRandomPetAsync(string animal);

        /// <summary>
        /// Gets the pets by breed async.
        /// </summary>
        /// <returns>The pets by breed async.</returns>
        /// <param name="animal">Animal.</param>
        /// <param name="breed">Breed.</param>
        /// <param name="location">Either Zip or City,State.</param>
        Task<petfinderPetRecordList> GetPetsByBreedAsync(string animal, string breed, string location);

        /// <summary>
        /// Gets the shelters by breeds they have available.
        /// </summary>
        /// <returns>The shelters by breed.</returns>
        /// <param name="animal">Animal.</param>
        /// <param name="breed">Breed.</param>
        /// <param name="skip">Skip.</param>
        /// <param name="take">Take.</param>
        Task<petfinderShelterRecordList> GetSheltersByBreed(string animal, string breed, int skip, int take);

        /// <summary>
        /// Gets the shelters by location.
        /// </summary>
        /// <returns>The shelters by location.</returns>
        /// <param name="location">Either zip or City,State.</param>
        Task<petfinderShelterRecordList> GetSheltersByLocation(string location);

        /// <summary>
        /// Gets the pets at shelter.
        /// </summary>
        /// <returns>The pets at shelter.</returns>
        /// <param name="shelterId">Shelter identifier.</param>
        Task<petfinderPetRecordList> GetPetsAtShelter(string shelterId);
    }
}
