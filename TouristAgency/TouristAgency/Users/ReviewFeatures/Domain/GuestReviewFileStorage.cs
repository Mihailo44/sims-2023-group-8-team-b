using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.ReviewFeatures.Domain
{
    public class GuestReviewFileStorage : IStorage<GuestReview>
    {
        private Serializer<GuestReview> _serializer;
        private readonly string _file = "guestreviews.txt";

        public GuestReviewFileStorage()
        {
            _serializer = new Serializer<GuestReview>();
        }

        public List<GuestReview> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<GuestReview> guestReviews)
        {
            _serializer.ToCSV(_file, guestReviews);
        }
    }
}
