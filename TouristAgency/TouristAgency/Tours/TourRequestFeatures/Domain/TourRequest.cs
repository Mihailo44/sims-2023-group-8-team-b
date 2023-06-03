using System;
using System.ComponentModel;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.TourRequests
{
    public class TourRequest : INotifyPropertyChanged, Interfaces.ISerializable, IDataErrorInfo
    {
        private int _ID;
        private int _touristID;
        private Tourist _tourist;
        private int _guideID;
        private Guide _guide;
        private TourRequestStatus _status;
        private Location _shortLocation;
        private int _shortLocationID;
        private string _description;
        private string _language;
        private int _maxAttendants;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _complexTourRequestID;
        private TourRequestType _type;

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
            _type = TourRequestType.SINLGE;
        }

        public TourRequest(TourRequestStatus status, Location shortLocation, string description, string language, int maxAttendants, DateTime startDate, DateTime endDate)
        {
            _status = status;
            _shortLocation = shortLocation;
            _description = description;
            _language = language;
            _maxAttendants = maxAttendants;
            _startDate = startDate;
            _endDate = endDate;
            _type = TourRequestType.SINLGE;
        }

        public int ID 
        {
            get => _ID;
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                    OnPropertyChanged("ID");
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
                    OnPropertyChanged("TouristID");
                }
            }
        }

        public Tourist Tourist
        {
            get => _tourist;
            set
            {
                if (value != _tourist)
                {
                    _tourist = value;
                    OnPropertyChanged("Tourist");
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
                    OnPropertyChanged("GuideID");
                }
            }
        }

        public Guide Guide
        {
            get => _guide;
            set
            {
                if (value != _guide)
                {
                    _guide = value;
                    OnPropertyChanged("Guide");
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
                    OnPropertyChanged("Status");
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
                    OnPropertyChanged("ShortLocation");
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
                    OnPropertyChanged("ShortLocationID");
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
                    OnPropertyChanged("Description");
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
                    OnPropertyChanged("Language");
                }
            }
        }

        public int MaxAttendants
        {
            get => _maxAttendants; 
            set
            {
                if (value != _maxAttendants)
                {
                    _maxAttendants = value;
                    OnPropertyChanged("MaxAttedants");
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
                    OnPropertyChanged("StartDate");
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
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public int ComplexTourRequestID
        {
            get => _complexTourRequestID;
            set
            {
                if(value != _complexTourRequestID)
                {
                    _complexTourRequestID = value;
                    OnPropertyChanged("ComplexTourRequestID");
                }
            }
        }

        public TourRequestType Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(ShortLocation.City))
                        return "Required field";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(ShortLocation.Country))
                        return "Required field";
                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                        return "Required field";
                }
                else if (columnName == "MaxAttendance")
                {
                    if (string.IsNullOrEmpty(MaxAttendants.ToString()))
                        return "Required field";
                }
                else if (columnName == "StartDate")
                {
                    if (string.IsNullOrEmpty(StartDate.ToString()))
                        return "Required field";
                }
                else if (columnName == "EndDate")
                {
                    if (string.IsNullOrEmpty(EndDate.ToString()))
                        return "Required field";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "City", "Country", "Language", "MaxAttendance", "StartDate", "EndDate" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
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
                MaxAttendants.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                ComplexTourRequestID.ToString()
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
            MaxAttendants = Convert.ToInt32(values[7]);
            StartDate = Convert.ToDateTime(values[8]);
            EndDate = Convert.ToDateTime(values[9]);
            ComplexTourRequestID = Convert.ToInt32(values[10]);
        }
    }
}
