using System;
namespace CAInine.Core.Models.Configuration
{
    /// <summary>
    /// Configuration manager for our pet finder api values
    /// </summary>
    public class PetFinder
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
