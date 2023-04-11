using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model.Enums;

namespace TouristAgency.Model
{
    public class PostponementRequest : ISerializable
    {
        private int _id;
        private DateTime _start;
        private DateTime _end;
        private string _comment;
        private Reservation _reservation;
        private int _reservationId;
        private PostponementRequestStatus _status;
        private bool _seen;

        public PostponementRequest()
        {
            _id = -1;
            _comment = "";
            _status = PostponementRequestStatus.PENDING;
            _seen = false;
        }

        public PostponementRequest(Reservation reservation, DateTime start, DateTime end)
        {
            _reservation = reservation;
            _reservationId = reservation.Id;
            _start = start;
            _end = end;
            _comment = "";
            _status = PostponementRequestStatus.PENDING;
            _seen = false;
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

        public bool Seen
        {
            get => _seen;
            set
            {
                if (_seen != value)
                {
                    _seen = value;
                }
            }
        }

        public Reservation Reservation
        {
            get => _reservation;
            set => _reservation = value;
        }

        public int ReservationId
        {
            get => _reservationId;
            set
            {
                if(_reservationId != value)
                {
                    _reservationId = value;
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
                }
            }
        }

        public PostponementRequestStatus Status
        {
            get => _status;
            set
            {
                if(value != _status)
                {
                    _status = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Start = DateTime.Parse(values[2]);
            End = DateTime.Parse(values[3]);
            Status = Enum.Parse<PostponementRequestStatus>(values[4]);
            Comment = values[5];
            Seen = Boolean.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Start.ToString(),
                End.ToString(),
                Status.ToString(),
                Comment,
                Seen.ToString()
            };

            return csvValues;
        }
    }
}
