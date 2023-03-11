using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Model;

namespace TouristAgency.Storage
{
    public class OwnerStorage
    {
        private Serializer<Owner> serializer;
        private readonly string file = "owners.txt";

        public OwnerStorage()
        {
            serializer = new Serializer<Owner>();
        }

        public List<Owner> Load()
        {
            return serializer.FromCSV(file);
        }

        public void Save(List<Owner> owners)
        {
            serializer.ToCSV(file,owners);
        }
    }
}
