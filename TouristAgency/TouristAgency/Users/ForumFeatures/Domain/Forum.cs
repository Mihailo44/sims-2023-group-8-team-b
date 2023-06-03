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

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class Forum : INotifyPropertyChanged, ISerializable
    {
        private int _id;
        private string _name;
        private Location _location;
        private bool _useful;
        private DateTime _created;

        public Forum()
        {
            _id = -1;
            _created = DateTime.Today;
        }

        public Forum(string name,Location location)
        {
            _id = -1;
            _name = name;
            _location = location;
            _created = DateTime.Today;
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

        public DateTime Created
        {
            get { return _created; }
            set
            {
                if(_created != value)
                {
                    _created = value;
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
            Location = new();

            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location.ID = Convert.ToInt32(values[2]);
            Created = DateTime.Parse(values[3]);
            IsUseful = bool.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Location.ID.ToString(),
                Created.ToString(),
                IsUseful.ToString()
            };

            return csvValues;
        }
    }
}
