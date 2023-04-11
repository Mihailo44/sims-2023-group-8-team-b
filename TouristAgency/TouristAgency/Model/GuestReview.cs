using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class GuestReview : ISerializable
    {
        private int _id;
        private Reservation _reservation;
        private int _reservationId;
        private DateTime _reviewDate;
        private int _cleanliness;
        private int _ruleAbiding;
        private int _communication;
        private int _overallImpression;
        private int _noiseLevel;
        private string _comment;
       

        public GuestReview()
        {
            _id = -1;
            _cleanliness = 1;
            _ruleAbiding = 1;
            _communication = 1;
            _overallImpression = 1;
            _noiseLevel = 1;
            _reviewDate = DateTime.Now;
        }

        public GuestReview(Reservation reservation, int cleanliness, int ruleAbiding,int communication,int overallImpression,int noiseLevel,string comment="")
        {
            _reservation = reservation;
            _reservationId = reservation.Id;
            _reviewDate = DateTime.Now;
            _cleanliness = cleanliness;
            _ruleAbiding = ruleAbiding;
            _communication = communication;
            _overallImpression = overallImpression;
            _noiseLevel = noiseLevel;
            _comment = comment;
        }

        public int Id
        {
            get => _id;
            set
            {
                if(value != _id)
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
                if(_reservation != value)
                {
                    _reservation = value;
                }
            }
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

        public DateTime ReviewDate
        {
            get => _reviewDate;
            set
            {
                if(value != _reviewDate)
                {
                    _reviewDate = value;
                }
            }
        }

        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if(value != _cleanliness)
                {
                    _cleanliness = value;
                }
            }
        }

        public int RuleAbiding
        {
            get => _ruleAbiding; 
            set
            {
                if (value != _ruleAbiding)
                {
                    _ruleAbiding = value;
                }
            }
        }

        public int Communication
        {
            get => _communication;
            set
            {
                if(value != _communication)
                {
                    _communication = value;
                }
            }
        }

        public int OverallImpression
        {
            get => _overallImpression;
            set
            {
                if(value != _overallImpression)
                {
                    _overallImpression = value;
                }
            }
        }

        public int NoiseLevel
        {
            get => _noiseLevel;
            set
            {
                if(value != _noiseLevel)
                {
                    _noiseLevel = value;
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

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            ReviewDate = DateTime.Parse(values[2]);
            Cleanliness = int.Parse(values[3]);
            RuleAbiding = int.Parse(values[4]);
            Communication = int.Parse(values[5]);
            OverallImpression = int.Parse(values[6]);
            NoiseLevel = int.Parse(values[7]);
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
                RuleAbiding.ToString(),
                Communication.ToString(),
                OverallImpression.ToString(),
                NoiseLevel.ToString(),
                Comment
            };

            return csvValues;
        }
    }
}
