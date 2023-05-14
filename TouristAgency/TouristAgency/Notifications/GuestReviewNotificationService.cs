using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;

namespace TouristAgency.Notifications
{
    public class GuestReviewNotificationService
    {
        private readonly App _app;
        public GuestReviewNotificationRepository GuestReviewNotificationRepository { get; }

        public GuestReviewNotificationService()
        {
            _app = (App)App.Current;
            GuestReviewNotificationRepository = _app.GuestReviewNotificationRepository;
        }

        public List<GuestReviewNotification> GetByOwnerId(int id)
        {
            return GuestReviewNotificationRepository.GetAll().FindAll(r => r.Reservation.Accommodation.OwnerId == id);
        }

        public List<GuestReviewNotification> ReviewNotification(int ownerId,ReservationService reservationService)
        {
            DateTime today = DateTime.UtcNow.Date;

            List<GuestReviewNotification> notifications = new List<GuestReviewNotification>();
            double dateDiff;
            string message;

            foreach (var reservation in reservationService.GetUnreviewed(ownerId))
            {
                dateDiff = (today - reservation.End).TotalDays;

                if (today > reservation.End && dateDiff <= 5.0)
                {
                    GuestReviewNotification oldNotification = GuestReviewNotificationRepository.GetAll().Find(n => n.ReservationId == reservation.Id);
                    if (oldNotification != null)
                    {
                        GuestReviewNotificationRepository.Delete(oldNotification.Id);
                    }

                    message = $"{reservation.Guest.FirstName} {reservation.Guest.LastName} {dateDiff} days left to review";
                    GuestReviewNotification newNotification = new GuestReviewNotification(message);
                    notifications.Add(GuestReviewNotificationRepository.Create(newNotification));
                }
            }

            return notifications;

        }
    }
}
