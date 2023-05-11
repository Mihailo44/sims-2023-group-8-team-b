using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.PostponementFeatures.Domain
{
    public class PostponementRequestFileStorage : IStorage<PostponementRequest>
    {
        private Serializer<PostponementRequest> _serializer;
        private readonly string _file = "postponementrequests.txt";

        public PostponementRequestFileStorage()
        {
            _serializer = new Serializer<PostponementRequest>();
        }

        public List<PostponementRequest> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<PostponementRequest> requests)
        {
            _serializer.ToCSV(_file, requests);
        }
    }
}
