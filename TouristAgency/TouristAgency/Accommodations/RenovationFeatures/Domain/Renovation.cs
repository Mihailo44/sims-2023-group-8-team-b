using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.RenovationFeatures.Domain
{
    public class Renovation : ISerializable
    {
        private int _id;
        private Accommodation _accommodation;
        private int _accommodationId;
        private DateTime _start;
        private DateTime _end;
        private int _estimatedDuration;
        private string _description;
        private bool _isCanceled;

        public Renovation()
        {
            _id = -1;
        }

        public Renovation(Accommodation accommodation, DateTime start, DateTime end, int estimatedDuration)
        {
            _id = -1;
            _accommodation = accommodation;
            _accommodationId = accommodation.Id;
            _start = start;
            _end = end;
            _estimatedDuration = estimatedDuration;
            _isCanceled = false;
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

        public int EstimatedDuration
        {
            get => _estimatedDuration;
            set
            {
                if (_estimatedDuration != value)
                {
                    _estimatedDuration = value;
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
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

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            Start = DateTime.Parse(values[2]);
            End = DateTime.Parse(values[3]);
            EstimatedDuration = int.Parse(values[4]);
            Description = values[5];
            IsCanceled = bool.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                Start.ToString(),
                End.ToString(),
                EstimatedDuration.ToString(),
                Description,
                IsCanceled.ToString()
            };

            return csvValues;
        }
    }
}
