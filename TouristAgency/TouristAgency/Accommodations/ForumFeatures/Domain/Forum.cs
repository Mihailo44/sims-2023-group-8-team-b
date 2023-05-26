using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.ForumFeatures.Domain
{
    public class Forum : INotifyPropertyChanged, ISerializable
    {
        private int _id;
        private string _name;
        private Location _location;
        private int _locationId;
        private bool _useful;

        public Forum()
        {
            _id = -1;
            _useful = false;
        }

        public Forum(string name,Location location)
        {
            _id = -1;
            _name = name;
            _location = location;
            _locationId = location.Id;
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

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public Location Location
        {
            get { return _location; }
            set
            {
                if(_location != value)
                {
                    _location = value;
                }
            }
        }

        public int LocationId
        {
            get { return _locationId; }
            set
            {
                if(_locationId != value)
                {
                    _locationId = value;
                }
            }
        }

        public bool IsUseful
        {
            get { return _useful; }
            set
            {
                if (_useful != value)
                {
                    _useful = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            IsUseful = bool.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                IsUseful.ToString()
            };

            return csvValues;
        }
    }
}
