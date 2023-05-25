using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Util;

namespace TouristAgency.Notifications.Domain
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
            if (_notifications.Count == 0) return 0;
            else
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
            currentNotification.TourID = newNotification.TourID;
            currentNotification.Tour = newNotification.Tour;
            currentNotification.CheckpointID = newNotification.CheckpointID;
            currentNotification.Checkpoint = newNotification.Checkpoint;
            currentNotification.Type = newNotification.Type;
            currentNotification.Title = newNotification.Title;
            currentNotification.IsSeen = newNotification.IsSeen;

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
        public void LoadToursToNotifications(List<Tour> tours)
        {
            foreach (Tour tour in tours)
            {
                foreach (TouristNotification notification in GetAll())
                {
                    if (notification.TourID == tour.ID)
                    {
                        notification.Tour = tour;
                    }
                }
            }
        }

        public void LoadCheckpointsToNotifications(List<Checkpoint> checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                foreach (TouristNotification notification in GetAll())
                {
                    if (notification.CheckpointID == checkpoint.ID)
                    {
                        notification.Checkpoint = checkpoint;
                    }
                }
            }
        }
    }
}
