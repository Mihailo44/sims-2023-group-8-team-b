using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Review.GuestReviewDisplayFeature.Domain;

namespace TouristAgency.Review.Domain
{
    public class GuestReviewService
    {
        private readonly App app = (App)System.Windows.Application.Current;
        public GuestReviewRepository GuestReviewRepository { get; }

        public GuestReviewService()
        {
            GuestReviewRepository = app.GuestReviewRepository;
        }

        public List<GuestReview> GetValidByReservationId(List<Reservation> reservations)
        {
            List<GuestReview> guestReviews = new List<GuestReview>();
            
            foreach(Reservation reservation in reservations)
            {
                guestReviews.Add(GuestReviewRepository.GetById(reservation.Id));
            }

            return guestReviews;
        }

        
    }
}
