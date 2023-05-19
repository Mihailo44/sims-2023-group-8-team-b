using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Notifications
{
    public class GuestReviewNotificationRepository : ICrud<GuestReviewNotification>,ISubject
    {
        private readonly IStorage<GuestReviewNotification> _storage;
        private readonly List<GuestReviewNotification> _notifications;
        private readonly List<IObserver> _observers;

        public GuestReviewNotificationRepository(IStorage<GuestReviewNotification> storage)
        {
            _storage = storage;
            _notifications = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _notifications.Count() == 0 ? 0 : _notifications.Max(n => n.Id) + 1;
        }

        public GuestReviewNotification GetById(int id)
        {
            return _notifications.Find(n => n.Id == id);
        }

        public GuestReviewNotification Create(GuestReviewNotification newNotification)
        {
            newNotification.Id = GenerateId();
            _notifications.Add(newNotification);
            _storage.Save(_notifications);
            NotifyObservers();

            return newNotification;
        }

        public GuestReviewNotification Update(GuestReviewNotification updatedNotification, int id)
        {
            GuestReviewNotification currentNotification = GetById(id);
            currentNotification.Message = updatedNotification.Message;
            // mozda menjati i datum
            _storage.Save(_notifications);
            NotifyObservers();

            return currentNotification;
        }

        public void Delete(int id)
        {
            GuestReviewNotification notification = GetById(id);
            if (notification == null)
                return;

            _notifications.Remove(notification);
            _storage.Save(_notifications);
            NotifyObservers();
        }

        public List<GuestReviewNotification> GetAll()
        {
            return _notifications;
        }

        public void LoadReservationsToNotifications(List<Reservation> reservations)
        {
            foreach(var notification in _notifications)
            {
                notification.Reservation = reservations.Find(r => r.Id == notification.ReservationId);
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
