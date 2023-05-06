using System.Collections.Generic;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;
using TouristAgency.TourRequests;

namespace TouristAgency.Storage.FileStorage
{
    public class TourRequestFileStorage : IStorage<TourRequest>
    {
        private Serializer<TourRequest> _serializer;
        private readonly string _file = "tourrequests.txt";

        public TourRequestFileStorage()
        {
            _serializer = new Serializer<TourRequest>();
        }

        public List<TourRequest> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TourRequest> tourRequests)
        {
            _serializer.ToCSV(_file, tourRequests);
        }
    }
}
