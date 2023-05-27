using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Accommodations.RenovationFeatures.Domain
{
    public class RenovationRecommendationFileStorage : IStorage<RenovationRecommendation>
    {
        private Serializer<RenovationRecommendation> _serializer;
        private readonly string _file = "renovationrecommendations.txt";

        public RenovationRecommendationFileStorage()
        {
            _serializer = new Serializer<RenovationRecommendation>();
        }

        public List<RenovationRecommendation> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<RenovationRecommendation> recommendations)
        {
            _serializer.ToCSV(_file, recommendations);
        }
    }
}
