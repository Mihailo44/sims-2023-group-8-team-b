using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;


namespace TouristAgency.Model
{
    public class Reservation : ISerializable
    {
        private int _id;
        private Guest _guest;
        private int _guestId;
        private Accommodation _accommodation;
        private int _accommodationId;
        private DateOnly _start;
        private DateOnly _end;
        private bool _canceled;
        private bool _postponed;

        public Reservation()
        {
            _id = -1;
            _canceled = false;
            _postponed = false;
        }

        public Reservation(Guest guest, Accommodation accommodation, DateOnly start, DateOnly end)
        {
            _guest = guest;
            _guestId = guest.ID;
            _accommodation = accommodation;
            _accommodationId = accommodation.Id;
            _start = start;
            _end = end;
            _canceled = false;
            _postponed = false;
        }
        
        public int Id
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;
                }
            }
        }

        public Guest Guest
        {
            get => _guest;
            set
            {
                if(_guest != value)
                {
                    _guest = value;
                }
            }
        }

        public int GuestId
        {
            get => _guestId;
            set
            {
                if(_guestId != value)
                {
                    _guestId = value;
                }
            }
        }

        public Accommodation Accommodation
        {
            get => _accommodation;
            set
            {
                if (_accommodation != value)
                {
                    _accommodation = value;
                }
            }
        }

        public int AccommodationId
        {
            get => _accommodationId;
            set
            {
                if(_accommodationId != value)
                {
                    _accommodationId = value;
                }
            }
        }

        public DateOnly Start
        {
            get => _start;
            set
            {
                if(_start != value)
                {
                    _start = value;
                }
            }
        }

        public DateOnly End
        {
            get => _end;
            set
            {
                if (_start != value)
                {
                    _start = value;
                }
            }
        }

        public bool Canceled
        {
            get => _canceled;
            set
            {
                if (_canceled != value)
                {
                    _canceled = value;
                }
            }
        }
        
        public bool Postponed
        {
            get => _postponed; 
            set
            {
                if (_postponed != value)
                {
                    _postponed = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            AccommodationId = int.Parse(values[2]);
            Start = DateOnly.Parse(values[3]);
            End = DateOnly.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                AccommodationId.ToString(),
                Start.ToString(),
                End.ToString()
            };

            return csvValues;
        }
    }
}
