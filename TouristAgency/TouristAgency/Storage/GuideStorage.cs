using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class GuideStorage
    {
        private Serializer<Guide> _serializer;
        private readonly string _file = "guides.txt";

        public GuideStorage()
        {
            _serializer = new Serializer<Guide>();
        }

        public List<Guide> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Guide> guides)
        {
            _serializer.ToCSV(_file, guides);
        }
    }
}
