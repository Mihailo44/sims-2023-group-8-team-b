using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage;

namespace TouristAgency.Service
{
    public class TourTouristService : ISubject
    {
        private readonly TourTouristStorage _storage;
        private readonly List<TourTourist> _tourtourist;
        private List<IObserver> _observers;

        public TourTouristService()
        {
            _storage = new TourTouristStorage();
            _tourtourist = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Create(TourTourist tourTourist)
        {
            _tourtourist.Add(tourTourist);
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

        public List<TourTourist> GetAll()
        {
            return _tourtourist;
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
