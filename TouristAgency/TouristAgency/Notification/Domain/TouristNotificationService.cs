using System.Collections.Generic;
using TouristAgency.TourRequests;
using TouristAgency.Tours;

namespace TouristAgency.Vouchers
{
    public class TouristNotificationService
    {
        private readonly App _app;
        public TouristNotificationRepository TouristNotificationRepository { get; }

        public TouristNotificationService()
        {
            _app = (App)System.Windows.Application.Current;
            TouristNotificationRepository = _app.TouristNotificationRepository;
        }

        public TouristNotification Create(TouristNotification newTouristNotification)
        {
            return TouristNotificationRepository.Create(newTouristNotification);
        }

        public List<TouristNotification> GetByTouristID(int TouristID)
        {
            List<TouristNotification> notifications = TouristNotificationRepository.GetAll().FindAll(t => t.TouristID == TouristID);
            notifications.Reverse();
            return notifications;
        }

        public List<TouristNotification> GetUnseen()
        {
            List<TouristNotification> notifications = TouristNotificationRepository.GetAll().FindAll(n => n.IsSeen == false);
            notifications.Reverse();
            return notifications;
        }

        public void NotifyAboutNewTour(Tour newTour, List<TourRequest> tourRequests)
        {
            foreach(TourRequest request in tourRequests)
            {
                bool equalByLocation = request.ShortLocation.Equals(newTour.ShortLocation);
                bool equalByLanguage = request.Language == newTour.Language;
                if (equalByLocation)
                {
                    string message = "A new tour based on " + request.ShortLocation.Country + ", " + request.ShortLocation.City;
                    TouristNotification notification = new TouristNotification(request.TouristID, Util.TouristNotificationType.SUGGESTED_TOUR, message);
                    notification.Tour = newTour;
                    notification.TourID = newTour.ID;
                    if (!IsNotified(request.TouristID, message))
                    {
                        TouristNotificationRepository.Create(notification);
                    }
                }
                if(equalByLanguage)
                {
                    string message = "A new tour based on " + request.Language;
                    TouristNotification notification = new TouristNotification(request.TouristID, Util.TouristNotificationType.SUGGESTED_TOUR, message);
                    notification.Tour = newTour;
                    notification.TourID = newTour.ID;
                    if (!IsNotified(request.TouristID, message))
                    {
                        TouristNotificationRepository.Create(notification);
                    }
                }
            }
        }

        public bool IsNotified(int touristID, string message) 
        {
            foreach(TouristNotification notification in TouristNotificationRepository.GetAll())
            {
                if(notification.TouristID == touristID && notification.Message == message)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
