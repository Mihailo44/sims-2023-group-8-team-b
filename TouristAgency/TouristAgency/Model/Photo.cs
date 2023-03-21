using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Photo : ISerializable
    {
        private int _ID;
        private string _link;
        private char _type; //T for tour, A for accomodation
        private int _externalID; //tourID or accomodationID

        public Photo()
        {
            _ID = -1;
        }

        public Photo(string link, char type, int externalID)
        {
            _link = link;
            _type = type;
            _externalID = externalID;
        }

        public Photo(Photo originalPhoto)
        {
            _ID = originalPhoto.ID;
            _link = originalPhoto.Link;
            _type = originalPhoto.Type;
            _externalID = originalPhoto.ExternalID;
        }

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

        public string Link
        {
            get => _link;
            set
            {
                if (value != _link)
                {
                    _link = value;
                }
            }
        }

        public char Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                }
            }
        }

        public int ExternalID
        {
            get => _externalID; 
            set
            {
                if (value != _externalID)
                {
                    _externalID = value;
                }
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _type.ToString(),
                _externalID.ToString(),
                _link
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _type = Convert.ToChar(values[1]);
            _externalID = Convert.ToInt32(values[2]);
            _link = values[3];
        }
    }
}
