using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    internal class LocationDAO : ICrud<Location>, ISubject
    {
        private readonly LocationStorage _storage;
        private readonly List<Location> _locations;
        private List<IObserver> _observers;

        public LocationDAO()
        {
            _storage = new LocationStorage();
            _locations = _storage.Load();
            _observers = new List<IObserver>();
        }
       
        public int GenerateId()
        {
            return _locations.Max(l => l.Id) + 1;
        }

        public Location FindById(int id)
        {
            return _locations.Find(l => l.Id == id);
        }

        public Location Create(Location newLocation)
        {
            newLocation.Id = GenerateId();
            _locations.Add(newLocation);
            _storage.Save(_locations);
            NotifyObservers();

            return newLocation;
        }

        public Location Update(Location newLocation,int id)
        {
            Location currentLocation = FindById(id);
            if (currentLocation == null)
                return null;

            //currentLocation.City = newLocation.City;
            _storage.Save(_locations);
            NotifyObservers();

            return newLocation;
        }

        public Location Delete(int id)
        {
            Location location = FindById(id);
            if (location == null)
                return null;

            _locations.Remove(location);
            _storage.Save(_locations);
            NotifyObservers();

            return location;
        }

        public List<Location> GetAll()
        {
            return _locations;
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
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
