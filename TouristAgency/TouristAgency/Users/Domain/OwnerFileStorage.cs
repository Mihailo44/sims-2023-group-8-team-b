using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.Domain
{
    public class OwnerFileStorage : IStorage<Owner>
    {
        private Serializer<Owner> _serializer;
        private readonly string _file = "owners.txt";

        public OwnerFileStorage()
        {
            _serializer = new Serializer<Owner>();
        }

        public List<Owner> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Owner> owners)
        {
            _serializer.ToCSV(_file, owners);
        }
    }
}
