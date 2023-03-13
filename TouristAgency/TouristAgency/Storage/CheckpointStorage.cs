using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class CheckpointStorage
    {
        private Serializer<Checkpoint> _serializer;
        private readonly string _file = "checkpoints.txt";

        public CheckpointStorage()
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
