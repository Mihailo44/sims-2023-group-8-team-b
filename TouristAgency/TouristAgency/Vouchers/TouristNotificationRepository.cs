using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;

namespace TouristAgency.Vouchers
{
    public class TouristNotificationRepository : ICrud<TouristNotification>, ISubject
    {
        private readonly IStorage<TouristNotification> _storage;
        private readonly List<TouristNotification> _notifications;
        private List<IObserver> _observers;

        public TouristNotificationRepository(IStorage<TouristNotification> storage)
        {
            _storage = storage;
            _notifications = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _notifications.Max(n => n.ID) + 1;
        }

        public TouristNotification GetById(int id)
        {
            return _notifications.Find(n => n.ID == id);
        }

        public TouristNotification Create(TouristNotification newNotification)
        {
            newNotification.ID = GenerateId();
            _notifications.Add(newNotification);
            _storage.Save(_notifications);
            NotifyObservers();

            return newNotification;
        }

        public TouristNotification Update(TouristNotification newNotification, int id)
        {
            TouristNotification currentNotification = GetById(id);

            if (currentNotification == null)
            {
                return null;
            }

            currentNotification.ID = newNotification.ID;
            currentNotification.TouristID = newNotification.TouristID;
            currentNotification.Type = newNotification.Type;
            currentNotification.Message = newNotification.Message;

            _storage.Save(_notifications);
            NotifyObservers();

            return currentNotification;
        }

        public void Delete(int id)
        {
            TouristNotification deletedNotification = GetById(id);
            _notifications.Remove(deletedNotification);
            _storage.Save(_notifications);
            NotifyObservers();
        }

        public List<TouristNotification> GetAll()
        {
            return _notifications;
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
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
