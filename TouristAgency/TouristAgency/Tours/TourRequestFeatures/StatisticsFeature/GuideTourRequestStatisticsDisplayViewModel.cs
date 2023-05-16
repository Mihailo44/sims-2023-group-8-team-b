using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Tours;
using TouristAgency.Users;
using TouristAgency.Util;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.CreationFeature;

namespace TouristAgency.TourRequests.StatisticsFeature
{
    public class GuideTourRequestStatisticsDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        private string _country;
        private string _city;
        private string _language;
        private int _startYear;
        private int _endYear;
        private int _startMonth;
        private int _endMonth;
        private int _requestNum;
        private string _mostRequestedLocation;
        private int _mostRequestedNum;
        private TourRequest _mostRequestedTourRequest;

        TourRequestService _tourRequestService;
        public DelegateCommand CloseCmd { get; set; }

        public DelegateCommand FilterCmd { get; set; }

        public DelegateCommand ClearCmd { get; set; }

        public DelegateCommand CreateMostRequestedCmd { get; set; }

        public DelegateCommand CreateTourByFilterCmd { get; set; }

        public GuideTourRequestStatisticsDisplayViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
        }

        private void InstantiateServices()
        {
            _tourRequestService = new TourRequestService();
        }

        private void InstantiateCollections()
        {
            Languages = new ObservableCollection<string>(_tourRequestService.GetAllLanguages());
            Cities = new ObservableCollection<string>(_tourRequestService.GetAllCities());
            Countries = new ObservableCollection<string>(_tourRequestService.GetAllCountries());
            MostRequestedLocation = PrepareLocation();
            MostRequestedNum = _tourRequestService.GetMostRequested().Item2;
            _mostRequestedTourRequest = _tourRequestService.GetMostRequested().Item1;
            Language = Languages[0];
            City = Cities[0];
            Country = Countries[0];
            StartYear = DateTime.Now.Year;
            EndYear = DateTime.Now.Year;
            StartMonth = 1;
            EndMonth = 12;
            RequestNum = 0;
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            FilterCmd = new DelegateCommand(param => FilterExecute(), param => CanFilterExecute());
            ClearCmd = new DelegateCommand(param => ClearExecute(), param => CanClearExecute());
            CreateMostRequestedCmd = new DelegateCommand(param => CreateMostRequestedExecute(), param => CanCreateMostRequestedExecute());
            CreateTourByFilterCmd = new DelegateCommand(param => CreateTourByFilterExecute(), param => CanCreateTourByFilterExecute());
        }

        public string PrepareLocation()
        {
            Location location = _tourRequestService.GetMostRequested().Item1.ShortLocation;
            return location.City + ", " + location.Country;
        }
        public ObservableCollection<String> Countries { get; set; }

        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public ObservableCollection<String> Cities { get; set; }

        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public ObservableCollection<String> Languages { get; set; }

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

        public int StartYear
        {
            get => _startYear;
            set
            {
                if(value != _startYear)
                {
                    _startYear = value;
                    OnPropertyChanged("StartYear");
                }
            }
        }
        public int EndYear
        {
            get => _endYear;
            set
            {
                if (value != _endYear)
                {
                    _endYear = value;
                    OnPropertyChanged("EndYear");
                }
            }
        }

        public int StartMonth
        {
            get => _startMonth;
            set
            {
                if(value != _startMonth)
                {
                    _startMonth = value;
                    OnPropertyChanged("StartMonth");
                }
            }
        }

        public int EndMonth
        {
            get => _endMonth;
            set
            {
                if (value != _endMonth)
                {
                    _endMonth = value;
                    OnPropertyChanged("EndMonth");
                }
            }
        }

        public int RequestNum
        {
            get => _requestNum;
            set
            {
                if(value != _requestNum)
                {
                    _requestNum = value;
                    OnPropertyChanged("RequestNum");
                }
            }
        }

        public string MostRequestedLocation
        {
            get => _mostRequestedLocation;
            set
            {
                if(value != _mostRequestedLocation)
                {
                    _mostRequestedLocation = value;
                    OnPropertyChanged("MostRequestedLocation");
                }
            }
        }

        public int MostRequestedNum
        {
            get => _mostRequestedNum;
            set
            {
                if(value != _mostRequestedNum)
                {
                    _mostRequestedNum = value;
                    OnPropertyChanged("MostRequestedNum");
                }
            }
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public bool CanFilterExecute()
        {
            return true;
        }

        public void FilterExecute()
        {
            DateTime startDate = new DateTime(StartYear, StartMonth, 1);
            DateTime endDate = new DateTime(EndYear, EndMonth, 1);
            RequestNum = _tourRequestService.GetRequestNum(Country, City, Language, startDate, endDate);
        }

        public bool CanClearExecute()
        {
            return true;
        }

        public void ClearExecute()
        {
            InstantiateCollections();
        }

        public bool CanCreateMostRequestedExecute()
        {
            return true;
        }

        public void CreateMostRequestedExecute()
        {
            if(_mostRequestedTourRequest != null)
            {
                _app.CurrentVM = new TourCreationViewModel(_mostRequestedTourRequest, TourCreationScenario.MOST_POPULAR_TOURREQ);
            }
        }

        public bool CanCreateTourByFilterExecute()
        {
            return true;
        }

        public void CreateTourByFilterExecute()
        {
            DateTime start = new DateTime(StartYear, StartMonth, 1);
            DateTime end = new DateTime(EndYear, EndMonth, 1);
            List<TourRequest> potentials =  _tourRequestService.Search(Country, City, Language, start, end);
            potentials = potentials.FindAll(t => t.Status != TourRequestStatus.PENDING);
            if(potentials != null && potentials.Count != 0)
            {
                _app.CurrentVM = new TourCreationViewModel(potentials[0], TourCreationScenario.MOST_POPULAR_TOURREQ);
            }
        }

    }   
}
