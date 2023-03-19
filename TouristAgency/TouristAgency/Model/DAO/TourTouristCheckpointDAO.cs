using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class TourTouristCheckpointDAO : ISubject
    {
        private readonly TourTouristCheckpointStorage _storage;
        private readonly List<TourTouristCheckpoint> _tourtouristcheckpoint;
        private List<IObserver> _observers;

        public TourTouristCheckpointDAO()
        {
            _storage = new TourTouristCheckpointStorage();
            _tourtouristcheckpoint = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Create(TourTouristCheckpoint tourtouristcheckpoint)
        {
            _tourtouristcheckpoint.Add(tourtouristcheckpoint);
            _storage.Save(_tourtouristcheckpoint);
            NotifyObservers();
        }

        public void Delete(int touristID)
        {
            TourTouristCheckpoint deletedTourTouristCheckpoint = _tourtouristcheckpoint.Find(t => t.TouristID == touristID);
            _tourtouristcheckpoint.Remove(deletedTourTouristCheckpoint);
            _storage.Save(_tourtouristcheckpoint);
            NotifyObservers();
        }

        public List<TourTouristCheckpoint> GetAll()
        {
            return _tourtouristcheckpoint;
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
