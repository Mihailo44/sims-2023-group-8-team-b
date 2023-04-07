using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class GuideReviewStorage
    {
        private Serializer<GuideReview> _serializer;
        private readonly string _file = "guidereviews.txt";

        public GuideReviewStorage()
        {
            _serializer = new Serializer<GuideReview>();
        }

        public List<GuideReview> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<GuideReview> guideReviews)
        {
            _serializer.ToCSV(_file, guideReviews);
        }
    }
}
