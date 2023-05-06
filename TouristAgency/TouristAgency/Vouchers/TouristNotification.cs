using System;
using System.ComponentModel;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.Vouchers
{
    public class TouristNotification : INotifyPropertyChanged, ISerializable
    {
        private int _Id;
        private int _touristID;
        private TouristNotificationType _type;
        private string _message;

        public event PropertyChangedEventHandler PropertyChanged;

        public TouristNotification()
        {
            _Id = -1;
            _touristID = -1;
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
                _type.ToString(),
                _message
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _Id = Convert.ToInt32(values[0]);
            _touristID = Convert.ToInt32(values[1]);
            _type = Enum.Parse<TouristNotificationType>(values[2]);
            _message = values[3];
        }
    }
}
