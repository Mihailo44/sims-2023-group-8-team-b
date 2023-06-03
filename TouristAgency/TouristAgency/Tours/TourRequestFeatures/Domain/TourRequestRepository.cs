using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.TourRequests
{
    public class TourRequestRepository : ICrud<TourRequest>, ISubject
    {
        private readonly IStorage<TourRequest> _storage;
        private readonly List<TourRequest> _requests;
        private List<IObserver> _observers;

        public TourRequestRepository(IStorage<TourRequest> storage)
        {
            _storage = storage;
            _requests = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if (_requests.Count == 0)
            {
                return 0;
            }
            return _requests.Max(t => t.ID) + 1;
        }

        public TourRequest Create(TourRequest newTourRequest)
        {
            newTourRequest.ID = GenerateId();
            _requests.Add(newTourRequest);
            _storage.Save(_requests);
            NotifyObservers();
            return newTourRequest;
        }

        public TourRequest GetById(int id)
        {
            return _requests.Find(r => r.ID == id);
        }

        public List<TourRequest> GetAll()
        {
            return _requests;
        }

        public TourRequest Update(TourRequest newTourRequest, int id)
        {
            TourRequest currentTourRequest = GetById(id);
            if (currentTourRequest == null)
            {
                return null;
            }
            currentTourRequest.Status = newTourRequest.Status;
            currentTourRequest.TouristID = newTourRequest.TouristID;
            currentTourRequest.GuideID = newTourRequest.GuideID;
            currentTourRequest.ShortLocation = newTourRequest.ShortLocation;
            currentTourRequest.Description = newTourRequest.Description;
            currentTourRequest.Language = newTourRequest.Language;
            currentTourRequest.MaxAttendants = newTourRequest.MaxAttendants;
            currentTourRequest.StartDate = newTourRequest.StartDate;
            currentTourRequest.EndDate = newTourRequest.EndDate;
            _storage.Save(_requests);
            NotifyObservers();
            return currentTourRequest;
        }

        public void Delete(int id)
        {
            TourRequest deletedTourRequest = GetById(id);
            _requests.Remove(deletedTourRequest);
            _storage.Save(_requests);
            NotifyObservers();
        }

        public void LoadLocationsToTourRequests(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (TourRequest request in _requests)
                {
                    if (request.ShortLocationID == location.ID)
                    {
                        request.ShortLocation = new Location(location);
                    }
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
