using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model.Enums;

namespace TouristAgency.Model
{

    public class Tour : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        private int _ID;
        private string _name;
        private STATUS _status;
        private string _description;
        private Location _shortLocation;
        private int _shortLocationID; //TODO PROVERA DA LI POSTOJI
        private string _language;
        private int _maxAttendants;
        private int _currentAttendants;
        private int _remainingCapacity;
        private int _duration;
        private DateTime _startDateTime;
        private List<Checkpoint> _checkpoints;
        private List<Tourist> _registeredTourists;
        private Guide _assignedGuide;
        private int _assignedGuideID;
        private List<Photo> _photos;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public Tour()
        {
            _ID = -1;
            _status = STATUS.NOT_STARTED;
            _startDateTime = DateTime.Today;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();
            _assignedGuide = new Guide();
            _shortLocation = new Location();
            _photos = new List<Photo>();
        }

        public Tour(int id, string name, string description, Location location, string language, int maxAttendants, int duration, DateTime startDateTime)
        {
            _ID = id;
            _status = STATUS.NOT_STARTED;
            _name = name;
            _description = description;
            _shortLocation = location;
            _language = language;
            _maxAttendants = maxAttendants;
            _remainingCapacity = maxAttendants;
            _duration = duration;
            _startDateTime = startDateTime;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();
            _assignedGuide = new Guide();
            _photos = new List<Photo>();
        }


        public Tour(Tour newTour)
        {
            _ID = newTour.ID;
            _status = newTour._status;
            _name = newTour.Name;
            _description = newTour.Description;
            _shortLocation = newTour.ShortLocation;
            _language = newTour.Language;
            _maxAttendants = newTour.MaxAttendants;
            _duration = newTour.Duration;
            _startDateTime = newTour.StartDateTime;
            _checkpoints = new List<Checkpoint>();
            _assignedGuide = new Guide();
            _assignedGuideID = newTour.AssignedGuideID;
            _registeredTourists = new List<Tourist>();
            _remainingCapacity = newTour.RemainingCapacity;
            _photos = newTour.Photos;
            _shortLocationID = newTour.ShortLocationID;
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

        public STATUS Status
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

        public string Name
        {
            get => _name;
            set {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
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
            get { return _shortLocationID; }
            set
            {
                _shortLocationID = value;
                OnPropertyChanged("ShortLocationID");
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
            set {
                if (value != _maxAttendants)
                {
                    _maxAttendants = value;
                    OnPropertyChanged("MaxAttendants");
                }
            }
        }

        public int CurrentAttendants
        {
            get => _currentAttendants;
            set
            {
                if (value != _currentAttendants)
                {
                    _currentAttendants = value;
                    OnPropertyChanged("CurrentAttendants");
                }
            }
        }

        public int RemainingCapacity
        {
            get => _remainingCapacity;
            set
            {
                if(value != _remainingCapacity)
                {
                    _remainingCapacity = value;
                    OnPropertyChanged("RemainingCapacity");
                }
            }
        }

        public int Duration
        {
            get => _duration;
            set {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public DateTime StartDateTime
        {
            get => _startDateTime;
            set {
                if (value != _startDateTime)
                {
                    _startDateTime = value;
                    OnPropertyChanged("StartDateTime");
                }
            }
        }

        public List<Checkpoint> Checkpoints
        {
            get => _checkpoints;
            set {
                if (value != _checkpoints)
                {
                    _checkpoints = value;
                    OnPropertyChanged("Checkpoints");
                }
            }
        }

        public List<Tourist> RegisteredTourists
        {
            get => _registeredTourists;
            set {
                if (value != _registeredTourists)
                {
                    _registeredTourists = value;
                    OnPropertyChanged("RegisteredTourists");
                }
            }
        }

        public Guide AssignedGuide
        {
            get => _assignedGuide;
            set {
                if (value != _assignedGuide)
                {
                    _assignedGuide = value;
                    OnPropertyChanged("AssignedGuide");
                }
            }
        }

        public int AssignedGuideID
        {
            get => _assignedGuideID;
            set
            {
                if (value != _assignedGuideID)
                {
                    _assignedGuideID = value;
                    OnPropertyChanged("AssignedGuideID");
                }
            }
        }

        public List<Photo> Photos
        {
            get => _photos;
            set
            {
                if (value != _photos)
                {
                    _photos = value;
                }
            }
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Required field";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Required field";
                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                        return "Required field";
                }
                else if (columnName == "MaxAttendants")
                {
                    if (string.IsNullOrEmpty(MaxAttendants.ToString()))
                        return "Required field";
                }
                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration.ToString()))
                        return "Required field";
                }
                else if (columnName == "StartDateTime")
                {
                    if (string.IsNullOrEmpty(StartDateTime.ToString()))
                        return "Required field";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "Name", "MaxGuestNum", "MinNumOfDays", "AllowedNumOfDaysForCancelation" };

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
                Name,
                Status.ToString(),
                Description,
                Language,
                MaxAttendants.ToString(),
                CurrentAttendants.ToString(),
                RemainingCapacity.ToString(),
                Duration.ToString(),
                StartDateTime.ToString(),
                AssignedGuideID.ToString(),
                ShortLocationID.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            Name = values[1];
            Status = Enum.Parse<STATUS>(values[2]);
            Description = values[3];
            Language = values[4];
            MaxAttendants = Convert.ToInt32(values[5]);
            CurrentAttendants = Convert.ToInt32(values[6]);
            RemainingCapacity = Convert.ToInt32(values[7]);
            Duration = Convert.ToInt32(values[8]);
            StartDateTime = DateTime.Parse(values[9]);
            AssignedGuideID = Convert.ToInt32(values[10]);
            ShortLocationID = Convert.ToInt32(values[11]);
        }
    }
}
