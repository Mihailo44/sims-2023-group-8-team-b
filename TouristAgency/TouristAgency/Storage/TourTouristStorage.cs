using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class TourTouristStorage
    {
        private Serializer<TourTourist> _serializer;
        private readonly string _file = "tourtourist.txt";

        public TourTouristStorage()
        {
            _serializer = new Serializer<TourTourist>();
        }

        public List<TourTourist> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TourTourist> tourtourist)
        {
            _serializer.ToCSV(_file, tourtourist);
        }
    }
}
