using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.RenovationFeatures.Domain;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.ReservationFeatures.Domain
{
    public class ReservationService
    {
        private readonly App _app;
        public ReservationRepository ReservationRepository { get; }
        public RenovationService RenovationService { get; set; }

        public ReservationService()
        {
            _app = (App)App.Current;
            ReservationRepository = _app.ReservationRepository;
            RenovationService = new RenovationService();
        }

        public List<Reservation> GetAll()
        {
            return ReservationRepository.GetAll();
        }

        public Reservation Create(Reservation reservation)
        {
            return ReservationRepository.Create(reservation);
        }

        public ObservableCollection<Reservation> GeneratePotentionalReservations(DateTime start, int numOfDays, int numOfReservations, Accommodation accommodation, Guest guest)
        {
            ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
            DateTime startInterval = start;
            DateTime endInterval = start.AddDays(numOfDays - 1);

            for (int i = 0; i < numOfReservations; i++)
            {
                if (IsReserved(accommodation.Id, startInterval.AddDays(i), endInterval.AddDays(i)) ==
                    false && RenovationService.IsRenovating(accommodation.Id, startInterval.AddDays(i), endInterval.AddDays(i)) == false)
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
                    false && RenovationService.IsRenovating(accommodation.Id, startFirstInterval.AddDays(i), endFirstInterval.AddDays(i)) == false)
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
                    false && RenovationService.IsRenovating(accommodation.Id, startSecondInterval.AddDays(i), endSecondInterval.AddDays(i)) == false)
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
                if (reservation.Accommodation.Id == accommodationID && reservation.IsCanceled == true)
                {
                    return false;
                }
                if (reservation.Accommodation.Id == accommodationID && end.Date >= reservation.Start.Date &&
                    end.Date <= reservation.End.Date)
                {
                    return true;
                }
                else if (reservation.Accommodation.Id == accommodationID && start.Date >= reservation.Start.Date &&
                         end.Date <= reservation.End.Date)
                {
                    return true;
                }
                else if (reservation.Accommodation.Id == accommodationID && start.Date >= reservation.Start.Date &&
                         start.Date <= reservation.End.Date)
                {
                    return true;
                }
                else if (reservation.Accommodation.Id == accommodationID && start.Date <= reservation.Start.Date &&
                         end.Date >= reservation.End.Date)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Reservation> GetUnreviewed(int ownerId)
        {
            return ReservationRepository.GetAll().Where(r => r.Accommodation.Owner.ID == ownerId && r.Status == ReviewStatus.UNREVIEWED).ToList();
        }

        public List<Reservation> GetOwnerReviewed(int guestId)
        {
            return ReservationRepository.GetAll().FindAll(r => r.Guest.ID == guestId && r.OStatus == ReviewStatus.REVIEWED);
        }

        public List<Reservation> GetUnreviewedByGuestId(int guestId)
        {
            return ReservationRepository.GetAll().Where(r => r.Guest.ID == guestId && r.End <= DateTime.Now && r.End.AddDays(5) >= DateTime.Now && r.OStatus == ReviewStatus.UNREVIEWED).ToList();
        }

        public List<Reservation> GetByOwnerId(int id)
        {
            return ReservationRepository.GetAll().FindAll(r => r.Accommodation.Owner.ID == id);
        }

        public List<Reservation> GetByAccommodationId(int id)
        {
            return ReservationRepository.GetAll().FindAll(r => r.Accommodation.Id ==id);
        }

        public List<Reservation> GetByGuestId(int id)
        {
            return ReservationRepository.GetAll().FindAll(r => r.Guest.ID == id && r.Start >= DateTime.Now && r.IsCanceled == false);
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

        public List<Accommodation> GetAllFreeAccommodation(DateTime start, DateTime end, List<Accommodation> accommodations, int numOfDays, int numOfPeople, Guest guest, int numOfReservations)
        {
            List<Accommodation> freeAccommodations = new List<Accommodation>();

            foreach(Accommodation accommodation in accommodations)
            {
                if (!freeAccommodations.Contains(accommodation) && GeneratePotentionalReservations(start, numOfDays, numOfReservations, accommodation, guest).Count != 0 && accommodation.MinNumOfDays <= numOfDays && accommodation.MaxGuestNum >= numOfPeople)
                {
                    freeAccommodations.Add(accommodation);
                }
            }
            return freeAccommodations;
            //DateTime start, int numOfDays, int numOfReservations, Accommodation accommodation, Guest guest

        }

        public List<Reservation> SearchReservations(string searchInput)
        {
            List<Reservation> reservations = GetByOwnerId(_app.LoggedUser.ID);

            searchInput ??= "";

            if (searchInput.ToLower() == "unrwd")
                return reservations.FindAll(r => r.Status == ReviewStatus.UNREVIEWED && r.End <= DateTime.Today);
            else
                return reservations.FindAll(r => r.Guest.FirstName.ToLower().Contains(searchInput.ToLower()) || r.Guest.LastName.ToLower().Contains(searchInput.ToLower()) || r.Accommodation.Name.ToLower().Contains(searchInput.ToLower()));
        }
    }
}
