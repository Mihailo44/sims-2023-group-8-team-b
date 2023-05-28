using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.RenovationFeatures.Domain
{
    public class RenovationRepository : ICrud<Renovation>, ISubject
    {
        private readonly IStorage<Renovation> _storage;
        private readonly List<Renovation> _renovations;
        private List<IObserver> _observers;

        public RenovationRepository(IStorage<Renovation> storage)
        {
            _storage = storage;
            _renovations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _renovations.Count == 0 ? 0 : _renovations.Max(r => r.Id) + 1;
        }

        public Renovation GetById(int id)
        {
            return _renovations.Find(r => r.Id == id); //zastita da ne bude null mozda
        }

        public Renovation Create(Renovation newRenovation)
        {
            newRenovation.Id = GenerateId();
            _renovations.Add(newRenovation);
            _storage.Save(_renovations);
            NotifyObservers();

            return newRenovation;
        }

        public Renovation Update(Renovation updatedRenovation, int id)
        {
            Renovation currentRenovation = GetById(id);

            if (currentRenovation == null)
                return null;

            currentRenovation.Start = updatedRenovation.Start;
            currentRenovation.End = updatedRenovation.End;
            currentRenovation.Description = updatedRenovation.Description;
            currentRenovation.IsCanceled = updatedRenovation.IsCanceled;

            _storage.Save(_renovations);
            NotifyObservers();

            return currentRenovation;
        }

        public void Delete(int id)
        {
            Renovation renovation = GetById(id);
            if (renovation != null)
            {
                _renovations.Remove(renovation);
                _storage.Save(_renovations);
                NotifyObservers();
            }
        }

        public List<Renovation> GetAll()
        {
            return _renovations;
        }

        public void LoadAccommodationsToRenovations(List<Accommodation> accommodations)
        {
            foreach (var renovation in _renovations)
            {
                Accommodation accommodation = accommodations.Find(a => a.Id == renovation.AccommodationId);
                if (accommodation != null)
                {
                    renovation.Accommodation = accommodation;
                    renovation.AccommodationId = accommodation.Id;
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
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
