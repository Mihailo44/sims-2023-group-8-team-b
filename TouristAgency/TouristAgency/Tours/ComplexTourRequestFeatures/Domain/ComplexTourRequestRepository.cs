using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.Domain
{
    public class ComplexTourRequestRepository : ICrud<ComplexTourRequest>, ISubject
    {
        private readonly IStorage<ComplexTourRequest> _storage;
        private readonly List<ComplexTourRequest> _requests;
        private List<IObserver> _observers;

        public ComplexTourRequestRepository(IStorage<ComplexTourRequest> storage)
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

        public ComplexTourRequest Create(ComplexTourRequest newComplexTourRequest)
        {
            newComplexTourRequest.ID = GenerateId();
            _requests.Add(newComplexTourRequest);
            _storage.Save(_requests);
            NotifyObservers();
            return newComplexTourRequest;
        }

        public ComplexTourRequest GetById(int id)
        {
            return _requests.Find(r => r.ID == id);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _requests;
        }

        public ComplexTourRequest Update(ComplexTourRequest newComplexTourRequest, int id)
        {
            ComplexTourRequest currentComplexTourRequest = GetById(id);
            if (currentComplexTourRequest == null)
            {
                return null;
            }
            currentComplexTourRequest.Name = newComplexTourRequest.Name;
            currentComplexTourRequest.Components = newComplexTourRequest.Components;
            _storage.Save(_requests);
            NotifyObservers();
            return currentComplexTourRequest;
        }

        public void Delete(int id)
        {
            ComplexTourRequest deletedComplexTourRequest = GetById(id);
            _requests.Remove(deletedComplexTourRequest);
            _storage.Save(_requests);
            NotifyObservers();
        }

        public void LoadTourRequestsToComplexTourRequests(List<TourRequest> requests)
        {
            foreach (TourRequest request in requests)
            {
                foreach (ComplexTourRequest complexRequest in GetAll())
                {
                    if (request.ComplexTourRequestID == complexRequest.Id)
                    {
                        complexRequest.Components.Add(request);
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
