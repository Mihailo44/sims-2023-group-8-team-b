using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.TourRequests;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours.Domain
{
    public class TourRequestWrapper: INotifyPropertyChanged
    {
        TourRequest _regular;
        ComplexTourRequest _complex;
        TourRequestType _type;

        private Tourist _tourist;
        private string _name;
        private string _language;
        private string _description;
        private string _countries;
        private string _cities;
        private int _maxAttendants;
        private DateTime _startDate;
        private DateTime _endDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public TourRequestWrapper(TourRequest regular)
        {
            _type = TourRequestType.SINGLE;
            _regular = regular;
            _complex = null;
            Tourist = regular.Tourist;
            Name = Tourist.FirstName + " " + Tourist.LastName;
            Countries += regular.ShortLocation.Country;
            Cities += regular.ShortLocation.City;
            Language += regular.Language;
            MaxAttendance = regular.MaxAttendants;
            StartDate = regular.StartDate;
            EndDate = regular.EndDate;
        }

        public TourRequestWrapper(ComplexTourRequest complex)
        {
            _type = TourRequestType.MULTI;
            _regular = null;
            _complex = complex;
            Tourist = complex.Parts.First().Tourist;
            Name = Tourist.FirstName + " " + Tourist.LastName;
            Countries = GetCountriesFromComplex(complex);
            Cities = GetCitiesFromComplex(complex);
            Language = GetLanguagesFromComplex(complex);
            MaxAttendance = complex.Parts.First().MaxAttendants;
            StartDate = GetStartDateFromComplex(complex);
            EndDate = GetEndDateFromComplex(complex);
        }


        public string GetCitiesFromComplex(ComplexTourRequest complex)
        {
            string cities = "";
            foreach(TourRequest request in complex.Parts)
            {
                if(!cities.Contains(request.ShortLocation.City))
                {
                    cities+= request.ShortLocation.City + " ";
                }
            }
            return cities;
        }

        public string GetCountriesFromComplex(ComplexTourRequest complex)
        {
            string countries = "";
            foreach (TourRequest request in complex.Parts)
            {
                if (!countries.Contains(request.ShortLocation.City))
                {
                    countries += request.ShortLocation.City + " ";
                }
            }
            return countries;
        }

        public string GetLanguagesFromComplex(ComplexTourRequest complex)
        {
            string language = "";
            foreach (TourRequest request in complex.Parts)
            {
                if (!language.Contains(request.Language))
                {
                    language += request.Language + " ";
                }
            }
            return language;
        }

        public DateTime GetStartDateFromComplex(ComplexTourRequest complex)
        {
            DateTime startDate = complex.Parts.First().StartDate;
            foreach(TourRequest tourRequest in complex.Parts)
            {
                if(tourRequest.StartDate < startDate)
                {
                    startDate = tourRequest.StartDate;
                }
            }
            return startDate;
        }

        public DateTime GetEndDateFromComplex(ComplexTourRequest complex)
        {
            DateTime endDate = complex.Parts.First().EndDate;
            foreach(TourRequest tourRequest in complex.Parts)
            {
                if(tourRequest.EndDate > endDate)
                {
                    endDate = tourRequest.EndDate;
                }
            }
            return endDate;
        }

        public TourRequest Regular
        {
            get => _regular;
            set
            {
                if (value != _regular)
                {
                    _regular = value;
                    OnPropertyChanged("Regular");
                }
            }
        }

        public ComplexTourRequest Complex
        {
            get => _complex;
            set
            {
                if (value != _complex)
                {
                    _complex = value;
                    OnPropertyChanged("Complex");
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

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Countries
        {
            get => _countries;
            set
            {
                if (value != _countries)
                {
                    _countries = value;
                    OnPropertyChanged("Countries");
                }
            }
        }

        public string Cities
        {
            get => _cities;
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        public int MaxAttendance
        {
            get => _maxAttendants;
            set
            {
                if (value != _maxAttendants)
                {
                    _maxAttendants = value;
                    OnPropertyChanged("MaxAttedance");
                }
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public TourRequestType Type
        {
            get => _type;
            set => _type = value;
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
