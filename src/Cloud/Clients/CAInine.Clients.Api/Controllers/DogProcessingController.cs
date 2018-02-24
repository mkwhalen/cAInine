using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CAInine.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAInine.Clients.Api.Controllers
{
    /// <summary>
    /// Http endpoints for the API of dog data processing
    /// </summary>
    [Route("api/[controller]")]
    public class DogProcessingController : BaseController
    {
        private readonly IDogProcessingService _dogProcessingService;
        public DogProcessingController(IDogProcessingService dogProcessingService)
        {
            _dogProcessingService = dogProcessingService;
        }

        /// <summary>
        /// Http POST endpoint for submitting a file to process the image.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("process")]
        public async Task<ActionResult> Post([FromForm]IFormFile file)
        {
            var result = await _dogProcessingService.AnalyzeDogImageAsync(file.FileName, GetDataFromFile(file));
            return FromResult(result);
        }

        /// <summary>
        /// Http GET endpoint for searching for submitted dogs by breed
        /// </summary>
        /// <param name="breed"></param>
        /// <returns></returns>
        [HttpGet("byBreed/{breed}")]
        public async Task<ActionResult> GetByBreed(string breed)
        {
            var result = await _dogProcessingService.GetSubmittedDogsByBreedAsync(breed);
            return FromResult(result);
        }

        /// <summary>
        /// Gets a byte array from a form file
        /// </summary>
        /// <param name="file">The form file</param>
        /// <returns>The raw data of the file</returns>
        private byte[] GetDataFromFile(IFormFile file)
        {
            using (var memStream = new MemoryStream())
            {
                file.CopyTo(memStream);
                var fileBytes = memStream.ToArray();
                return fileBytes;
            }
        }
    }
}
