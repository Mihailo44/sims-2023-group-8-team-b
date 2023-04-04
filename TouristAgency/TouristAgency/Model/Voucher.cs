using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Voucher : INotifyPropertyChanged, ISerializable
    {
        private int _ID;
        private int _touristID;
        private string _name;
        private bool _isUsed;
        private DateTime _expirationDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public Voucher() 
        {

        }

        public Voucher(int touristId, bool isUsed, DateTime expirationDate)
        {
            _touristID = touristId;
            _isUsed = isUsed;
            _expirationDate = expirationDate;
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
            get { return _ID; }
            set
            {
                if (value != _ID)
                {
                    _touristID = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        public int TouristID
        {
            get { return _touristID; }
            set
            {
                if (value != _touristID)
                {
                    _touristID = value;
                    OnPropertyChanged("TouristID");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public bool IsUsed
        {
            get { return _isUsed; }
            set
            {
                if (value != _isUsed)
                {
                    _isUsed = value;
                    OnPropertyChanged("IsUsed");
                }
            }
        }

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                if (value != _expirationDate)
                {
                    _expirationDate = value;
                    OnPropertyChanged("ExpirationDate");
                }
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _touristID.ToString(),
                _name,
                _isUsed.ToString(),
                _expirationDate.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _touristID = Convert.ToInt32(values[1]);
            _name = Convert.ToString(values[2]);
            _isUsed = Convert.ToBoolean(values[3]);
            _expirationDate = Convert.ToDateTime(values[4]);
        }
    }
}
