using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;
using TouristAgency.Users.SuperGuestFeature.Domain;

namespace TouristAgency.Accommodations.ForumFeatures.Domain
{
    public class ForumFileStorage : IStorage<Forum>
    {
        private Serializer<Forum> _serializer;
        private readonly string _file = "forums.txt";

        public ForumFileStorage()
        {
            _serializer = new Serializer<Forum>();
        }

        public List<Forum> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Forum> forums)
        {
            _serializer.ToCSV(_file, forums);
        }
    }
}
