using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.ReservationFeatures.Domain
{
    public class ReservationService
    {
        private readonly App _app;
        public ReservationRepository ReservationRepository { get; }

        public ReservationService()
        {
            _app = (App)App.Current;
            ReservationRepository = _app.ReservationRepository;
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
            foreach (Reservation reservation in ReservationRepository.GetAll())
            {
                if (reservation.AccommodationId == accommodationID && reservation.IsCanceled == true)
                {
                    return false;
                }
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

        public List<Reservation> GetUnreviewed(int ownerId)
        {
            return ReservationRepository.GetAll().Where(r => r.Accommodation.OwnerId == ownerId && r.Status == ReviewStatus.UNREVIEWED).ToList();
        }

        public List<Reservation> GetOwnerReviewed(int guestId)
        {
            return ReservationRepository.GetAll().FindAll(r => r.GuestId == guestId && r.OStatus == ReviewStatus.REVIEWED);
        }

        public List<Reservation> GetUnreviewedByGuestId(int guestId)
        {
            return ReservationRepository.GetAll().Where(r => r.GuestId == guestId && r.End <= DateTime.Now && r.End.AddDays(5) >= DateTime.Now && r.OStatus == ReviewStatus.UNREVIEWED).ToList();
        }

        public List<Reservation> GetByOwnerId(int id)
        {
            return ReservationRepository.GetAll().FindAll(r => r.Accommodation.OwnerId == id);
        }

        public List<Reservation> GetByAccommodationId(int id)
        {
            return ReservationRepository.GetAll().FindAll(r => r.AccommodationId ==id);
        }

        public List<Reservation> GetByGuestId(int id)
        {
            return ReservationRepository.GetAll().FindAll(r => r.GuestId == id && r.Start >= DateTime.Now && r.IsCanceled == false);
        }

        public void ExpiredReservationsCheck(int ownerId)
        {
            DateTime today = DateTime.UtcNow.Date;
            double dateDiff;

            foreach (var reservation in GetUnreviewed(ownerId))
            {
                dateDiff = (today - reservation.End).TotalDays;

                if (dateDiff > 5.0)
                {
                    reservation.Status = ReviewStatus.EXPIRED;
                    ReservationRepository.Update(reservation, reservation.Id);
                }
            }
        }

        public bool CancelReservation(Reservation reservation)
        {
            int numOfDays = Math.Min(-1, -reservation.Accommodation.AllowedNumOfDaysForCancelation);
            DateTime limit = reservation.Start.AddDays(numOfDays);

            if (DateTime.Now <= limit)
            {
                reservation.IsCanceled = true;
                ReservationRepository.Update(reservation, reservation.Id);
                return true;
            }

            return false;
        }
    }
}
