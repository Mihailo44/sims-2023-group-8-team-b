using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Requests.Domain;

namespace TouristAgency.Accommodations.RenovationFeatures.DomainA
{
    public class RenovationRecommendationService
    {
        private readonly App app = (App)System.Windows.Application.Current;
        public RenovationRecommendationRepository RenovationRecommendationRepository { get; }

        public RenovationRecommendationService()
        {
            RenovationRecommendationRepository = app.RenovationRecommendationRepository;
        }
    }
}
