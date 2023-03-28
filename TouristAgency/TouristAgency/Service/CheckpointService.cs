using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage;
using TouristAgency.View.Home;

namespace TouristAgency.Service
{
    public class CheckpointService : ICrud<Checkpoint>, ISubject
    {
        private readonly CheckpointStorage _storage;
        private readonly List<Checkpoint> _checkpoints;
        private List<IObserver> _observers;

        public CheckpointService()
        {
            _storage = new CheckpointStorage();
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

        public Checkpoint FindById(int id)
        {
            return _checkpoints.Find(c => c.ID == id);
        }

        public List<Checkpoint> FindSuitableByLocation(Location location)
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();
            foreach (Checkpoint checkpoint in _checkpoints)
            {
                if (checkpoint.Location.Equals(location))
                {
                    checkpoints.Add(checkpoint);
                }
            }
            return checkpoints;
        }

        public Checkpoint Create(Checkpoint newCheckpoint)
        {
            newCheckpoint.ID = GenerateId();
            _checkpoints.Add(newCheckpoint);
            _storage.Save(_checkpoints);
            NotifyObservers();
            return newCheckpoint;
        }

        public Checkpoint Update(Checkpoint newCheckpoint, int id)
        {
            Checkpoint currentCheckpoint = FindById(id);
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
            Checkpoint currentCheckpoint = FindById(id);
            _checkpoints.Remove(currentCheckpoint);
        }

        public List<Checkpoint> GetAll()
        {
            return _checkpoints;
        }

        public void LoadLocationsToCheckpoints(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Checkpoint checkpoint in _checkpoints)
                {
                    if (checkpoint.LocationID == location.Id)
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
