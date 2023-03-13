using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public enum TYPE { HOTEL,HUT,APARTMENT};

    public class Accommodation : ISerializable
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
        private List<string> _photoUrls;
        private bool _recentlyRenovated;

        public Accommodation()
        {
            _id = -1;
            _allowedNumOfDaysForCancelation = 1;   
            _recentlyRenovated = false;
            _photoUrls = new List<string>();
        }

        public Accommodation(string name,Owner owner,Location location, TYPE type,int maxGuestNum,int minNumOfDays,int allowedNumOfDaysForCancelation)
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
            _photoUrls = new List<string>();
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
                if(value != _ownerId)
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

        public int LocationId
        {
            get => _locationId;
            set
            {
                if(value != _locationId)
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

        public List<string> PhotoUrls
        {
            get => _photoUrls;
            set => _photoUrls = value;
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

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            OwnerId = int.Parse(values[1]);
            Name = values[2];
            Location.Id = int.Parse(values[3]); // vrv ne radi
            Type = Enum.Parse<TYPE>(values[4]);
            MaxGuestNum = int.Parse(values[5]);
            MinNumOfDays = int.Parse(values[6]);
            AllowedNumOfDaysForCancelation = int.Parse(values[7]);
            if (values[8].Length > 0)
            {
                string[] urls = values[8].Split(',');
                foreach(var url in urls)
                {
                    PhotoUrls.Add(url);
                }
            }
        }

        public string[] ToCSV()
        {
            string urls = "";
            List<string> uniqueUrls = PhotoUrls.Distinct().ToList();
            if(uniqueUrls.Count > 0)
            {
                for(int i = 0; i < uniqueUrls.Count; i++)
                {
                    if (i == 0)
                        urls += uniqueUrls[i];
                    else
                        urls += "," + uniqueUrls[i];
                }
            }

            string[] csvValues =
            {
                Id.ToString(),
                OwnerId.ToString(),
                Name,
                Location.Id.ToString(),
                Type.ToString(),
                MaxGuestNum.ToString(),
                MinNumOfDays.ToString(),
                AllowedNumOfDaysForCancelation.ToString(),
                urls
            };
            return csvValues;
        }
    }
}
