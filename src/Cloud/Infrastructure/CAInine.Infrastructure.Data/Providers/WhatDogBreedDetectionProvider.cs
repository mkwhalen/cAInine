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

            var contentString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ProcessedDogResult>(JsonConvert.DeserializeObject<string>(contentString)); // note: the what-dog api double serializes the json, so we need to double down too.
                return result;
            }
            else
            {
                Console.WriteLine("Error posting dog data:");
                Console.WriteLine(contentString);
                throw new Exception("Unable to process dog request.");
            }
        }
    }
}
