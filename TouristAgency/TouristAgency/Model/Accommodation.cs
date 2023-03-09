using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    public enum TYPE { HOTEL,HUT,APARTMENT};

    internal class Accommodation
    {
        private int _id;
        private string _name;
        private Location _location;
        private TYPE _type;
        private int _maxGuestNumber;
        private int _minNumOfDays;
        private int _allowedNumOfDaysForCancelation;
        private bool _recentlyRenovated;

        public Accommodation()
        {
            _id = -1;
            _allowedNumOfDaysForCancelation = 1;   
            _recentlyRenovated = false;
        }

        public Accommodation(string name, Location location, TYPE type,int maxGuestNumber,int minNumOfDays,int allowedNumOfDaysForCancelation)
        {
            _name = name;
            _location = location;
            _type = type;
            _maxGuestNumber = maxGuestNumber;
            _minNumOfDays = minNumOfDays;
            _allowedNumOfDaysForCancelation = allowedNumOfDaysForCancelation;
            _recentlyRenovated = false;
        }

        public int Id
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if(value != _name)
                {
                    _name = value;
                }
            }
        }

        public Location Location
        {
            get => _location;
            set
            {
                if(value != _location)
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
                }
            }
        }

        public int MaxGuestNumber
        {
            get => _maxGuestNumber; 
            set
            {
                if (value != _maxGuestNumber)
                {
                    _maxGuestNumber = value;
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
    }
}
