using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class UserCommentFileStorage : IStorage<UserComment>
    {
        private Serializer<UserComment> _serializer;
        private readonly string _path = "usercomment.txt";

        public UserCommentFileStorage()
        {
            _serializer = new Serializer<UserComment>();
        }

        public List<UserComment> Load()
        {
           return _serializer.FromCSV(_path);
        }

        public void Save(List<UserComment> list)
        {
            _serializer.ToCSV(_path,list);
        }
    }
}
