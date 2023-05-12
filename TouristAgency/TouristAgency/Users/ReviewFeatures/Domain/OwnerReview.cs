using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Users.ReviewFeatures.Domain
{
    public class OwnerReview : ISerializable, INotifyPropertyChanged
    {
        private int _id;
        private Reservation _reservation;
        private int _reservationId;
        private DateTime _reviewDate;
        private int _cleanliness;
        private int _ownerCorrectness;
        private int _location;
        private int _comfort;
        private int _wifi;
        private string _comment;
        private List<Photo> _photos;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public OwnerReview()
        {
            _id = -1;
            _cleanliness = 1;
            _ownerCorrectness = 1;
            _location = 1;
            _comfort = 1;
            _wifi = 1;
            _reviewDate = DateTime.Now;
            _photos = new List<Photo>();
        }

        public OwnerReview(Reservation reservation, int cleanliness, int ownerCorrectness, int location, int comfort, int wifi, string comment = "")
        {
            _reservation = reservation;
            _reservationId = reservation.Id;
            _reviewDate = DateTime.Now;
            _cleanliness = cleanliness;
            _ownerCorrectness = ownerCorrectness;
            _location = location;
            _comfort = comfort;
            _wifi = wifi;
            _comment = comment;
            _photos = new List<Photo>();
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

        public DateTime ReviewDate
        {
            get => _reviewDate;
            set
            {
                if (value != _reviewDate)
                {
                    _reviewDate = value;
                    OnPropertyChanged("ReviewDate");
                }
            }
        }

        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged("Cleanliness");
                }
            }
        }

        public int OwnerCorrectness
        {
            get => _ownerCorrectness;
            set
            {
                if (value != _ownerCorrectness)
                {
                    _ownerCorrectness = value;
                    OnPropertyChanged("OwnerCorrectness");
                }
            }
        }

        public int Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        public int Comfort
        {
            get => _comfort;
            set
            {
                if (value != _comfort)
                {
                    _comfort = value;
                    OnPropertyChanged("Comfort");
                }
            }
        }

        public int Wifi
        {
            get => _wifi;
            set
            {
                if (value != _wifi)
                {
                    _wifi = value;
                    OnPropertyChanged("Wifi");
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

        public List<Photo> Photos
        {
            get => _photos;
            set => _photos = value;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            ReviewDate = DateTime.Parse(values[2]);
            Cleanliness = int.Parse(values[3]);
            OwnerCorrectness = int.Parse(values[4]);
            Location = int.Parse(values[5]);
            Comfort = int.Parse(values[6]);
            Wifi = int.Parse(values[7]);
            Comment = values[8];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                ReviewDate.ToShortDateString(),
                Cleanliness.ToString(),
                OwnerCorrectness.ToString(),
                Location.ToString(),
                Comfort.ToString(),
                Wifi.ToString(),
                Comment
            };

            return csvValues;
        }
    }
}
