using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Tours.BeginTourFeature.Domain
{
    public class TourCheckpointFileStorage : IStorage<TourCheckpoint>
    {
        private Serializer<TourCheckpoint> _serializer;
        private readonly string _file = "tourcheckpoints.txt";

        public TourCheckpointFileStorage()
        {
            _serializer = new Serializer<TourCheckpoint>();
        }

        public List<TourCheckpoint> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TourCheckpoint> tourCheckpoints)
        {
            _serializer.ToCSV(_file, tourCheckpoints);
        }
    }
}
