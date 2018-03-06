using System;
using System.Collections.Generic;
using CAInine.Core.Models.Transfer.PetFinder.Enums;

namespace CAInine.Core.Models.Transfer
{
    public class Animal
    {
        public string Id { get; set; }
        public string ShelterId { get; set; }
        public string ShelterPetId { get; set; }
        public string Name { get; set; }
        public string AnimalType { get; set; }
        public List<string> Breeds { get; set; }
        public bool IsMixBreed { get; set; }
        public string Description { get; set; }
        public PetGender Gender { get; set; }
        public PetAgeType Age { get; set; }
        public PetSize Size { get; set; }
    }
}
