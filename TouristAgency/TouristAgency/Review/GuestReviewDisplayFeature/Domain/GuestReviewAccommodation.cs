using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Review.Domain;

namespace TouristAgency.Review.GuestReviewDisplayFeature.Domain
{
    public class GuestReviewAccommodation
    {
        private Accommodation _accommodation;
        private GuestReview _guestReview;


        public GuestReviewAccommodation(Accommodation accommodation, GuestReview guestReview)
        {
            _accommodation = accommodation;
            _guestReview = guestReview;
        }
        public string AccommodationName
        {
            get => _accommodation.Name;
        }

        public string OwnerName
        {
            get => _accommodation.Owner.FirstName + " " + _accommodation.Owner.LastName;
        }
        public int Cleanliness
        {
            get => _guestReview.Cleanliness;
        }

        public int RuleAbiding
        {
            get => _guestReview.RuleAbiding;
        }

        public int Communication
        {
            get => _guestReview.Communication;
        }

        public int OverallImpression
        {
            get => _guestReview.OverallImpression;
        }

        public int NoiseLevel
        {
            get => _guestReview.NoiseLevel;
        }

        public string Comment
        {
            get => _guestReview.Comment;
        }
    }
}
