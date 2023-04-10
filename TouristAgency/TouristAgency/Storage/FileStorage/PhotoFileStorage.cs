using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage.FileStorage
{
    public class PhotoFileStorage : IStorage<Photo>
    {
        private Serializer<Photo> _serializer;
        private readonly string _file = "photos.txt";

        public PhotoFileStorage()
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
