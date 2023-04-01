using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage;

namespace TouristAgency.Service
{
    public class ReservationService : ICrud<Reservation>, ISubject
    {
        private readonly ReservationStorage _storage;
        private readonly List<Reservation> _reservations;
        private List<IObserver> _observers;

        public ReservationService()
        {
            _storage = new ReservationStorage();
            _reservations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _reservations.Count == 0 ? 0 : _reservations.Max(r => r.Id) + 1;
        }

        public Reservation FindById(int id)
        {
            return _reservations.Find(r => r.Id == id);
        }

        public ObservableCollection<Reservation> GeneratePotentionalReservations(DateTime start, int numOfDays, int numOfReservations, Accommodation accommodation, Guest guest)
        {
            ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
            DateTime startInterval = start;
            DateTime endInterval = start.AddDays(numOfDays - 1);

            for (int i = 0; i < numOfReservations; i++)
            {
                if (IsReserved(accommodation.Id, startInterval.AddDays(i), endInterval.AddDays(i)) ==
                    false)
                {
                    reservations.Add(new Reservation(guest, accommodation, startInterval.AddDays(i), endInterval.AddDays(i)));
                }

            }

            return reservations;
        }

        public ObservableCollection<Reservation> GenerateAlternativeReservations(DateTime start, int numOfDays, int numOfReservations, Accommodation accommodation, Guest guest)
        {
            ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
            //DateTime startFirstInterval = start.AddMonths(1);
            //DateTime startSecondInterval = start.AddMonths(-1);
            // DateTime endFirstInterval = startFirstInterval.AddDays(numOfDays - 1);
            //DateTime endSecondInterval = startSecondInterval.AddDays(numOfDays - 1);

            /*for (int i = 0; i < numOfReservations; i++)
            {
                if (IsReserved(accommodation.Id, startSecondInterval.AddDays(i), endSecondInterval.AddDays(i)) ==
                    false)
                {
                    reservations.Add(new Reservation(guest, accommodation, startSecondInterval.AddDays(i), endSecondInterval.AddDays(i)));
                }
            }*/

            /*for (int i = 0; i < numOfReservations; i++)
            {
                if (IsReserved(accommodation.Id, startFirstInterval.AddDays(i), endFirstInterval.AddDays(i)) ==
                    false)
                {
                    reservations.Add(new Reservation(guest, accommodation, startFirstInterval.AddDays(i), endFirstInterval.AddDays(i)));
                }

            }*/

            foreach (Reservation reservation in GenerateSecondInterval(start, numOfDays, numOfReservations, accommodation, guest))
            {
                reservations.Add(reservation);
            }

            foreach (Reservation reservation in GenerateFirstInterval(start, numOfDays, numOfReservations, accommodation, guest))
            {
                reservations.Add(reservation);
            }

            return reservations;
        }

        public ObservableCollection<Reservation> GenerateFirstInterval(DateTime start, int numOfDays,
            int numOfReservations, Accommodation accommodation, Guest guest)
        {
            ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
            DateTime startFirstInterval = start.AddMonths(1);
            DateTime endFirstInterval = startFirstInterval.AddDays(numOfDays - 1);

            for (int i = 0; i < numOfReservations; i++)
            {
                if (IsReserved(accommodation.Id, startFirstInterval.AddDays(i), endFirstInterval.AddDays(i)) ==
                    false)
                {
                    reservations.Add(new Reservation(guest, accommodation, startFirstInterval.AddDays(i), endFirstInterval.AddDays(i)));
                }

            }
            return reservations;
        }

        public ObservableCollection<Reservation> GenerateSecondInterval(DateTime start, int numOfDays,
            int numOfReservations, Accommodation accommodation, Guest guest)
        {
            ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
            DateTime startSecondInterval = start.AddMonths(-1);
            DateTime endSecondInterval = startSecondInterval.AddDays(numOfDays - 1);

            for (int i = 0; i < numOfReservations; i++)
            {
                if (IsReserved(accommodation.Id, startSecondInterval.AddDays(i), endSecondInterval.AddDays(i)) ==
                    false)
                {
                    reservations.Add(new Reservation(guest, accommodation, startSecondInterval.AddDays(i), endSecondInterval.AddDays(i)));
                }
            }

            return reservations;
        }

        public bool IsReserved(int accommodationID, DateTime start, DateTime end)
        {
            foreach (Reservation reservation in _reservations)
            {
                if (reservation.AccommodationId == accommodationID && end.Date >= reservation.Start.Date &&
                    end.Date <= reservation.End.Date)
                {
                    return true;
                }
                else if (reservation.AccommodationId == accommodationID && start.Date >= reservation.Start.Date &&
                         end.Date <= reservation.End.Date)
                {
                    return true;
                }
                else if (reservation.AccommodationId == accommodationID && start.Date >= reservation.Start.Date &&
                         start.Date <= reservation.End.Date)
                {
                    return true;
                }
                else if (reservation.AccommodationId == accommodationID && start.Date <= reservation.Start.Date &&
                         end.Date >= reservation.End.Date)
                {
                    return true;
                }
            }

            return false;
        }

        public Reservation Create(Reservation newReservation)
        {
            newReservation.Id = GenerateId();
            _reservations.Add(newReservation);
            _storage.Save(_reservations);
            NotifyObservers();

            return newReservation;
        }

        public Reservation Update(Reservation updatedReservation, int id)
        {
            Reservation currentReservation = FindById(id);
            if (currentReservation == null)
                return null;

            //currentReservation.GuestId = newReservation.GuestId;
            //currentReservation.Guest = newReservation.Guest;
            currentReservation.Start = updatedReservation.Start;
            currentReservation.End = updatedReservation.End;
            currentReservation.Status = updatedReservation.Status;
            currentReservation.Postponed = updatedReservation.Postponed;

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

        public List<Reservation> GetUnreviewed(int ownerId)
        {
            return _reservations.Where(r => r.Accommodation.OwnerId == ownerId && r.Status == REVIEW_STATUS.UNREVIEWED).ToList();
        }

        public List<Reservation> GetByOwnerId(int id)
        {
            return _reservations.FindAll(r => r.Accommodation.OwnerId == id);
        }

        public string ReviewNotification(int ownerId, out int changes)
        {
            DateTime today = DateTime.UtcNow.Date;

            string notification = "Unreviewed guests:\n";
            double dateDiff;
            changes = 0;

            foreach (var reservation in GetUnreviewed(ownerId))
            {
                dateDiff = (today - reservation.End).TotalDays;

                if (today > reservation.End && dateDiff < 5.0)
                {
                    notification += $"{reservation.Guest.FirstName} {reservation.Guest.LastName} {dateDiff} days left\n";
                    changes++;
                }
                else
                {
                    reservation.Status = REVIEW_STATUS.EXPIRED;
                    Update(reservation, reservation.Id);
                }
            }

            return notification;

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
