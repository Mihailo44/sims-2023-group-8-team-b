using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Storage;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using TouristAgency.Controller;

namespace TouristAgency.Model.DAO
{
    internal class ReservationDAO : ICrud<Reservation>,ISubject
    {
        private readonly ReservationStorage _storage;
        private readonly List<Reservation> _reservations;
        private List<IObserver> _observers;

        public ReservationDAO()
        {
            _storage = new ReservationStorage();
            _reservations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return (_reservations.Count == 0) ? 0: _reservations.Max(r => r.Id) + 1;
        }

        public Reservation FindById(int id)
        {
            return _reservations.Find(r => r.Id == id);
        }

        public Reservation Create(Reservation newReservation)
        {
            newReservation.Id = GenerateId();
            _reservations.Add(newReservation);
            _storage.Save(_reservations);
            NotifyObservers();

            return newReservation;
        }

        public Reservation Update(Reservation updatedReservation,int id)
        {
            Reservation currentReservation = FindById(id);
            if (currentReservation == null)
                return null;

            //currentReservation.GuestId = newReservation.GuestId;
            //currentReservation.Guest = newReservation.Guest;
            currentReservation.AccommodationId = updatedReservation.AccommodationId;
            currentReservation.Accommodation = updatedReservation.Accommodation;
            currentReservation.Start = updatedReservation.Start;
            currentReservation.End = updatedReservation.End;

            _storage.Save(_reservations);
            NotifyObservers();

            return currentReservation;
        }

        public void Delete(int id)
        {
            Reservation reservation = FindById(id);
            _reservations.Remove(reservation);
            _storage.Save(_reservations);
            NotifyObservers();
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public void LoadAccommodationsToReservations(List<Accommodation> accommodations)
        {
            foreach (var reservation in _reservations)
            {
                Accommodation accommodation = accommodations.Find(a => a.Id == reservation.AccommodationId);
                if (accommodation != null)
                {
                    reservation.Accommodation = accommodation;
                }
            }
        }

        public void LoadGuestsToReservations(List<Guest> guests)
        {
            foreach (var reservation in _reservations)
            {
                Guest guest = guests.Find(g => g.ID == reservation.GuestId);
                if (guest != null)
                {
                    reservation.Guest = guest;
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
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
