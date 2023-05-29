using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;
using TouristAgency.TourRequests;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.Domain
{
    public class ComplexTourRequestFileStorage : IStorage<ComplexTourRequest>
    {
        private Serializer<ComplexTourRequest> _serializer;
        private readonly string _file = "complextourrequests.txt";

        public ComplexTourRequestFileStorage()
        {
            _serializer = new Serializer<ComplexTourRequest>();
        }

        public List<ComplexTourRequest> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<ComplexTourRequest> complexTourRequests)
        {
            _serializer.ToCSV(_file, complexTourRequests);
        }
    }
}
