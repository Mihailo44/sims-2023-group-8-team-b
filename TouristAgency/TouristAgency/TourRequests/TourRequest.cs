using System;
using System.ComponentModel;
using TouristAgency.Base;
using TouristAgency.Util;

namespace TouristAgency.TourRequests
{
    public class TourRequest : INotifyPropertyChanged
    {
        private int _ID;
        private TourRequestStatus _status;
        private Location _shortLocation;
        private int _shortLocationID;
        private string _description;
        private string _language;
        private int _maxAttendants;
        private DateTime _startDate;
        private DateTime _endDate;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public TourRequest()
        {
            _ID = -1;
            _status = TourRequestStatus.PENDING;
            _shortLocation = new Location();
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
        }

        public TourRequest(int id, TourRequestStatus status, Location shortLocation, string description, string language, int maxAttendants, DateTime startDate, DateTime endDate)
        {
            _ID = id;
            _status = status;
            _shortLocation = shortLocation;
            _description = description;
            _language = language;
            _maxAttendants = maxAttendants;
            _startDate = startDate;
            _endDate = endDate;
        }

        public int ID 
        {
            get => _ID;
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                }
            }
        }

        public TourRequestStatus Status
        {
            get => _status;
            set 
            {
                if (value != _status) 
                {
                    _status = value;
                }
            }
        }

        public Location ShortLocation
        {
            get => _shortLocation;
            set
            {
                if (value != _shortLocation) 
                {
                    _shortLocation = value;
                }
            }
        }

        public int ShortLocationID
        {
            get => _shortLocationID;
            set
            {
                if (_shortLocationID != value)
                {
                    _shortLocationID = value;
                }
            }
        }

        public string Description
        {
            get => _description; 
            set
            {
                if (value != _description)
                {
                    _description = value;
                }
            }
        }

        public string Language
        {
            get => _language; 
            set
            {
                if (value != _language)
                {
                    _language = value;
                }
            }
        }

        public int MaxAttendance
        {
            get => _maxAttendants; 
            set
            {
                if (value != _maxAttendants)
                {
                    _maxAttendants = value;
                }
            }
        }

        public DateTime StartDate
        {
            get => _startDate; 
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                }
            }
        }

        public DateTime EndDate
        {
            get => _endDate; 
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                }
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                Status.ToString(),
                ShortLocation.ToString(),
                Description,
                Language,
                MaxAttendance.ToString(),
                StartDate.ToString(),
                EndDate.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            Status = Enum.Parse<TourRequestStatus>(values[1]);
            ShortLocationID = Convert.ToInt32(values[2]);
            Description = Convert.ToString(values[3]);
            Language = values[4];
            MaxAttendance = Convert.ToInt32(values[5]);
            StartDate = Convert.ToDateTime(values[6]);
            EndDate = Convert.ToDateTime(values[7]);
        }
    }
}
