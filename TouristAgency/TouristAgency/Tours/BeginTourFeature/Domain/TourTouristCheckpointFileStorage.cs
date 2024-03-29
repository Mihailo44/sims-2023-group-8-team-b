﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Tours.BeginTourFeature.Domain
{
    public class TourTouristCheckpointFileStorage : IStorage<TourTouristCheckpoint>
    {
        private Serializer<TourTouristCheckpoint> _serializer;
        private readonly string _file = "tourtouristcheckpoint.txt";

        public TourTouristCheckpointFileStorage()
        {
            _serializer = new Serializer<TourTouristCheckpoint>();
        }

        public List<TourTouristCheckpoint> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TourTouristCheckpoint> tourtouristcheckpoint)
        {
            _serializer.ToCSV(_file, tourtouristcheckpoint);
        }
    }
}
