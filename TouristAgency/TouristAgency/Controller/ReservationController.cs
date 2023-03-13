using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Model.DAO;
using TouristAgency.Interfaces;

namespace TouristAgency.Controller
{
    public class ReservationController
    {
        private readonly ReservationDAO _reservation;

        public ReservationController()
        {
            _reservation = new ReservationDAO();
        }

        public List<Reservation> GetAll()
        {
            return _reservation.GetAll();
        }

        public void Create(Reservation newReservation)
        {
            _reservation.Create(newReservation);
        }

        public void Update(Reservation updatedReservation,int id)
        {
            _reservation.Update(updatedReservation, id);
        }

        public void Delete(Reservation reservation)
        {
            _reservation.Delete(reservation.Id);
        }

        public void Subcribe(IObserver observer)
        {
            _reservation.Subscribe(observer);
        }
    }
}
