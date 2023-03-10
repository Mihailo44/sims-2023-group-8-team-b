using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    public class Location
    {
        private int _id;
        private string _city;
        private string _country;
        private int _reservedAccommodationsNum;

        public Location()
        {
            _id = -1;
        }

        public Location(string city, string country, int reservedAccommodationsNum)
        {
            _city = city;
            _country = country;
            _reservedAccommodationsNum = reservedAccommodationsNum;
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
    }
}
