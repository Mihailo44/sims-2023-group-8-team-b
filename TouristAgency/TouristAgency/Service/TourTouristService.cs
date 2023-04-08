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

        public TourTourist FindByTouristID(int ID)
        {
            return _tourtourist.First(tt => tt.TouristID == ID);
        }

        public TourTourist FindByTourAndTouristID(int tourID, int touristID)
        {
            return _tourtourist.First(tt => tt.TourID == tourID && tt.TouristID == touristID);
        }

        public void Create(TourTourist tourTourist)
        {
            _tourtourist.Add(tourTourist);
            _storage.Save(_tourtourist);
            NotifyObservers();
        }

        public void Update(TourTourist tourTourist)
        {
            TourTourist newTourTourist = FindByTourAndTouristID(tourTourist.TourID, tourTourist.TouristID);
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

        public List<TourTourist> GetAll()
        {
            return _tourtourist;
        }

        public List<Tourist> GetArrivedTourist(int tourID, List<Tourist> tourists)
        {
            List<Tourist> arrivedTourists = new List<Tourist>();
            foreach(TourTourist tourTourist in _tourtourist)
            {
                foreach(Tourist tourist in tourists) 
                {
                    if(tourTourist.TourID == tourID && tourTourist.TouristID == tourist.ID && tourTourist.Arrived == true)
                    {
                        arrivedTourists.Add(tourist);
                    }
                }
            }

            return arrivedTourists;
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
