using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Accommodations.ForumFeatures.Domain
{
    public class ForumCommentFileStorage : IStorage<ForumComment>
    {
        private Serializer<ForumComment> _serializer;
        private readonly string _file = "comments.txt";

        public ForumCommentFileStorage()
        {
            _serializer = new Serializer<ForumComment>();
        }

        public List<ForumComment> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<ForumComment> comments)
        {
            _serializer.ToCSV(_file, comments);
        }
    }
}
