using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class GuestFileStorage : IStorage<Guest>
    {
        private Serializer<Guest> _serializer;
        private readonly string _file = "guests.txt";

        public GuestFileStorage()
        {
            _serializer = new Serializer<Guest>();
        }

        public List<Guest> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Guest> guests)
        {
            _serializer.ToCSV(_file, guests);
        }
    }
}
