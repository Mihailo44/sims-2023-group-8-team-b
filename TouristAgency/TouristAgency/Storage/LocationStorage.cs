using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    internal class LocationStorage
    {
        private Serializer<Location> _serializer;
        private readonly string _file = "locations.txt";

        public LocationStorage()
        {
            _serializer = new Serializer<Location>();
        }

        public List<Location> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Location> locations)
        {
            _serializer.ToCSV(_file,locations);
        }
    }
}
