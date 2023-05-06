﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Reservations.Domain
{
    public class Reservation : ISerializable
    {
        private int _id;
        private Guest _guest;
        private int _guestId;
        private Accommodation _accommodation;
        private int _accommodationId;
        private DateTime _start;
        private DateTime _end;
        private bool _isCanceled;
        private ReviewStatus _status;
        private ReviewStatus _ostatus;

        public Reservation()
        {
            _id = -1;
            _isCanceled = false;
            _status = ReviewStatus.UNREVIEWED;
            _ostatus = ReviewStatus.UNREVIEWED;
        }

        public Reservation(Guest guest, Accommodation accommodation, DateTime start, DateTime end)
        {
            _guest = guest;
            _guestId = guest.ID;
            _accommodation = accommodation;
            _accommodationId = accommodation.Id;
            _start = start;
            _end = end;
            _isCanceled = false;
            _status = ReviewStatus.UNREVIEWED;
            _ostatus = ReviewStatus.UNREVIEWED;
        }

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
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
                if (_guest != value)
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
                if (_guestId != value)
                {
                    _guestId = value;
                }
            }
        }

        public Accommodation Accommodation
        {
            get => _accommodation;
            set => _accommodation = value;
        }

        public int AccommodationId
        {
            get => _accommodationId;
            set
            {
                if (_accommodationId != value)
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
                if (_start != value)
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

        public bool IsCanceled
        {
            get => _isCanceled;
            set
            {
                if (_isCanceled != value)
                {
                    _isCanceled = value;
                }
            }
        }

        public ReviewStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                }
            }
        }

        public ReviewStatus OStatus
        {
            get => _ostatus;
            set
            {
                if (_ostatus != value)
                {
                    _ostatus = value;
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
            Status = Enum.Parse<ReviewStatus>(values[5]);
            OStatus = Enum.Parse<ReviewStatus>(values[6]);
            IsCanceled = bool.Parse(values[7]);
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
                Status.ToString(),
                OStatus.ToString(),
                IsCanceled.ToString()
            };

            return csvValues;
        }
    }
}
