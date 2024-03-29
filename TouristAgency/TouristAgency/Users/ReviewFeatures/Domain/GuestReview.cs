﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.ReviewFeatures.Domain
{
    public class GuestReview : ISerializable, INotifyPropertyChanged,IValidate
    {
        private int _id;
        private Reservation _reservation;
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
            Reservation = new();
        }

        public GuestReview(Reservation reservation, int cleanliness, int ruleAbiding, int communication, int overallImpression, int noiseLevel, string comment = "")
        {
            _id = -1;
            _reservation = reservation;
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
                if (_reservation != value)
                {
                    _reservation = value;
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        public int Communication
        {
            get => _communication;
            set
            {
                if (value != _communication)
                {
                    _communication = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OverallImpression
        {
            get => _overallImpression;
            set
            {
                if (value != _overallImpression)
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
                if (value != _noiseLevel)
                {
                    _noiseLevel = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        private Dictionary<string, string> _validationErrors = new()
        {
            {"Comment",string.Empty}
        };

        public Dictionary<string, string> ValidationErrors
        {
            get => _validationErrors;
            set
            {
                _validationErrors = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var key in ValidationErrors.Keys)
                {
                    if (!string.IsNullOrEmpty(ValidationErrors[key]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void ValidationClear()
        {
            ValidationErrors["Comment"] = string.Empty;
            OnPropertyChanged(nameof(ValidationErrors));
        }

        public void ValidateSelf()
        {
            ValidationClear();

            if (string.IsNullOrEmpty(Comment))
            {
                ValidationErrors["Comment"] = "This is a required field";
            }

            OnPropertyChanged(nameof(ValidationErrors));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Reservation.Id = int.Parse(values[1]);
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
                Reservation.Id.ToString(),
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
