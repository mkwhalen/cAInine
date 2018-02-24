using System;
using System.Collections.Generic;
using System.Text;

namespace CAInine.Core.Models.Configuration
{
    /// <summary>
    /// Model for representing urls found from the appsettings.json file
    /// </summary>
    public class Urls
    {
        public string BlobStorageBaseUrl { get; set; }
        public string DogDetectionUrl { get; set; }
    }
}
