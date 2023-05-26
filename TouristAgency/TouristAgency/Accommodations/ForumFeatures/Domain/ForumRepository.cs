using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Users.SuperGuestFeature.Domain;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.ForumFeatures.Domain
{
    public class ForumRepository : ICrud<Forum>, ISubject
    {
        private readonly IStorage<Forum> _storage;
        private readonly List<Forum> _forums;
        private List<IObserver> _observers;

        public ForumRepository(IStorage<Forum> storage)
        {
            _storage = storage;
            _forums = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _forums.Count() == 0 ? 0 : _forums.Max(f => f.Id) + 1;
        }

        public Forum GetById(int id)
        {
            return _forums.Find(f => f.Id == id);
        }

        public Forum Create(Forum newForum)
        {
            newForum.Id = GenerateId();
            _forums.Add(newForum);
            _storage.Save(_forums);
            NotifyObservers();

            return newForum;
        }

        public Forum Update(Forum updatedForum, int id)
        {
            Forum currentForum = GetById(id);

            if (currentForum == null)
            {
                return null;
            }

            currentForum.Name = updatedForum.Name;
            currentForum.IsUseful = updatedForum.IsUseful;

            _storage.Save(_forums);
            NotifyObservers();

            return currentForum;
        }

        public void Delete(int id)
        {
            Forum deletedForum = GetById(id);
            _forums.Remove(deletedForum);
            _storage.Save(_forums);
            NotifyObservers();
        }

        public List<Forum> GetAll()
        {
            return _forums;
        }

        public void LoadLocationsToForums(List<Location> locations)
        {
            foreach(Forum forum in _forums)
            {
                Location location = locations.Find(l => l.Id == forum.LocationId);
                if(location != null)
                {
                    forum.Location = location;
                }
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
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
