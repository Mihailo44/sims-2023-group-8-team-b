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
        //private int _tourID;
        private string _attractionName;
        private bool _isVisited;
        private Location _location;
        private int _locationID;

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

        /*public int TourID
        {
            get => _tourID;
            set
            {
                if (value != _tourID)
                {
                    _tourID = value;
                }
            }
        }*/

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

        public int LocationID
        {
            get => _locationID;
            set
            {
                if (value != _locationID)
                {
                    _locationID = value;
                }
            }
        }

        public Location Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                }
            }
        }

        public Checkpoint()
        {
            _ID = -1;
            //_tourID = -1;
            _attractionName = "";
            _location = new Location();
            _isVisited = false;
        }

        public Checkpoint(int id, string attractionName, bool isVisited, Location location)
        {
            _ID = id;
            //_tourID = tourId;
            _attractionName = attractionName;
            _isVisited = isVisited;
            _location = new Location(location);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _attractionName,
                _isVisited.ToString(),
                //_tourID.ToString(),
                _locationID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _attractionName = values[1];
            _isVisited = Convert.ToBoolean(values[2]);
            //_tourID = Convert.ToInt32(values[3]);
            _locationID = Convert.ToInt32(values[3]);
        }
    }
}
