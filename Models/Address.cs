using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprocketsSelbeeP2.Models
{
    public class Address
    {
        // == CONSTRUCTORS
        public Address() : this("TBD", "TBD", "TBD", "TBD")
        {
        }

        public Address(string street, string city, string state, string zip)
        {
            this.Street = street;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        // == PROPERTIES
        public string Street { get; set; }
        public string City {  get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        // == METHODS
        public override string ToString()
        {
            return $"{Street}\n{City}, {State} {Zip}";
        }
    }
}
