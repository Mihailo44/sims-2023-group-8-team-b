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
        private int _id;
        private int _tourID;
        private int _checkpointID;
        private bool _isVisited;
        private Checkpoint _checkpoint;
        private List<Tourist> _arrivedTourists;

        public TourCheckpoint()
        {
            _tourID = -1;
            _checkpointID = -1;
            _isVisited = false;
            _arrivedTourists = new List<Tourist>();
        }

        public TourCheckpoint(int tourID, int checkpointID, bool isVisited)
        {
            _tourID = tourID;
            _checkpointID = checkpointID;
            _isVisited = isVisited;
            _arrivedTourists = new List<Tourist>();
        }

        public int ID
        {
            get => _id;
            set
            {
                if (value == _id)
                {
                    _id = value;
                }
            }
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

        public bool IsVisited
        {
            get => _isVisited;
            set => _isVisited = value;
        }

        public Checkpoint Checkpoint
        {
            get => _checkpoint;
            set => _checkpoint = value;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _tourID.ToString(),
                _checkpointID.ToString(),
                _isVisited.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _tourID = int.Parse(values[0]);
            _checkpointID = int.Parse(values[1]);
            _isVisited = Boolean.Parse(values[2]);
        }
    }
}
