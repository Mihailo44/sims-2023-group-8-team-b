using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class TourCheckpointStorage
    {
        private Serializer<TourCheckpoint> _serializer;
        private readonly string _file = "tourcheckpoints.txt";

        public TourCheckpointStorage()
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
