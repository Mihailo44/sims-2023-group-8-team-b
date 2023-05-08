using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Tours.BeginTourFeature.Domain
{
    public class CheckpointFileStorage : IStorage<Checkpoint>
    {
        private Serializer<Checkpoint> _serializer;
        private readonly string _file = "checkpoints.txt";

        public CheckpointFileStorage()
        {
            _serializer = new Serializer<Checkpoint>();
        }

        public List<Checkpoint> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Checkpoint> checkpoints)
        {
            _serializer.ToCSV(_file, checkpoints);
        }
    }
}
