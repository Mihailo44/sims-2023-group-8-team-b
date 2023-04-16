using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Storage;
using TouristAgency.Interfaces;

namespace TouristAgency.Service
{
    public class AccommodationService : ICrud<Accommodation>, ISubject
    {
        private readonly AccommodationStorage _storage;
        private readonly List<Accommodation> _accommodations;
        private List<IObserver> _observers;

        public AccommodationService()
        {
            _storage = new AccommodationStorage();
            _accommodations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if (_accommodations.Count == 0)
                return 0;
            else
                return _accommodations.Max(a => a.Id) + 1;
        }

        public Accommodation FindById(int id)
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
            Accommodation currentAccommodation = FindById(id);
            if (currentAccommodation == null)
                return null;

            currentAccommodation.OwnerId = updatedAccommodation.OwnerId;
            currentAccommodation.Owner = updatedAccommodation.Owner;
            currentAccommodation.Name = updatedAccommodation.Name;
            currentAccommodation.MaxGuestNum = updatedAccommodation.MaxGuestNum;
            currentAccommodation.MinNumOfDays = updatedAccommodation.MinNumOfDays;
            currentAccommodation.AllowedNumOfDaysForCancelation = updatedAccommodation.AllowedNumOfDaysForCancelation;

            _storage.Save(_accommodations);
            NotifyObservers();

            return currentAccommodation;

        }

        public void Delete(int id)
        {
            Accommodation accommodation = FindById(id);
            _accommodations.Remove(accommodation);
            _storage.Save(_accommodations);
            NotifyObservers();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            names.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!names.Contains(accommodation.Name) && accommodation.Name != "")
                {
                    names.Add(accommodation.Name);
                }
            }

            return names;
        }

        public List<string> GetCities()
        {
            List<string> citites = new List<string>();
            citites.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!citites.Contains(accommodation.Location.City) && accommodation.Location.City != "")
                {
                    citites.Add(accommodation.Location.City);
                }
            }

            return citites;
        }

        public List<string> GetCountries()
        {
            List<string> countries = new List<string>();
            countries.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!countries.Contains(accommodation.Location.Country) && accommodation.Location.Country != "")
                {
                    countries.Add(accommodation.Location.Country);
                }
            }

            return countries;
        }

        public List<string> GetTypes()
        {
            List<string> types = new List<string>();
            types.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!types.Contains(accommodation.Type.ToString()) && accommodation.Type.ToString() != "")
                {
                    types.Add(accommodation.Type.ToString());
                }
            }

            return types;
        }

        public void LoadLocationsToAccommodations(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.LocationId == location.Id)
                    {
                        accommodation.Location = new Location(location);
                    }
                }
            }
        }

        public void LoadPhotosToAccommodations(List<Photo> photos)
        {
            foreach (Photo photo in photos)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.Id == photo.ExternalID && photo.Type == 'A')
                    {
                        accommodation.Photos.Add(new Photo(photo));
                    }
                }
            }
        }

        public void LoadOwnersToAccommodations(List<Owner> owners)
        {
            foreach (Owner owner in owners)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.OwnerId == owner.ID)
                    {
                        accommodation.Owner = owner;
                    }
                }
            }
        }

        public List<Accommodation> GetByOwnerId(int id = 0)
        {
            return _accommodations.FindAll(a => a.OwnerId == id);
        }

        public List<Accommodation> Search(string country, string city, string name, string type, int maxGuest, int minDays)
        {

            return _accommodations.Where(a => a.Location.Country.Contains(country) && a.Location.City.Contains(city) && a.Name.Contains(name) && a.Type.ToString().Contains(type) && a.MaxGuestNum >= maxGuest && a.MinNumOfDays <= minDays).ToList();
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
