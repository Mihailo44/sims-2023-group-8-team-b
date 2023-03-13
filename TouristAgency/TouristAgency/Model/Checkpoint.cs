using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Checkpoint : ISerializable
    {
        private int _ID;
        private int _tourID;
        private string _attractionName;
        private bool _isVisited;
        private Address _address;
        private int _addressID;

        public int ID
        {
            get => _ID;
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                }
            }
        }

        public int TourID
        {
            get => _tourID;
            set
            {
                if (value != _tourID)
                {
                    _tourID = value;
                }
            }
        }

        public string AttractionName
        {
            get => _attractionName;
            set
            {
                if(value != _attractionName)
                {
                    _attractionName = value;
                }
            }
        }

        public bool IsVisited
        {
            get => _isVisited;
            set
            {
                if (value != _isVisited)
                {
                    _isVisited = value;
                }
            }
        }

        public int AddressID
        {
            get => _addressID;
            set
            {
                if (value != _addressID)
                {
                    _addressID = value;
                }
            }
        }

        public Address Address
        {
            get => _address;
            set
            {
                if (value != _address)
                {
                    _address = value;
                }
            }
        }

        public Checkpoint()
        {
            _ID = -1;
            _tourID = -1;
            _attractionName = "";
            _address = new Address();
            _isVisited = false;
        }

        public Checkpoint(int id, int tourId, string attractionName, bool isVisited, Address address)
        {
            _ID = id;
            _tourID = tourId;
            _attractionName = attractionName;
            _isVisited = isVisited;
            _address = new Address(address);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _attractionName,
                _isVisited.ToString(),
                _tourID.ToString(),
                _addressID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _attractionName = values[1];
            _isVisited = Convert.ToBoolean(values[2]);
            _tourID = Convert.ToInt32(values[3]);
            _addressID = Convert.ToInt32(values[4]);
        }
    }
}
