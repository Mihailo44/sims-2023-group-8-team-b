using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Users
{
    public class GuestRepository : ICrud<Guest>, ISubject
    {
        private readonly IStorage<Guest> _storage;
        private readonly List<Guest> _guests;
        private List<IObserver> _observers;

        public GuestRepository(IStorage<Guest> storage)
        {
            _storage = storage;
            _guests = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _guests.Max(g => g.ID) + 1;
        }

        public Guest GetById(int id)
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
            Guest currentGuest = GetById(id);

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
            Guest deletedGuest = GetById(id);
            _guests.Remove(deletedGuest);
            _storage.Save(_guests);
            NotifyObservers();
        }

        public List<Guest> GetAll()
        {
            return _guests;
        }

        public void LoadReservationsToGuests(List<Reservation> reservations)
        {
            foreach (Reservation reservation in reservations)
            {
                foreach(Guest guest in GetAll())
                {
                    if(guest.ID == reservation.Guest.ID)
                    {
                        guest.Reservations.Add(reservation);
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
