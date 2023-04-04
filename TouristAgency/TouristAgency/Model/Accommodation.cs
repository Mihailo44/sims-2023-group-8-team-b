using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using TouristAgency.Interfaces;
using TouristAgency.Model.Enums;

namespace TouristAgency.Model
{
    public class Accommodation : ISerializable,INotifyPropertyChanged,IDataErrorInfo
    {
        private int _id;
        private Owner _owner;
        private int _ownerId;
        private string _name;
        private Location _location;
        private int _locationId;
        private TYPE _type;
        private int _maxGuestNum;
        private int _minNumOfDays;
        private int _allowedNumOfDaysForCancelation;
        private List<Photo> _photos;
        private bool _recentlyRenovated;

        public Accommodation()
        {
            _id = -1;
            _allowedNumOfDaysForCancelation = 1;
            _photos = new List<Photo>();
            _recentlyRenovated = false;
        }

        public Accommodation(string name, Owner owner, Location location, TYPE type, int maxGuestNum, int minNumOfDays, int allowedNumOfDaysForCancelation)
        {
            _name = name;
            _owner = owner;
            _ownerId = _owner.ID;
            _location = location;
            _locationId = location.Id;
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

        public int OwnerId
        {
            get => _ownerId;
            set
            {
                if (value != _ownerId)
                {
                    _ownerId = value;
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

        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value != _locationId)
                {
                    _locationId = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                else if (columnName == "MaxGuestNum")
                {
                    if (string.IsNullOrEmpty(MaxGuestNum.ToString()))
                        return "Required field";
                }
                else if (columnName == "MinNumOfDays")
                {
                    if (string.IsNullOrEmpty(MinNumOfDays.ToString()))
                        return "Required field";
                }
                else if (columnName == "AllowedNumOfDaysForCancelation")
                {
                    if (string.IsNullOrEmpty(AllowedNumOfDaysForCancelation.ToString()))
                        return "Required field";
                }

                return null;

            }
        }

        private readonly string[] _validatedProperties = {"Name", "MaxGuestNum", "MinNumOfDays", "AllowedNumOfDaysForCancelation" };

        public bool IsValid
        {
            get
            {
                foreach(var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            OwnerId = int.Parse(values[1]);
            Name = values[2];
            LocationId = int.Parse(values[3]);
            Type = Enum.Parse<TYPE>(values[4]);
            MaxGuestNum = int.Parse(values[5]);
            MinNumOfDays = int.Parse(values[6]);
            AllowedNumOfDaysForCancelation = int.Parse(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                OwnerId.ToString(),
                Name,
                LocationId.ToString(),
                Type.ToString(),
                MaxGuestNum.ToString(),
                MinNumOfDays.ToString(),
                AllowedNumOfDaysForCancelation.ToString()
            };
            return csvValues;
        }
    }
}
