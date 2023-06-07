using System.Collections.Generic;
using TouristAgency.TourRequests;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Util;

namespace TouristAgency.Notifications.Domain
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
            foreach (TourRequest request in tourRequests)
            {
                bool equalByLocation = request.ShortLocation.Equals(newTour.ShortLocation);
                bool equalByLanguage = request.Language == newTour.Language;
                if (equalByLocation)
                {
                    string message = "A new tour based on " + request.ShortLocation.Country + ", " + request.ShortLocation.City;
                    TouristNotification notification = new TouristNotification(request.TouristID, Util.TouristNotificationType.SUGGESTED_TOUR_LOCATION, message);
                    notification.Tour = newTour;
                    notification.TourID = newTour.ID;
                    notification.Description = "Based on your previous requests, a new tour with the location " + request.ShortLocation.Country + ", " + request.ShortLocation.City + " is available. Check it out!";
                    if (!IsNotified(request.TouristID, message))
                    {
                        TouristNotificationRepository.Create(notification);
                    }
                }
                if (equalByLanguage)
                {
                    string message = "A new tour based on " + request.Language;
                    TouristNotification notification = new TouristNotification(request.TouristID, Util.TouristNotificationType.SUGGESTED_TOUR_LANGUAGE, message);
                    notification.Tour = newTour;
                    notification.TourID = newTour.ID;
                    notification.Description = "Based on your previous requests, a new tour with the language " + request.Language + " is available. Check it out!";
                    if (!IsNotified(request.TouristID, message))
                    {
                        TouristNotificationRepository.Create(notification);
                    }
                }
            }
        }

        public void NotifyAboutAttendance(int touristID, Checkpoint checkpoint, int checkpointID)
        {
            TouristNotification notification = new TouristNotification(touristID, TouristNotificationType.ATTENDANCE, "Attendance confirmation");
            notification.Checkpoint = checkpoint;
            notification.CheckpointID = checkpointID;
            notification.Description = "The guide has marked you as present at " + checkpoint.Location.Country + ", " + checkpoint.Location.City + ". Please confirm.";
            TouristNotificationRepository.Create(notification);
        }

        public void NotifyAboutWonVoucher(int touristID)
        {
            TouristNotification notification = new TouristNotification(touristID, TouristNotificationType.VOUCHER, "Won voucher");
            notification.Description = "You have won a voucher for booking 5 tours. Click here to see your vouchers.";
            TouristNotificationRepository.Create(notification);
        }

        public bool IsNotified(int touristID, string message)
        {
            foreach (TouristNotification notification in TouristNotificationRepository.GetAll())
            {
                if (notification.TouristID == touristID && notification.Title == message)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
