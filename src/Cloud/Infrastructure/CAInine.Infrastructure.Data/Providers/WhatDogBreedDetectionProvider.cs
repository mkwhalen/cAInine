using CAInine.Core.Interfaces.Providers;
using CAInine.Core.Models.Configuration;
using CAInine.Core.Models.Transfer.DogProcessing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Infrastructure.Data.Providers
{
    /// <summary>
    /// Breed detection provider using Microsoft's What-Dog.net service
    /// </summary>
    public class WhatDogBreedDetectionProvider : IBreedDetectionProvider
    {
        private readonly HttpClient _client;
        private readonly IOptions<Urls> _urlSettings;
        public WhatDogBreedDetectionProvider(HttpClient client, IOptions<Urls> urlSettings)
        {
            _client = client;
            _urlSettings = urlSettings;
        }

        /// <summary>
        /// Submits the dog request to the what-dog analyze endpoint and returns the processed data.
        /// </summary>
        /// <param name="model">Request model</param>
        /// <returns>Response model</returns>
        /// <exception cref="Exception">Throws if error while getting response.</exception>
        public async Task<ProcessedDogResult> SubmitDogAsync(ProcessDogRequest model)
        {
            var response = await _client.PostAsync(_urlSettings.Value.DogDetectionUrl, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            if(response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProcessedDogResult>(jsonContent);
                return result;
            }
            else
            {
                throw new Exception("Unable to process dog request.")
            }
        }
    }
}
