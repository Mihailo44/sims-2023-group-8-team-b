using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class AccommodationPhotos
    {
        private int _accommodationId;
        private string _photoUrl;

        public AccommodationPhotos()
        {

        }

        public AccommodationPhotos(int accommodationId, string photoUrl)
        {
            _accommodationId = accommodationId;
            _photoUrl = photoUrl;
        }

        public int AccommodationId 
        {
            get => _accommodationId; 
            set { _accommodationId = value; } 
        }

        public string PhotoUrl 
        {
            get => _photoUrl;
            set { _photoUrl = value; }
        }
    }
}
