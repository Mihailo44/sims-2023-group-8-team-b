using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage.FileStorage;

namespace TouristAgency.Service
{
    public class LocationService : ICrud<Location>, ISubject
    {
        private readonly IStorage<Location> _storage;
        private readonly List<Location> _locations;
        private List<IObserver> _observers;

        public LocationService(IStorage<Location> storage)
        {
            _storage = storage;
            _locations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if (_locations.Count == 0)
                return 0;
            else
                return _locations.Max(l => l.Id) + 1;
        }

        public Location FindById(int id)
        {
            return _locations.Find(l => l.Id == id);
        }

        public int FindLocationId(Location location)
        {
            return _locations.Find(l => l.Equals(location)).Id;
        }

        public Location Create(Location newLocation)
        {
            newLocation.Id = GenerateId();
            _locations.Add(newLocation);
            _storage.Save(_locations);
            NotifyObservers();

            return newLocation;
        }

        public Location Update(Location newLocation, int id)
        {
            Location currentLocation = FindById(id);
            if (currentLocation == null)
                return null;

            _storage.Save(_locations);
            NotifyObservers();

            return newLocation;
        }

        public void Delete(int id)
        {
            Location location = FindById(id);
            _locations.Remove(location);
            _storage.Save(_locations);
            NotifyObservers();
        }

        public List<Location> GetAll()
        {
            return _locations;
        }

        public Location FindByCountryAndCity(string country, string city)
        {
            Location location = _locations.Find(l => l.Country.ToLower() == country.ToLower() && l.City.ToLower() == city.ToLower());
            if (location == null)
            {
                location = new Location(country, city);
                Create(location);
            }

            return location;
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
