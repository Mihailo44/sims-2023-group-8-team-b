using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Address
    {
        private string _street;
        private string _streetNumber;
        private string _city;
        private string _country;

        public Address()
        {

        }

        public string Street
        { 
            get { return _street; } 
            set {  _street = value; } 
        }

        public string StreetNumber
        { 
            get { return _streetNumber; } 
            set { _streetNumber = value; } 
        }

        public string City 
        { 
            get { return _city; } 
            set { _city = value; } 
        }

        public string Country 
        { 
            get { return _country; } 
            set { _country = value; } 
        }
    }
}
