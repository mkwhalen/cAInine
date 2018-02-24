using CAInine.Core.Models.Entities;
using CAInine.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Core.Interfaces.Services
{
    /// <summary>
    /// Business logic for processing dog data. Every method should return the result model
    /// </summary>
    public interface IDogProcessingService
    {
        /// <summary>
        /// Analyzes a dog image
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="imageDate"></param>
        /// <returns></returns>
        Task<Result<SubmittedDog>> AnalyzeDogImageAsync(string fileName, byte[] imageDate);

        /// <summary>
        /// Gets the submitted dogs for the given breed
        /// </summary>
        /// <param name="breed">The breed to search for</param>
        /// <returns>The list of submitted dogs wrapped in a result</returns>
        Task<Result<List<SubmittedDog>>> GetSubmittedDogsByBreedAsync(string breed);
    }
}
