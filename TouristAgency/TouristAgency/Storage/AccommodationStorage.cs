using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Model;

namespace TouristAgency.Storage
{
    public class AccommodationStorage
    {
        private Serializer<Accommodation> _serializer;
        private readonly string _file = "accommodations.txt";

        public AccommodationStorage()
        {
            _serializer = new Serializer<Accommodation>();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(_file, accommodations);
        }
    }
}
