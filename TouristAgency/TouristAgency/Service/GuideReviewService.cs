using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model.Enums;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Storage.FileStorage;

namespace TouristAgency.Service
{
    public class GuideReviewService
    {
        private readonly App _app;
        private GuideReviewRepository _guideReviewRepository;

        public GuideReviewService()
        {
            _app = (App)App.Current;
            _guideReviewRepository = _app.GuideReviewRepository;
        }

        public List<GuideReview> GetByGuideId(int id)
        {
            return _guideReviewRepository.GetAll().FindAll(g => g.Tour.AssignedGuideID == id);
        }

        public List<GuideReview> GetReviewsForGuideTourID(int guideID, int tourID)
        {
            List<GuideReview> reviews= new List<GuideReview>();

            foreach (var guideReview in _guideReviewRepository.GetAll())
            {

                if (guideReview.Tour.AssignedGuideID == guideID &&  guideReview.TourID == tourID)
                {
                    reviews.Add(guideReview);
                }
            }
            return reviews;
        }
    }
}
