using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class TourFileStorage : IStorage<Tour>
    {
        private Serializer<Tour> _serializer;
        private readonly string _file = "tours.txt";

        public TourFileStorage()
        {
            _serializer = new Serializer<Tour>();
        }

        public List<Tour> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Tour> tours)
        {
            _serializer.ToCSV(_file, tours);
        }
    }
}
