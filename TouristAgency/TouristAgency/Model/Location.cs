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
        private int _id;
        private string _city;
        private string _country;
        private int _reservedAccommodationsNum = 0;

        public Location()
        {
            _id = -1;
        }

        public Location(string city, string country)
        {
            _city = city;
            _country = country;
        }

        public int Id
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;
                }
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if(_city != value)
                {
                    _city = value;
                }
            }
        }

        public string Country
        {
            get => _country; 
            set
            {
                if (_country != value)
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

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                City,
                Country
            };

            return csvValues;
        }
    }
}
