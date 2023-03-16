using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;


namespace TouristAgency.Model
{
    public enum REVIEW_STATUS { REVIEWED,UNREVIEWED,EXPIRED}

    public class Reservation : ISerializable
    {
        private int _id;
        private Guest _guest;
        private int _guestId;
        private Accommodation _accommodation;
        private int _accommodationId;
        private DateTime _start;
        private DateTime _end;
        private bool _canceled;
        private bool _postponed;
        private REVIEW_STATUS _status;

        public Reservation()
        {
            _id = -1;
            _canceled = false;
            _postponed = false;
            _status = REVIEW_STATUS.UNREVIEWED;
        }

        public Reservation(Guest guest, Accommodation accommodation, DateTime start, DateTime end)
        {
            _guest = guest;
            _guestId = guest.ID;
            _accommodation = accommodation;
            _accommodationId = accommodation.Id;
            _start = start;
            _end = end;
            _canceled = false;
            _postponed = false;
            _status = REVIEW_STATUS.UNREVIEWED;
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

        public DateTime Start
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

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                {
                    _end = value;
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

        public REVIEW_STATUS Status
        {
            get => _status;
            set
            {
                if(_status != value)
                {
                    _status = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            AccommodationId = int.Parse(values[2]);
            Start = DateTime.Parse(values[3]);
            End = DateTime.Parse(values[4]);
            Status = Enum.Parse<REVIEW_STATUS>(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                AccommodationId.ToString(),
                Start.ToString(),
                End.ToString(),
                Status.ToString()
            };

            return csvValues;
        }
    }
}
