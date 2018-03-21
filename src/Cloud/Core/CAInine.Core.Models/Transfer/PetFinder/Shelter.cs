using System;
namespace CAInine.Core.Models.Transfer
{
    public class Shelter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
