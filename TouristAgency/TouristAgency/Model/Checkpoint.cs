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

        public bool IsSelected { get; set; }

        public Checkpoint()
        {
            _ID = -1;
            _attractionName = "";
            _location = new Location();
        }

        public Checkpoint(int id, string attractionName, bool isVisited, Location location)
        {
            _ID = id;
            _attractionName = attractionName;
            _location = new Location(location);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _attractionName,
                _locationID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _attractionName = values[1];
            _locationID = Convert.ToInt32(values[2]);
        }
    }
}
