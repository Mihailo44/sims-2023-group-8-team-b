﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;
using TouristAgency.Users;

namespace TouristAgency.Storage.FileStorage
{
    public class TouristFileStorage : IStorage<Tourist>
    {
        private Serializer<Tourist> _serializer;
        private readonly string _file = "tourists.txt";

        public TouristFileStorage()
        {
            _serializer = new Serializer<Tourist>();
        }

        public List<Tourist> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Tourist> tourists)
        {
            _serializer.ToCSV(_file, tourists);
        }
    }
}
