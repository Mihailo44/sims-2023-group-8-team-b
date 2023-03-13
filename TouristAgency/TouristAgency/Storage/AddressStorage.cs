using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class AddressStorage
    {
        private Serializer<Address> _serializer;
        private readonly string _file = "address.txt";

        public AddressStorage() 
        {
            _serializer = new Serializer<Address>();
        }

        public List<Address> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Address> addresses)
        {
            _serializer.ToCSV(_file, addresses);
        }
    }
}
