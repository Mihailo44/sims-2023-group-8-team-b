using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;

namespace TouristAgency.Users.ReviewFeatures.Domain
{
    public class GuestReviewService
    {
        private readonly App app = (App)System.Windows.Application.Current;
        public GuestReviewRepository GuestReviewRepository { get; }

        public GuestReviewService()
        {
            GuestReviewRepository = app.GuestReviewRepository;
        }

        public GuestReview GetByReservationId(int reservationId)
        {
            return GuestReviewRepository.GetAll().FirstOrDefault(g => g.Reservation.Id == reservationId);
        }

        public List<GuestReview> GetValidByReservationId(List<Reservation> reservations)
        {
            List<GuestReview> guestReviews = new List<GuestReview>();

            foreach (Reservation reservation in reservations)
            {
                GuestReview guestReview = GetByReservationId(reservation.Id);
                if (guestReview != null)
                {
                    guestReviews.Add(guestReview);
                }
            }

            return guestReviews;
        }


    }
}
