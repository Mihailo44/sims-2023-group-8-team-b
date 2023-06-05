using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.Domain
{
    public class Accommodation : ISerializable, INotifyPropertyChanged, IValidate
    {
        private int _id;
        private Owner _owner;
        private string _name;
        private Location _location;
        private TYPE _type;
        private int _maxGuestNum;
        private int _minNumOfDays;
        private int _allowedNumOfDaysForCancelation;
        private List<Photo> _photos;
        private bool _recentlyRenovated;
        private bool _hotLocation;

        public Accommodation()
        {
            _id = -1;
            _allowedNumOfDaysForCancelation = 1;
            _type = TYPE.HOTEL;
            _photos = new List<Photo>();
            Owner = new();
            Location = new();
        }

        public Accommodation(string name, Owner owner, Location location, TYPE type, int maxGuestNum, int minNumOfDays, int allowedNumOfDaysForCancelation)
        {
            _id = -1;
            _name = name;
            _owner = owner;
            _location = location;
            _type = type;
            _maxGuestNum = maxGuestNum;
            _minNumOfDays = minNumOfDays;
            _allowedNumOfDaysForCancelation = allowedNumOfDaysForCancelation;
            _recentlyRenovated = false;
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

        public Owner Owner
        {
            get => _owner;
            set
            {
                if (value != _owner)
                {
                    _owner = value;
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public Location Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                }
            }
        }

        public TYPE Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MaxGuestNum
        {
            get => _maxGuestNum;
            set
            {
                if (value != _maxGuestNum)
                {
                    _maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MinNumOfDays
        {
            get => _minNumOfDays;
            set
            {
                if (value != _minNumOfDays)
                {
                    _minNumOfDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AllowedNumOfDaysForCancelation
        {
            get => _allowedNumOfDaysForCancelation;
            set
            {
                if (value != _allowedNumOfDaysForCancelation)
                {
                    _allowedNumOfDaysForCancelation = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Photo> Photos
        {
            get => _photos;
            set => _photos = value;
        }

        public bool RecentlyRenovated
        {
            get => _recentlyRenovated;
            set
            {
                if (value != _recentlyRenovated)
                {
                    _recentlyRenovated = value;
                }
            }
        }

        public bool HotLocation
        {
            get => _hotLocation;
            set
            {
                if (value != _hotLocation)
                {
                    _hotLocation = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Dictionary<string, string> _validationErrors = new()
        {
            {"Name",string.Empty},
            {"MaxGuestNum",string.Empty},
            {"MinNumOfDays",string.Empty},
            {"CancelationDays",string.Empty }
        };

        public Dictionary<string, string> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
            set
            {
                _validationErrors = value;
                OnPropertyChanged();
            }
        }

        public void ValidateSelf(string capacity,string minNumOfDays,string cancelationDays)
        {
            ValidationClear();

            Regex _intRegex = new Regex("[0-9]");

            if (string.IsNullOrEmpty(Name))
            {
                ValidationErrors["Name"] = "This is a required field";
            }

            if (!string.IsNullOrEmpty(capacity))
            {
                Match match = _intRegex.Match(capacity);
                if (!match.Success)
                {
                    ValidationErrors["MaxGuestNum"] = "Enter a number";
                }
            }
            else
            {
                ValidationErrors["MaxGuestNum"] = "This is a required field";
            }

            if (!string.IsNullOrEmpty(minNumOfDays))
            {
                ValidationErrors["MinNumOfDays"] = "This is a required field";
                Match match = _intRegex.Match(minNumOfDays);
                if (!match.Success)
                {
                    ValidationErrors["MinNumOfDays"] = "Enter a number";
                }
            }
            else
            {
                ValidationErrors["MinNumOfDays"] = "This is a required field";
            }

            if (!string.IsNullOrEmpty(cancelationDays))
            {
                Match match = _intRegex.Match(cancelationDays);
                if (!match.Success)
                {
                    ValidationErrors["CancelationDays"] = "Enter a number";
                }
            }
            else
            {
                ValidationErrors["CancelationDays"] = "This is a required field";
            }

            OnPropertyChanged(nameof(ValidationErrors));
        }

        public void ValidationClear()
        {
            ValidationErrors["Name"] = string.Empty;
            ValidationErrors["MaxGuestNum"] = string.Empty;
            ValidationErrors["MinNumOfDays"] = string.Empty;
            ValidationErrors["CancelationDays"] = string.Empty;
            OnPropertyChanged(nameof(ValidationErrors));
        }

        public bool IsValid
        {
            get
            {
                foreach(var key in ValidationErrors.Keys)
                {
                    if (!string.IsNullOrEmpty(ValidationErrors[key]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }


        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Owner.ID = int.Parse(values[1]);
            Name = values[2];
            Location.ID = int.Parse(values[3]);
            Type = Enum.Parse<TYPE>(values[4]);
            MaxGuestNum = int.Parse(values[5]);
            MinNumOfDays = int.Parse(values[6]);
            AllowedNumOfDaysForCancelation = int.Parse(values[7]);
            RecentlyRenovated = bool.Parse(values[8]);
            HotLocation = bool.Parse(values[9]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Owner.ID.ToString(),
                Name,
                Location.ID.ToString(),
                Type.ToString(),
                MaxGuestNum.ToString(),
                MinNumOfDays.ToString(),
                AllowedNumOfDaysForCancelation.ToString(),
                RecentlyRenovated.ToString(),
                HotLocation.ToString()
            };
            return csvValues;
        }
    }
}
