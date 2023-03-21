using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Location : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        private int _ID;
        private string _street;
        private string _streetNumber;
        private string _city;
        private string _country;
        private int _reservedAccommodationsNum = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        public Location()
        {
            _ID = -1;
            _street = "";
            _streetNumber = "";
            _city = "";
            _country = "";
        }

        public Location(string country, string city, string street = "", string streetNumber = "")
        {
            _street = street;
            _streetNumber = streetNumber;
            _city = city;
            _country = country;
        }

        public Location(Location originalLocation)
        {
            _ID = originalLocation._ID;
            _street = originalLocation.Street;
            _streetNumber = originalLocation.StreetNumber;
            _city = originalLocation.City;
            _country = originalLocation.Country;
        }

        public int Id
        {
            get { return _ID; }
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                }
            }
        }

        public string Street
        { 
            get { return _street; }
            set
            {
                if (value != _street)
                {
                    _street = value;
                    OnPropertyChanged("Street");
                }
            }
        }

        public string StreetNumber
        { 
            get { return _streetNumber; }
            set
            {
                if (value != _streetNumber)
                {
                    _streetNumber = value;
                    OnPropertyChanged("StreetNumber");
                }
            }
        }

        public string City 
        { 
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public string Country 
        { 
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }
        public int ReservedAccommodationsNum
        {
            get => _reservedAccommodationsNum;
            set
            {
                if (_reservedAccommodationsNum != value)
                {
                    _reservedAccommodationsNum = value;
                }
            }
        }

        public override bool Equals(object obj)
        {
            Location newLocation = (Location)obj;
            if (newLocation.Street == "" && newLocation.StreetNumber == "")
            {
                return this.City.ToLower() == newLocation.City.ToLower() && this.Country.ToLower() == newLocation.Country.ToLower();
            }
            else
            {
                bool country = Country.ToLower() == newLocation.Country.ToLower();
                bool city = City.ToLower() == newLocation.City.ToLower();
                bool street = Street.ToLower() == newLocation.Street.ToLower();
                bool streetNum = StreetNumber.ToLower() == newLocation.StreetNumber.ToLower();
                return country && city && street && streetNum;
            }
        }


        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))
                        return "Required field";
                }
                else if (columnName == "StreetNumber")
                {
                    if (string.IsNullOrEmpty(StreetNumber))
                        return "Required field";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "Required field";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Required field";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "Street", "StreetNumber", "City", "Country" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _street,
                _streetNumber,
                _city,
                _country
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _street = values[1];
            _streetNumber = values[2];
            _city = values[3];
            _country = values[4];
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
