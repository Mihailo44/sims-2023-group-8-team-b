using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.ForumFeatures.Domain
{
    public class Forum : INotifyPropertyChanged, ISerializable
    {
        private int _id;
        private string _name;
        private bool _useful;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Forum()
        {
            _id = -1;
            _useful = false;
        }

        public Forum(string name)
        {
            _name = name;
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
                    OnPropertyChanged("Name");
                }
            }
        }

        public bool Useful
        {
            get { return _useful; }
            set
            {
                if (_useful != value)
                {
                    _useful = value;
                    OnPropertyChanged("Useful");
                }
            }
        }

        public new void FromCSV(string[] values)
        {
            _id = Convert.ToInt32(values[0]);
            _name = values[1];
            _useful = bool.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Useful.ToString()
            };

            return csvValues;
        }
    }
}
