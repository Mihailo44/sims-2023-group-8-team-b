using System.Collections.Generic;
using System.Linq;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Interfaces;
using TouristAgency.Users;

namespace TouristAgency.Accommodations.ReservationFeatures.Domain
{
    public class ReservationRepository : ICrud<Reservation>, ISubject
    {
        private readonly IStorage<Reservation> _storage;
        private readonly List<Reservation> _reservations;
        private List<IObserver> _observers;

        public ReservationRepository(IStorage<Reservation> storage)
        {
            _storage = storage;
            _reservations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _reservations.Count == 0 ? 0 : _reservations.Max(r => r.Id) + 1;
        }

        public Reservation GetById(int id)
        {
            return _reservations.Find(r => r.Id == id);
        }

        public Reservation Create(Reservation newReservation)
        {

            newReservation.Id = GenerateId();
            newReservation.IsCanceled = false;
            _reservations.Add(newReservation);
            _storage.Save(_reservations);
            NotifyObservers();

            return newReservation;

        }

        public Reservation Update(Reservation updatedReservation, int id)
        {
            Reservation currentReservation = GetById(id);
            if (currentReservation == null)
                return null;

            currentReservation.Accommodation = updatedReservation.Accommodation;
            currentReservation.Accommodation.Id = updatedReservation.Accommodation.Id;
            currentReservation.Start = updatedReservation.Start;
            currentReservation.End = updatedReservation.End;
            currentReservation.Status = updatedReservation.Status;
            currentReservation.OStatus = updatedReservation.OStatus;

            currentReservation.IsCanceled = updatedReservation.IsCanceled;

            _storage.Save(_reservations);
            NotifyObservers();

            return currentReservation;
        }

        public void Delete(int id)
        {
            Reservation reservation = GetById(id);
            _reservations.Remove(reservation);
            _storage.Save(_reservations);
            NotifyObservers();
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public void LoadGuestsToReservations(List<Guest> guests)
        {
            foreach (var reservation in _reservations)
            {
                Guest guest = guests.Find(g => g.ID == reservation.Guest.ID);
                if (guest != null)
                {
                    reservation.Guest = guest;
                }
            }
        }

        public void LoadAccommodationsToReservations(List<Accommodation> accommodations)
        {
            foreach (var reservation in _reservations)
            {
                Accommodation accommodation = accommodations.Find(a => a.Id == reservation.Accommodation.Id);
                if (accommodation != null)
                {
                    reservation.Accommodation = accommodation;
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
