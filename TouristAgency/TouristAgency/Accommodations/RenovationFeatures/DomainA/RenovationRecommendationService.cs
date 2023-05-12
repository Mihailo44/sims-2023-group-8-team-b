using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool IsAlreadySubmitted(RenovationRecommendation newRecommendation)
        {
            RenovationRecommendation oldRecommendation = RenovationRecommendationRepository.GetAll().FirstOrDefault(
                r => r.ReservationId == newRecommendation.ReservationId);
            if (oldRecommendation != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
