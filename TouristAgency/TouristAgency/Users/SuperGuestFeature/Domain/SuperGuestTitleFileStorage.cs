using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Users.SuperGuestFeature.Domain
{
    public class SuperGuestTitleFileStorage : IStorage<SuperGuestTitle>
    {
        private Serializer<SuperGuestTitle> _serializer;
        private readonly string _file = "superguesttitles.txt";

        public SuperGuestTitleFileStorage()
        {
            _serializer = new Serializer<SuperGuestTitle>();
        }

        public List<SuperGuestTitle> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<SuperGuestTitle> titles)
        {
            _serializer.ToCSV(_file, titles);
        }
    }
}
