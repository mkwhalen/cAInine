using System;
using System.Threading.Tasks;
using CAInine.Core.Models.Entities;
using CAInine.Core.Models.Results;

namespace CAInine.Apps.Core.Interfaces.Providers
{
    /// <summary>
    /// Breed detection provider.
    /// </summary>
    public interface IBreedDetectionProvider
    {
        Task<Result<SubmittedDog>> SubmitDogImageAsync(string localImageFilePath);
    }
}
