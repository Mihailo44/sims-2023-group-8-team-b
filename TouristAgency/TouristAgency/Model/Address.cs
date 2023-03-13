using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    public class Address
    {
        private int _ID;
        private string _street;
        private string _streetNumber;
        private string _city;
        private string _country;

        public Address()
        {

        }


        public Address(string street, string streetNumber, string city, string country)
        {
            _street = street;
            _streetNumber = streetNumber;
            _city = city;
            _country = country;
        }

        public Address(Address originalAddress)
        {
            _ID = originalAddress._ID;
            _street = originalAddress.Street;
            _streetNumber = originalAddress.StreetNumber;
            _city = originalAddress.City;
            _country = originalAddress.Country;
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
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
