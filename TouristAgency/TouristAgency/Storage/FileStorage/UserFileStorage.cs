using System.Collections.Generic;
using TouristAgency.Model;
using TouristAgency.Serialization;
using TouristAgency.Interfaces;

namespace TouristAgency.Storage.FileStorage
{
    public class UserFileStorage : IStorage<User>
    {
        private Serializer<User> _serializer;
        private readonly string _file = "users.txt";

        public UserFileStorage()
        {
            _serializer = new Serializer<User>();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(_file, users);
        }
    }
}
