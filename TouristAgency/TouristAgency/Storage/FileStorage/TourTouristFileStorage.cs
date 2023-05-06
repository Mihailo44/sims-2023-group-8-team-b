using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;
using TouristAgency.Tours;

namespace TouristAgency.Storage.FileStorage
{
    public class TourTouristFileStorage : IStorage<TourTourist>
    {
        private Serializer<TourTourist> _serializer;
        private readonly string _file = "tourtourist.txt";

        public TourTouristFileStorage()
        {
            _serializer = new Serializer<TourTourist>();
        }

        public List<TourTourist> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TourTourist> tourtourist)
        {
            _serializer.ToCSV(_file, tourtourist);
        }
    }
}
