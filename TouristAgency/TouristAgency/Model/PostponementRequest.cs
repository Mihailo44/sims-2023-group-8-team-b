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

        public PostponementRequest()
        {
            _id = -1;
        }

        public PostponementRequest(Reservation reservation, DateTime start, DateTime end)
        {
            _reservation = reservation;
            _start = start;
            _end = end;
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

        public Reservation Reservation
        {
            get => _reservation;
            set => _reservation = value;
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Start = DateTime.Parse(values[1]);
            End = DateTime.Parse(values[2]);
            Comment = values[3];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Start.ToString(),
                End.ToString(),
                Comment
            };

            return csvValues;
        }
    }
}
