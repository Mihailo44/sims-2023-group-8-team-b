using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Location : ISerializable
    {
        private int _ID;
        private string _street;
        private string _streetNumber;
        private string _city;
        private string _country;
        private int _reservedAccommodationsNum = 0;

        public Location()
        {
            _ID = -1;
        }

        public Location(string street, string streetNumber, string city, string country)
        {
            _street = street;
            _streetNumber = streetNumber;
            _city = city;
            _country = country;
        }

        public Location(string city, string country)
        {
            _street = "";
            _streetNumber = "";
            _city = city;
            _country = country;
        }

        public Location(Location originalLocation)
        {
            _ID = originalLocation._ID;
            _street = originalLocation.Street;
            _streetNumber = originalLocation.StreetNumber;
            _city = originalLocation.City;
            _country = originalLocation.Country;
        }

        public int Id
        {
            get { return _ID; }
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                }
            }
        }

        public string Street
        { 
            get { return _street; }
            set
            {
                if (value != _street)
                {
                    _street = value;
                }
            }
        }

        public string StreetNumber
        { 
            get { return _streetNumber; }
            set
            {
                if (value != _streetNumber)
                {
                    _streetNumber = value;
                }
            }
        }

        public string City 
        { 
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    _city = value;
                }
            }
        }

        public string Country 
        { 
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
                }
            }
        }
        public int ReservedAccommodationsNum
        {
            get => _reservedAccommodationsNum;
            set
            {
                if (_reservedAccommodationsNum != value)
                {
                    _reservedAccommodationsNum = value;
                }
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _street,
                _streetNumber,
                _city,
                _country
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _street = values[1];
            _streetNumber = values[2];
            _city = values[3];
            _country = values[4];
        }
    }
}
