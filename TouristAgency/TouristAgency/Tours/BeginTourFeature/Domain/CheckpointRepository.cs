using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Tours.BeginTourFeature.Domain
{
    public class CheckpointRepository : ICrud<Checkpoint>, ISubject
    {
        private readonly IStorage<Checkpoint> _storage;
        private readonly List<Checkpoint> _checkpoints;
        private List<IObserver> _observers;

        public CheckpointRepository(IStorage<Checkpoint> storage)
        {
            _storage = storage;
            _checkpoints = _storage.Load(); ;
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if (_checkpoints.Count == 0)
            {
                return 0;
            }
            return _checkpoints.Max(c => c.ID) + 1;
        }

        public Checkpoint Create(Checkpoint newCheckpoint)
        {
            newCheckpoint.ID = GenerateId();
            _checkpoints.Add(newCheckpoint);
            _storage.Save(_checkpoints);
            NotifyObservers();
            return newCheckpoint;
        }

        public List<Checkpoint> GetAll()
        {
            return _checkpoints;
        }

        public Checkpoint GetById(int id)
        {
            return _checkpoints.Find(c => c.ID == id);
        }

        public Checkpoint Update(Checkpoint newCheckpoint, int id)
        {
            Checkpoint currentCheckpoint = GetById(id);
            if (currentCheckpoint == null)
            {
                return null;
            }
            currentCheckpoint.AttractionName = newCheckpoint.AttractionName;
            currentCheckpoint.Location = newCheckpoint.Location; //! Duboka kopija?
            _storage.Save(_checkpoints);
            NotifyObservers();
            return currentCheckpoint;
        }

        public void Delete(int id)
        {
            Checkpoint currentCheckpoint = GetById(id);
            _checkpoints.Remove(currentCheckpoint);
        }

        public void LoadLocationsToCheckpoints(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Checkpoint checkpoint in _checkpoints)
                {
                    if (checkpoint.LocationID == location.ID)
                    {
                        checkpoint.Location = new Location(location);
                    }
                }
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer); ;
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer); ;
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
