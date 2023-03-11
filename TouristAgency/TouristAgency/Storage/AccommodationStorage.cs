using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Model;

namespace TouristAgency.Storage
{
    public class AccommodationStorage
    {
        private Serializer<Accommodation> serializer;
        private readonly string file = "accommodations.txt";

        public AccommodationStorage()
        {
            serializer = new Serializer<Accommodation>();
        }

        public List<Accommodation> Load()
        {
            return serializer.FromCSV(file);
        }

        public void Save(List<Accommodation> accommodations)
        {
            serializer.ToCSV(file, accommodations);
        }
    }
}
