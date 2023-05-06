using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.TourRequests
{
    public class TourRequest : INotifyPropertyChanged, Interfaces.ISerializable
    {
        private int _ID;
        private int _touristID;
        private int _guideID;
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
            _touristID = -1;
            _guideID = -1;
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

        public int TouristID
        {
            get => _touristID;
            set
            {
                if (value != _touristID)
                {
                    _touristID = value;
                }
            }
        }

        public int GuideID
        {
            get => _guideID;
            set
            {
                if (value != _guideID)
                {
                    _guideID = value;
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
                TouristID.ToString(),
                GuideID.ToString(),
                Status.ToString(),
                ShortLocationID.ToString(),
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
            TouristID = Convert.ToInt32(values[1]);
            GuideID = Convert.ToInt32(values[2]);
            Status = Enum.Parse<TourRequestStatus>(values[3]);
            ShortLocationID = Convert.ToInt32(values[4]);
            Description = Convert.ToString(values[5]);
            Language = values[6];
            MaxAttendance = Convert.ToInt32(values[7]);
            StartDate = Convert.ToDateTime(values[8]);
            EndDate = Convert.ToDateTime(values[9]);
        }
    }
}
