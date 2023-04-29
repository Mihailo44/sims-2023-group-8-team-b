using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;

namespace TouristAgency.Tours
{
    public class TourTouristCheckpointRepository : ISubject
    {
        private readonly IStorage<TourTouristCheckpoint> _storage;
        private readonly List<TourTouristCheckpoint> _tourtouristcheckpoints;
        private List<IObserver> _observers;

        public TourTouristCheckpointRepository(IStorage<TourTouristCheckpoint> storage)
        {
            _storage = storage;
            _tourtouristcheckpoints = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Create(TourTouristCheckpoint tourtouristcheckpoint)
        {
            _tourtouristcheckpoints.Add(tourtouristcheckpoint);
            _storage.Save(_tourtouristcheckpoints);
            NotifyObservers();
        }
        public List<TourTouristCheckpoint> GetAll()
        {
            return _tourtouristcheckpoints;
        }

        public TourTouristCheckpoint Update(TourTouristCheckpoint ttc, int touristID, int tourID)
        {
            TourTouristCheckpoint currentttc = GetAll().First(t => t.TouristID == touristID && t.TourCheckpoint.TourID == tourID);
            currentttc.TourCheckpoint = ttc.TourCheckpoint;
            currentttc.TouristID = ttc.TouristID;
            currentttc.InvitationStatus = ttc.InvitationStatus;
            _storage.Save(_tourtouristcheckpoints);
            NotifyObservers();

            return currentttc;
        }

        public void Delete(int touristID)
        {
            TourTouristCheckpoint deletedTourTouristCheckpoint = _tourtouristcheckpoints.Find(t => t.TouristID == touristID);
            _tourtouristcheckpoints.Remove(deletedTourTouristCheckpoint);
            _storage.Save(_tourtouristcheckpoints);
            NotifyObservers();
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
