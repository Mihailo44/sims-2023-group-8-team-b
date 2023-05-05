using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;

namespace TouristAgency.Tours
{
    public class TourTouristRepository : ISubject
    {
        private readonly IStorage<TourTourist> _storage;
        private readonly List<TourTourist> _tourtourist;
        private List<IObserver> _observers;

        public TourTouristRepository(IStorage<TourTourist> storage)
        {
            _storage = storage;
            _tourtourist = _storage.Load();
            _observers = new List<IObserver>();
        }
        public TourTourist Create(TourTourist tourTourist)
        {
            _tourtourist.Add(tourTourist);
            _storage.Save(_tourtourist);
            NotifyObservers();

            return tourTourist;
        }
        public TourTourist GetByTourAndTouristID(int tourID, int touristID)
        {
            return _tourtourist.First(tt => tt.TourID == tourID && tt.TouristID == touristID);
        }

        public List<TourTourist> GetAll()
        {
            return _tourtourist;
        }

        public void Update(TourTourist tourTourist)
        {
            TourTourist newTourTourist = GetByTourAndTouristID(tourTourist.TourID, tourTourist.TouristID);
            newTourTourist.Arrived = tourTourist.Arrived;
            _storage.Save(_tourtourist);
            NotifyObservers();
        }

        public void Delete(int touristID)
        {
            TourTourist deletedTourTourist = _tourtourist.Find(t => t.TouristID == touristID);
            _tourtourist.Remove(deletedTourTourist);
            _storage.Save(_tourtourist);
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
