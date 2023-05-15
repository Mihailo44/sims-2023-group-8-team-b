using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.SuperGuestFeature.Domain
{
    public class SuperGuestTitle : INotifyPropertyChanged, ISerializable
    {
        private int _id;
        private int _guestId;
        private Guest _guest;
        private int _points;
        private int _numOfReservations;
        private DateTime _lastUpdated;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public SuperGuestTitle()
        {
            _id = -1;
            _guestId = -1;
            _guest = new Guest();
            _points = 0;
            _numOfReservations = 0;
            _lastUpdated = DateTime.Now;
        }

        public SuperGuestTitle(int guestId, Guest guest, bool isSuperGuest, int points, int numOfReservations, DateTime lastUpdated)
        {
            _guestId = guestId;
            _guest = guest;
            _points = points;
            _numOfReservations = numOfReservations;
            _lastUpdated = lastUpdated;
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                }
            }
        }

        public int GuestId
        {
            get { return _guestId; }
            set
            {
                if (value != _guestId)
                {
                    _guestId = value;
                    OnPropertyChanged("GuestId");

                }
            }
        }

        public Guest Guest
        {
            get { return _guest; }
            set
            {
                if (value != _guest)
                {
                    _guest = value;
                    OnPropertyChanged("Guest");

                }
            }
        }

        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                if (value != _lastUpdated)
                {
                    _lastUpdated = value;
                    OnPropertyChanged("LastUpdated");

                }
            }
        }

        public int Points
        {
            get { return _points; }
            set
            {
                if (_points != value)
                {
                    _points = value;
                    OnPropertyChanged("Points");
                }
            }
        }

        public int NumOfReservations
        {
            get { return _numOfReservations; }
            set
            {
                if (_numOfReservations != value)
                {
                    _numOfReservations = value;
                    OnPropertyChanged("NumOfReservations");
                }
            }
        }

        public new void FromCSV(string[] values)
        {
            _id = Convert.ToInt32(values[0]);
            _guestId = Convert.ToInt32(values[1]);
            _numOfReservations = Convert.ToInt32(values[2]);
            _points = Convert.ToInt32(values[3]);
            _lastUpdated = Convert.ToDateTime(values[4]);
        }

        public new string[] ToCSV()
        {
            string[] csvValues =
            {
                _id.ToString(),
                _guestId.ToString(),
                _numOfReservations.ToString(),
                _points.ToString(),
                _lastUpdated.ToString(),
            };
            return csvValues;
        }
    }
}
