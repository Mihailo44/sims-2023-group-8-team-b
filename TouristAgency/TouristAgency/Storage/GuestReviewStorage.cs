using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Model;

namespace TouristAgency.Storage
{
    public class GuestReviewStorage
    {
        private Serializer<GuestReview> _serializer;
        private readonly string _file = "guestreviews.txt";

        public GuestReviewStorage()
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
