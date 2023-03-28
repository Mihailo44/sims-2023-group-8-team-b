using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using TouristAgency.Service;
using TouristAgency.Base;
using System.Collections.ObjectModel;

namespace TouristAgency.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly ReservationService _reservation;

        public ReservationViewModel()
        {
            _reservation = new ReservationService();
        }

        public List<Reservation> GetAll()
        {
            return _reservation.GetAll();
        }

        public void Create(Reservation newReservation)
        {
            _reservation.Create(newReservation);
        }

        public void Update(Reservation updatedReservation, int id)
        {
            _reservation.Update(updatedReservation, id);
        }

        public void Delete(Reservation reservation)
        {
            _reservation.Delete(reservation.Id);
        }

        public void LoadAccommodationsToReservations(List<Accommodation> accommodations)
        {
            _reservation.LoadAccommodationsToReservations(accommodations);
        }

        public void LoadGuestsToReservations(List<Guest> guests)
        {
            _reservation.LoadGuestsToReservations(guests);
        }

        public List<Reservation> GetUnreviewed(int ownerId = 0)
        {
            return _reservation.GetUnreviewed(ownerId);
        }

        public List<Reservation> GetByOwnerId(int id)
        {
            return _reservation.GetByOwnerId(id);
        }

        public ObservableCollection<Reservation> GeneratePotentionalReservations(DateTime start, int numOfDays,
            int numOfReservations, Accommodation accommodation, Guest guest)
        {
            return _reservation.GeneratePotentionalReservations(start, numOfDays, numOfReservations, accommodation,
                guest);
        }

        public ObservableCollection<Reservation> GenerateAlternativeReservations(DateTime start, int numOfDays,
            int numOfReservations, Accommodation accommodation, Guest guest)
        {
            return _reservation.GenerateAlternativeReservations(start, numOfDays, numOfReservations, accommodation, guest);
        }

        public bool IsReserved(int accommodationID, DateTime start, DateTime end)
        {
            return _reservation.IsReserved(accommodationID, start, end);
        }

        public string ReviewNotification(Owner owner, out int changes)
        {
            return _reservation.ReviewNotification(owner.ID, out changes);
        }

        public void Subcribe(IObserver observer)
        {
            _reservation.Subscribe(observer);
        }
    }
}
