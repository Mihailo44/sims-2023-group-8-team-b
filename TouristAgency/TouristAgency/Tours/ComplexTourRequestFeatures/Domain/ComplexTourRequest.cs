using System;
using System.Collections.Generic;
using System.ComponentModel;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.Domain
{
    public class ComplexTourRequest : INotifyPropertyChanged, ISerializable
    {
        private int _ID;
        private string _name;
        private int _touristID;
        private Tourist _tourist;
        private List<TourRequest> _parts;
        private ComplexTourRequestStatus _status;
        private TourRequestType _type;

        public event PropertyChangedEventHandler PropertyChanged;

        public ComplexTourRequest() 
        {
            _ID = -1;
            _parts = new List<TourRequest>();
            _status = ComplexTourRequestStatus.PENDING;
        }

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
                    OnPropertyChanged("ID");
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
                    OnPropertyChanged("Name");
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

        public List<TourRequest> Parts
        {
            get => _parts;
            set
            {
                if (_parts != value) 
                {
                    _parts = value;
                    OnPropertyChanged("Parts");
                }
            }
        }

        public ComplexTourRequestStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public TourRequestType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _name,
                _touristID.ToString(),
                _status.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _name = values[1];
            _touristID = Convert.ToInt32(values[2]);
            _status = Enum.Parse<ComplexTourRequestStatus>(values[3]);
        }

        public void InvalidateAll()
        {
            foreach(TourRequest req in Parts)
            {
                req.Status = TourRequestStatus.INVALID;
            }
        }
    }
}
