using System;
using System.Collections.Generic;
using System.Text;

namespace CAInine.Core.Models.Entities
{
    /// <summary>
    /// A submitted dog image and result
    /// </summary>
    public class SubmittedDog
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ImageUrl { get; set; }
        public string BreedName { get; set; } // can be null if the check was unsuccessful
        public string BreedDetails { get; set; }
    }
}
