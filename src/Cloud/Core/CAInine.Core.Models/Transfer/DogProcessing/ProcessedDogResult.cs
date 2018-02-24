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
        public string Breed { get; set; }
        public string About { get; set; }
    }
}
