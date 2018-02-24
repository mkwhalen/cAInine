using System;
using System.Collections.Generic;
using System.Text;

namespace CAInine.Core.Models.Transfer.DogProcessing
{
    /// <summary>
    /// Model to represent the data retrieved from processing a dog image
    /// </summary>
    public class ProcessedDogResult
    {
        public bool IsDog { get; set; }
        public string BreedName { get; set; }
        public string Keywords { get; set; }
        public string AnalyticsEvent { get; set; }
    }
}
