using System;
using System.Threading.Tasks;
using CAInine.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAInine.Clients.Api.Controllers
{

    [Route("api/[controller]")]
    public class PetSearchController : BaseController
    {
        private readonly IPetFinderService _petFinderService;
        public PetSearchController(IPetFinderService petFinderService)
        {
            _petFinderService = petFinderService;
        }

        [HttpGet("shelters")]
        public async Task<IActionResult> GetShelter(string location)
        {
            var result = await _petFinderService.GetSheltersByLocation(location);
            return FromResult(result);
        }

        [HttpGet("pets/breed/{breed}")]
        public async Task<IActionResult> GetPets(string breed, string location)
        {
            var result = await _petFinderService.GetAnimalsByBreedAsync(breed, location);
            return FromResult(result);
        }


        [HttpGet("pets/shelter/{shelterId}")]
        public async Task<IActionResult> GetPets(string shelterId)
        {
            var result = await _petFinderService.GetAnimalsByShelterAsync(shelterId);
            return FromResult(result);
        }
    }
}
