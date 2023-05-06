using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Users;

namespace TouristAgency.TourRequests
{
    public class TourRequestDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<TourRequest> _tourRequests;

        private string _country;
        private string _city;
        private string _language;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _maxAttendants;

        private TourRequestService _tourRequestService;

        public DelegateCommand CloseCmd { get; set; }

        public DelegateCommand FilterCmd { get; set; }

        public DelegateCommand ClearFilterCmd { get; set; }

        public DelegateCommand AcceptTourRequestCmd { get; set; }

        public TourRequestDisplayViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
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
            Country = "";
            City = "";
            Language = "";
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetPendingTourRequests());
            Countries = new ObservableCollection<String>(_tourRequestService.GetAllCountries());
            Cities = new ObservableCollection<String>(_tourRequestService.GetAllCities());
            Languages = new ObservableCollection<String>(_tourRequestService.GetAllLanguages());
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            AcceptTourRequestCmd = new DelegateCommand(param => AcceptTourRequestExecute(), param => CanAcceptTourRequestExecute());
            FilterCmd = new DelegateCommand(param => FilterExecute(), param => CanFilterExecute());
            ClearFilterCmd = new DelegateCommand(param => ClearFilterExecute(), param => CanClearFilterExecute());
        }


        public ObservableCollection<TourRequest> TourRequests
        {
            get { return _tourRequests; }
            set
            {
                if(value != _tourRequests)
                {
                    _tourRequests = value;
                    OnPropertyChanged("TourRequests");
                }
            }
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

        public int MaxAttendants
        {
            get => _maxAttendants;
            set
            {
                if (_maxAttendants != value)
                {
                    _maxAttendants = value;
                    OnPropertyChanged("MaxAttendants");
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

        public TourRequest SelectedTourRequest
        {
            get;
            set;
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public bool CanAcceptTourRequestExecute()
        {
            return true;
        }

        public void AcceptTourRequestExecute()
        {
            if(SelectedTourRequest != null)
                _app.CurrentVM = new TourCreationViewModel(SelectedTourRequest);
        }

        public bool CanFilterExecute()
        {
            return true;
        }

        public void FilterExecute()
        {
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.Search(Country, City, Language, MaxAttendants, StartDate, EndDate));
        }

        public bool CanClearFilterExecute()
        {
            return true;
        }

        public void ClearFilterExecute()
        {
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetPendingTourRequests());
            Country = "";
            City = "";
            Language = "";
        }

    }
}
