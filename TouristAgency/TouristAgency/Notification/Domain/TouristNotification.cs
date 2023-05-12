using System;
using System.ComponentModel;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Util;

namespace TouristAgency.Vouchers
{
    public class TouristNotification : INotifyPropertyChanged, ISerializable
    {
        private int _Id;
        private int _touristID;
        private int _tourID;
        private Tour _tour;
        private int _checkpointID;
        private Checkpoint _checkpoint;
        private TouristNotificationType _type;
        private string _message;

        public event PropertyChangedEventHandler PropertyChanged;

        public TouristNotification()
        {
            _Id = -1;
            _touristID = -1;
            _tourID = -1;
            _checkpointID = -1;
            _type = TouristNotificationType.MESSAGE;
        }

        public TouristNotification(int touristID, TouristNotificationType type, string message)
        {
            TouristID = touristID;
            Type = type;
            Message = message;
        }

        public int ID
        {
            get => _Id;
            set
            {
                if(value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged("ID");
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

        public int TourID
        {
            get => _tourID;
            set
            {
                if (value != _tourID)
                {
                    _tourID = value;
                    OnPropertyChanged("TourID");
                }
            }
        }

        public Tour Tour
        {
            get => _tour;
            set
            {
                if(value != _tour)
                {
                    _tour = value;
                    OnPropertyChanged("Tour");
                }
            }
        }


        public int CheckpointID
        {
            get => _checkpointID;
            set
            {
                if (value != _checkpointID)
                {
                    _checkpointID = value;
                    OnPropertyChanged("CheckpointID");
                }
            }
        }

        public Checkpoint Checkpoint
        {
            get => _checkpoint;
            set
            {
                if (value != _checkpoint)
                {
                    _checkpoint = value;
                    OnPropertyChanged("Checkpoint");
                }
            }
        }


        public TouristNotificationType Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                if (value != _message)
                {
                    _message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _Id.ToString(),
                _touristID.ToString(),
                _tourID.ToString(),
                _checkpointID.ToString(),
                _type.ToString(),
                _message
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _Id = Convert.ToInt32(values[0]);
            _touristID = Convert.ToInt32(values[1]);
            _tourID = Convert.ToInt32(values[2]);
            _checkpointID = Convert.ToInt32(values[3]);
            _type = Enum.Parse<TouristNotificationType>(values[4]);
            _message = values[5];
        }
    }
}
