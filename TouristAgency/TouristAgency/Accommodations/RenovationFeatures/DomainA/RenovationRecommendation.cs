using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.RenovationFeatures.DomainA
{
    public class RenovationRecommendation : ISerializable, INotifyPropertyChanged
    {
        private int _id;
        private Reservation _reservation;
        private int _reservationId;
        private string _comment;
        private int _urgencyLevel;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public RenovationRecommendation()
        {
            _id = -1;
            _comment = "";
            _urgencyLevel = 1;
        }

        public RenovationRecommendation(Reservation reservation, string comment, int urgencyLevel)
        {
            _reservation = reservation;
            _reservationId = reservation.Id;
            _comment = comment;
            _urgencyLevel = urgencyLevel;
        }

        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                }
            }
        }

        public Reservation Reservation
        {
            get => _reservation;
            set
            {
                if (value != _reservation)
                {
                    _reservation = value;
                    OnPropertyChanged("Reservation");
                }              
            }
        }

        public int ReservationId
        {
            get => _reservationId;
            set
            {
                if (value != _reservationId)
                {
                    _reservationId = value;
                    OnPropertyChanged("ReservationId");
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public int UrgencyLevel
        {
            get => _urgencyLevel;
            set
            {
                if (value != _urgencyLevel)
                {
                    _urgencyLevel = value;
                    OnPropertyChanged("UrgencyLevel");
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Comment = values[2];
            UrgencyLevel = int.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Comment,
                UrgencyLevel.ToString()
            };

            return csvValues;
        }

    }
}
