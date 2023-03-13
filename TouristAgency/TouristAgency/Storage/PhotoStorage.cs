using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class PhotoStorage
    {
        private Serializer<Photo> _serializer;
        private readonly string _file = "photos.txt";

        public PhotoStorage()
        {
            _serializer = new Serializer<Photo>();
        }

        public List<Photo> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Photo> photos)
        {
            _serializer.ToCSV(_file, photos);
        }
    }
}
