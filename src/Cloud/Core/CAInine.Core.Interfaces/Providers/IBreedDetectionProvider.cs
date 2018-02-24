using CAInine.Core.Models.Transfer.DogProcessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Core.Interfaces.Providers
{
    /// <summary>
    /// Data provider for communicating files and breed processed data
    /// </summary>
    public interface IBreedDetectionProvider
    {
        /// <summary>
        /// Submits a request to process a dog and detect its breed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ProcessedDogResult> SubmitDogAsync(ProcessDogRequest model);
    }
}
