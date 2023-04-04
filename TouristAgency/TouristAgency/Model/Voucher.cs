using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    public class Voucher : INotifyPropertyChanged
    {
        private int _touristID;
        private bool _isUsed;
        private DateTime _expirationDate;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Voucher(int touristId, bool isUsed, DateTime expirationDate)
        {
            _touristID = touristId;
            _isUsed = isUsed;
            _expirationDate = expirationDate;
        }

        public int TouristID
        {
            get { return _touristID; }
            set
            {
                if (value != _touristID)
                {
                    _touristID = value;
                    OnPropertyChanged("Status");
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

    }
}
