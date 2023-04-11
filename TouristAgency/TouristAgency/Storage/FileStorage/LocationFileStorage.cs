using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage.FileStorage
{
    internal class LocationFileStorage : IStorage<Location>
    {
        private Serializer<Location> _serializer;
        private readonly string _file = "locations.txt";

        public LocationFileStorage()
        {
            _serializer = new Serializer<Location>();
        }

        public List<Location> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Location> locations)
        {
            _serializer.ToCSV(_file, locations);
        }
    }
}
