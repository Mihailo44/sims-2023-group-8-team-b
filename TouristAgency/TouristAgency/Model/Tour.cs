using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Tour : ISerializable, INotifyPropertyChanged
    {
        private int _ID;
        private string _name;
        private string _description;
        //private string _location; // !
        private Location _shortLocation;
        private string _language;
        private int _maxAttendants;
        private int _duration;
        private DateOnly _startDate;
        private List<Checkpoint> _checkpoints;
        private List<Tourist> _registeredTourists;
        private Guide _assignedGuide;
        private int _assignedGuideID;

        private List<String> _photoLinks;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
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

        /*public string Location
        {
            get => _location;
            set {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }*/

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

        public DateOnly StartDate
        {
            get => _startDate;
            set {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
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

        public List<String> PhotoLinks
        {
            get => _photoLinks;
            set => _photoLinks = value;
        }

        public Tour()
        {
            _ID = -1;
            //_maxAttendants = -1;
            //_duration = -1;
            _startDate = DateOnly.MinValue;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();
            _assignedGuide = new Guide();
        }

        public Tour(int id, string name, string description,Location location, string language, int maxAttendants, int duration, DateOnly startDate)
        {
            _ID = id;
            _name = name;
            _description = description;
            _shortLocation = location;
            _language = language;
            _maxAttendants = maxAttendants;
            _duration = duration;
            _startDate = startDate;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();
            _assignedGuide = new Guide();
            //slike?
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                Name,
                Description,
                ShortLocation.ToString(), //!
                Language,
                MaxAttendants.ToString(),
                Duration.ToString(),
                StartDate.ToString(),
                AssignedGuide.ID.ToString()
                //Slike mozda u svoju klasu
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            //ShortLocation = values[3];
            Language = values[4];
            MaxAttendants = Convert.ToInt32(values[5]);
            Duration = Convert.ToInt32(values[6]);
            StartDate = DateOnly.Parse(values[7]);
            AssignedGuide.ID = Convert.ToInt32(values[8]);
        }

    }
}
