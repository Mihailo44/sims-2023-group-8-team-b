using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Notifications.Domain
{
    public class GuestReviewNotification : Notification, ISerializable
    {
        private int _reservationId;
        private Reservation _reservation;

        public GuestReviewNotification() : base()
        {

        }

        public GuestReviewNotification(string message,int reservationId) : base(message)
        {
            _reservationId = reservationId;
        }

        public int ReservationId
        {
            get => _reservationId;
            set
            {
                _reservationId = value;
            }
        }

        public Reservation Reservation
        {
            get => _reservation;
            set
            {
                _reservation = value;
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Message = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Message
            };

            return csvValues;
        }
    }
}
