﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class TourTouristCheckpoint : ISerializable
    {
        private TourCheckpoint _tourCheckpoint;
        private int _touristID;

        public TourTouristCheckpoint()
        {
            _tourCheckpoint = new TourCheckpoint();
            _tourCheckpoint.TourID = -1;
            _tourCheckpoint.CheckpointID = -1;
            _touristID = -1;
        }

        public TourTouristCheckpoint(int tourID, int touristID, int checkpointID)
        {
            _tourCheckpoint = new TourCheckpoint();
            _tourCheckpoint.TourID = tourID;
            _tourCheckpoint.CheckpointID = checkpointID;
            _touristID = touristID;
        }

        public TourCheckpoint TourCheckpoint
        {
            get => _tourCheckpoint;
            set => _tourCheckpoint = value;
        }

        public int TouristID
        {
            get => _touristID;
            set
            {
                if (value != _touristID)
                {
                    _touristID = value;
                }
            }
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _tourCheckpoint.TourID.ToString(),
                _touristID.ToString(),
                _tourCheckpoint.CheckpointID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _tourCheckpoint.TourID = Int32.Parse(values[0]);
            _touristID = Int32.Parse(values[1]);
            _tourCheckpoint.CheckpointID = Int32.Parse(values[2]);
        }
    }
}