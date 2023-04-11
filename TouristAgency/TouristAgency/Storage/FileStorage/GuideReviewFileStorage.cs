using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage.FileStorage
{
    public class GuideReviewFileStorage : IStorage<GuideReview>
    {
        private Serializer<GuideReview> _serializer;
        private readonly string _file = "guidereviews.txt";

        public GuideReviewFileStorage()
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
