using System.Collections.Generic;

namespace TouristAgency.Review.Domain
{
    public class GuideReviewService
    {
        private readonly App _app;
        public GuideReviewRepository GuideReviewRepository { get; }

        public GuideReviewService()
        {
            _app = (App)System.Windows.Application.Current;
            GuideReviewRepository = _app.GuideReviewRepository;
        }

        public List<GuideReview> GetByGuideId(int id)
        {
            return GuideReviewRepository.GetAll().FindAll(g => g.Tour.AssignedGuideID == id);
        }

        public List<GuideReview> GetReviewsForGuideTourID(int guideID, int tourID)
        {
            List<GuideReview> reviews = new List<GuideReview>();
            foreach (var guideReview in GuideReviewRepository.GetAll())
            {

                if (guideReview.Tour.AssignedGuideID == guideID && guideReview.TourID == tourID)
                {
                    reviews.Add(guideReview);
                }
            }
            return reviews;
        }

        public double GetGuideScore(int guideID, int year)
        {
            int count = 0;
            double score = 0;
            foreach(GuideReview review in GuideReviewRepository.GetAll())
            {
                if(review.Tour.AssignedGuideID == guideID && review.Tour.StartDateTime.Year == year)
                {
                    score += review.OverallScore();
                    count++;
                }
            }

            return score / count;
        }
    }
}
