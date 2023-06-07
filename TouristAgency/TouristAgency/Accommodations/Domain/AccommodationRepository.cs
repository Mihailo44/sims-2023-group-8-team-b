using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.Domain
{
    public class AccommodationRepository : ICrud<Accommodation>, ISubject
    {
        private readonly IStorage<Accommodation> _storage;
        private readonly List<Accommodation> _accommodations;
        private List<IObserver> _observers;

        public AccommodationRepository(IStorage<Accommodation> storage)
        {
            _storage = storage;
            _accommodations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _accommodations.Count == 0 ? 0 : _accommodations.Max(a => a.Id) + 1;
        }

        public Accommodation GetById(int id)
        {
            return _accommodations.Find(a => a.Id == id);
        }

        public Accommodation Create(Accommodation newAccommodation)
        {
            newAccommodation.Id = GenerateId();
            _accommodations.Add(newAccommodation);
            _storage.Save(_accommodations);
            NotifyObservers();

            return newAccommodation;
        }

        public Accommodation Update(Accommodation updatedAccommodation, int id)
        {
            Accommodation currentAccommodation = GetById(id);
            if (currentAccommodation == null)
                return null;

            currentAccommodation.Owner.ID = updatedAccommodation.Owner.ID;
            currentAccommodation.Owner = updatedAccommodation.Owner;
            currentAccommodation.Name = updatedAccommodation.Name;
            currentAccommodation.MaxGuestNum = updatedAccommodation.MaxGuestNum;
            currentAccommodation.MinNumOfDays = updatedAccommodation.MinNumOfDays;
            currentAccommodation.AllowedNumOfDaysForCancelation = updatedAccommodation.AllowedNumOfDaysForCancelation;
            currentAccommodation.RecentlyRenovated = updatedAccommodation.RecentlyRenovated;
            currentAccommodation.HotLocation = updatedAccommodation.HotLocation;

            _storage.Save(_accommodations);
            NotifyObservers();

            return currentAccommodation;
        }

        public void Delete(int id)
        {
            Accommodation accommodation = GetById(id);
            _accommodations.Remove(accommodation);
            _storage.Save(_accommodations);
            NotifyObservers();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public void LoadLocationsToAccommodations(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.Location.ID == location.ID)
                    {
                        accommodation.Location = location;
                    }
                }
            }
        }

        public void LoadPhotosToAccommodations(List<Photo> photos)
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                List<Photo> photo = photos.FindAll(p => p.ExternalID == accommodation.Id && p.Type == 'A');
                if (photo != null)
                {
                    accommodation.Photos.AddRange(photo);
                }
            }
        }

        public void LoadOwnersToAccommodations(List<Owner> owners)
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                Owner owner = owners.Find(o => o.ID == accommodation.Owner.ID);
                if (owner != null)
                {
                    accommodation.Owner = owner;
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
