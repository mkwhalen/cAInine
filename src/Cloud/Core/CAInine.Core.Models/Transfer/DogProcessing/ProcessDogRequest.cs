using System;
using System.Collections.Generic;
using System.Text;

namespace CAInine.Core.Models.Transfer.DogProcessing
{
    /// <summary>
    /// Model for sending data up to the dog processing provider
    /// </summary>
    public class ProcessDogRequest
    {
        public bool IsTest { get; set; }
        public string Version { get; set; }
        public string FaceUrl { get; set; }
        public string FaceName { get; set; }
    }
}
