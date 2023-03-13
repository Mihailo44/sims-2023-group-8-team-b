using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class TourTourist : ISerializable
    {
        private int _tourID;
        private int _touristID;

        public int TourID
        {
            get => _tourID;
            set
            {
                if(value != _tourID)
                {
                    _tourID = value;
                }
            }
        }

        public int TouristID
        {
            get => _touristID;
            set
            {
                if (value != _touristID)
                {
                    _touristID = value;
                }
            }
        }

        public TourTourist()
        {
        }

        public TourTourist(int tourID, int touristID)
        {
            _tourID = tourID;
            _touristID = touristID;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _tourID.ToString(),
                _touristID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _tourID = Convert.ToInt32(values[0]);
            _touristID = Convert.ToInt32(values[1]);
        }
    }
}
