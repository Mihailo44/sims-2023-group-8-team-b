using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Accommodations.RenovationFeatures.DomainA
{
    public class RenovationFileStorage : IStorage<Renovation>
    {
        private Serializer<Renovation> _serializer;
        private readonly string _file = "renovations.txt";

        public RenovationFileStorage()
        {
            _serializer = new Serializer<Renovation>();
        }

        public List<Renovation> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Renovation> renovations)
        {
            _serializer.ToCSV(_file, renovations);
        }
    }
}
