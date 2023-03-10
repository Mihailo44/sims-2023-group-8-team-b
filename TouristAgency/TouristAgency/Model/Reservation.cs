using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    public class Reservation
    {
        private int _id;
        private Guest _guest;
        private Accommodation _accommodation;
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
            _accommodation = accommodation;
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
    }
}
