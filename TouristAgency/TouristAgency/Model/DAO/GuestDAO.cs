using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class GuestDAO : ICrud, ISubject
    {
        private readonly GuestStorage _storage;
        private readonly List<Guest> _guests;
        private List<IObserver> _observers;

        public GuestDAO()
        {
            _storage = new GuestStorage();
            _guests = new List<Guest>();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _guests.Max(g => g.ID);
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

        public Guest Delete(int id)
        {
            Guest deletedGuest = FindById(id);

            if (deletedGuest == null)
            {
                return null;
            }

            _guests.Remove(deletedGuest);
            _storage.Save(_guests);
            NotifyObservers();

            return deletedGuest; // VOID ??
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
