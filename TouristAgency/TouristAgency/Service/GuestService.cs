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
    public class GuestService : ICrud<Guest>, ISubject
    {
        private readonly GuestStorage _storage;
        private readonly List<Guest> _guests;
        private List<IObserver> _observers;

        public GuestService()
        {
            _storage = new GuestStorage();
            _guests = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _guests.Max(g => g.ID) + 1;
        }

        public Guest FindById(int id)
        {
            return _guests.Find(g => g.ID == id);
        }

        public Guest Create(Guest newGuest)
        {
            newGuest.ID = GenerateId();
            _guests.Add(newGuest);
            _storage.Save(_guests);
            NotifyObservers();

            return newGuest;
        }

        public Guest Update(Guest newGuest, int id)
        {
            Guest currentGuest = FindById(id);

            if (currentGuest == null)
            {
                return null;
            }

            currentGuest.Username = newGuest.Username;
            currentGuest.Password = newGuest.Password;
            currentGuest.FirstName = newGuest.FirstName;
            currentGuest.LastName = newGuest.LastName;
            currentGuest.DateOfBirth = newGuest.DateOfBirth;
            currentGuest.Email = newGuest.Email;
            currentGuest.FullLocation = newGuest.FullLocation;
            currentGuest.Phone = newGuest.Phone;

            return currentGuest;
        }

        public void Delete(int id)
        {
            Guest deletedGuest = FindById(id);
            _guests.Remove(deletedGuest);
            _storage.Save(_guests);
            NotifyObservers();
        }

        public List<Guest> GetAll()
        {
            return _guests;
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
