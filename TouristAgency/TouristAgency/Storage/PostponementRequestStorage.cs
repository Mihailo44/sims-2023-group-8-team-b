using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class PostponementRequestStorage
    {
        private Serializer<PostponementRequest> _serializer;
        private readonly string _file = "postponementrequests.txt";

        public PostponementRequestStorage()
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
