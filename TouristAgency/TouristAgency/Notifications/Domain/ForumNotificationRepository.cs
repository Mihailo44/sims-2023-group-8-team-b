using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Users.ForumFeatures.Domain;

namespace TouristAgency.Notifications.Domain
{
    public class ForumNotificationRepository : ICrud<ForumNotification>, ISubject
    {
        private readonly IStorage<ForumNotification> _storage;
        private readonly List<ForumNotification> _forumNotifications;
        private readonly List<IObserver> _observers;

        public ForumNotificationRepository(IStorage<ForumNotification> storage)
        {
            _storage = storage;
            _forumNotifications = _storage.Load();
            _observers = new List<IObserver>();
        }

        private int GenerateId()
        {
            return _forumNotifications.Count() == 0 ? 0 : _forumNotifications.Max(f => f.Id) + 1;
        }

        public ForumNotification GetById(int id)
        {
            return _forumNotifications.Find(f => f.Id == id);
        }

        public ForumNotification Create(ForumNotification newNotification)
        {
            newNotification.Id = GenerateId();
            _forumNotifications.Add(newNotification);
           // _storage.Save(_forumNotifications);
            NotifyObservers();

            return newNotification;
        }

        public ForumNotification Update(ForumNotification updatedNotification, int id)
        {
            ForumNotification currentNotification = GetById(id);

            currentNotification.Message = updatedNotification.Message;
            currentNotification.Created = updatedNotification.Created;

           // _storage.Save(_forumNotifications);
            NotifyObservers();

            return currentNotification;
        }

        public void Delete(int id)
        {
            ForumNotification forumNotification = GetById(id);
            if(forumNotification != null)
            {
                _forumNotifications.Remove(forumNotification);
               // _storage.Save(_forumNotifications);
                NotifyObservers();
            }
        }

        public List<ForumNotification> GetAll()
        {
            return _forumNotifications;
        }

        public void LoadForumsToNotifications(List<Forum> forums)
        {
            foreach (ForumNotification forumNotification in _forumNotifications)
            {
                forumNotification.Forum = forums.Find(f => f.Id == forumNotification.Forum.Id);
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
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
