using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.CreationFeature;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Tours.ComplexTourRequestFeatures.AcceptRequestFeature;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Tours.Domain;
using TouristAgency.Tours.TourRequestFeatures.Domain;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.TourRequests.AcceptRequestFeature
{
    public class TourRequestDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<TourRequestWrapper> _tourRequests;

        private string _country;
        private string _city;
        private string _language;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _maxAttendants;

        private TourRequestService _tourRequestService;
        private ComplexTourRequestService _complexTourRequestService;

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
            _complexTourRequestService = new ComplexTourRequestService();
            _tourRequestService.InvalidateOldTourRequests();
            _complexTourRequestService.InvalidateOldTourRequests();
        }

        private void InstantiateCollections()
        {
            Country = "";
            City = "";
            Language = "";
            TourRequests = new ObservableCollection<TourRequestWrapper>();
            LoadRequests();
            Countries = new ObservableCollection<String>(_tourRequestService.GetAllCountries());
            Cities = new ObservableCollection<String>(_tourRequestService.GetAllCities());
            Languages = new ObservableCollection<String>(_tourRequestService.GetAllLanguages());
        }

        private void LoadRequests()
        {
            foreach (TourRequest req in _tourRequestService.GetPendingTourRequests())
            {   if(req.ComplexTourRequestID == -1)
                    TourRequests.Add(new TourRequestWrapper(req));
            }
            foreach (ComplexTourRequest req in _complexTourRequestService.GetPending())
            {
                TourRequests.Add(new TourRequestWrapper(req));
            }
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            AcceptTourRequestCmd = new DelegateCommand(param => AcceptTourRequestExecute(), param => CanAcceptTourRequestExecute());
            FilterCmd = new DelegateCommand(param => FilterExecute(), param => CanFilterExecute());
            ClearFilterCmd = new DelegateCommand(param => ClearFilterExecute(), param => CanClearFilterExecute());
        }


        public ObservableCollection<TourRequestWrapper> TourRequests
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

        public TourRequestWrapper SelectedTourRequest
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
            if (SelectedTourRequest != null)
                if (SelectedTourRequest.Type == Util.TourRequestType.SINGLE)
                    _app.CurrentVM = new TourCreationViewModel(SelectedTourRequest.Regular, Util.TourCreationScenario.ACCEPT_TOURREQ);
                else
                    _app.CurrentVM = new ComplexTourRequestPartDisplayViewModel(SelectedTourRequest.Complex);
        }

        public bool CanFilterExecute()
        {
            return true;
        }

        public void FilterExecute()
        {
            TourRequests = new ObservableCollection<TourRequestWrapper>();
            foreach(TourRequest req in _tourRequestService.Search(Country, City, Language, MaxAttendants, StartDate, EndDate))
            {
                TourRequests.Add(new TourRequestWrapper(req));
            }
        }

        public bool CanClearFilterExecute()
        {
            return true;
        }

        public void ClearFilterExecute()
        {
            TourRequests = new ObservableCollection<TourRequestWrapper>();
            LoadRequests();
            Country = "";
            City = "";
            Language = "";
        }

    }
}
