﻿using CAInine.Core.Interfaces.Providers;
using CAInine.Core.Interfaces.Repositories;
using CAInine.Core.Interfaces.Services;
using CAInine.Core.Models.Entities;
using CAInine.Core.Models.Results;
using CAInine.Core.Models.Transfer.DogProcessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Infrastructure.Business.Services
{
    /// <summary>
    /// Business logic for processing dog data.
    /// Includes using repositories and other providers to handle the request.
    /// Each method should return a result and should never throw an exception
    /// </summary>
    public class DogProcessingService : IDogProcessingService
    {
        private readonly IBlobProvider _blobStorageProvider;
        private readonly IBreedDetectionProvider _breedDetectionProvider;
        private readonly ISubmittedDogRepository _subittedDogRepository;

        public DogProcessingService(IBlobProvider blobProvider, IBreedDetectionProvider breedDetectionProvider, ISubmittedDogRepository submittedDogRepository)
        {
            _blobStorageProvider = blobProvider;
            _breedDetectionProvider = breedDetectionProvider;
            _subittedDogRepository = submittedDogRepository;
        }

        /// <summary>
        /// Analyzes an image of a dog. Stores the image in blob storage, processes against the AI, 
        /// and stores the result data before returning
        /// </summary>
        /// <param name="fileName">Name of the uploaded file</param>
        /// <param name="imageDate">The actual image</param>
        /// <returns>The result of the request with a submitted dog object</returns>
        public async Task<Result<SubmittedDog>> AnalyzeDogImageAsync(string fileName, byte[] imageDate)
        {
            try
            {
                // validate input 
                if(string.IsNullOrEmpty(fileName) || imageDate == null)
                    return new InvalidResult<SubmittedDog>("Invalid image uploaded.");

                // submit the image data to blob storage
                var imageUrl = await _blobStorageProvider.UploadImageAsync(fileName, imageDate);
                if (string.IsNullOrEmpty(imageUrl))
                    return new InvalidResult<SubmittedDog>("Error uploading image file");

                // submit image to breed detection
                var detectedBreedResult = await _breedDetectionProvider.SubmitDogAsync(new ProcessDogRequest
                {
                    IsTest = false,
                    FaceName = imageUrl,
                    FaceUrl = imageUrl,
                    Version = "001"
                });

                // add submitted info to repository
                var entity = new SubmittedDog
                {
                    BreedName = detectedBreedResult.Breed,
                    BreedDetails = detectedBreedResult.About,
                    ImageUrl = imageUrl,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };
                var finalEntity = await _subittedDogRepository.AddAsync(entity);

                // return result
                return new SuccessResult<SubmittedDog>(finalEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error analyzing dog image:");
                Console.WriteLine(ex);
                return new UnexpectedResult<SubmittedDog>();
            }
        }
    }
}
