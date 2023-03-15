using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class TourCheckpoint : ISerializable
    {
        private int _tourID;
        private int _checkpointID;

        public TourCheckpoint()
        {
            _tourID = -1;
            _checkpointID = -1;
        }

        public TourCheckpoint(int tourID, int checkpointID)
        {
            _tourID = tourID;
            _checkpointID = checkpointID;
        }

        public int TourID
        {
            get => _tourID;
            set => _tourID = value;
        }

        public int CheckpointID
        {
            get => _checkpointID;
            set => _checkpointID = value;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _tourID.ToString(),
                _checkpointID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _tourID = int.Parse(values[0]);
            _checkpointID = int.Parse(values[1]);
        }
    }
}
