using System;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.TourRequests
{
    public class TourRequestCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;

        private string _country;
        private string _city;
        private string _language;
        private ObservableCollection<TourRequest> _tourRequests;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _description;
        private int _numOfPeople;

        private TourRequestService _tourRequestService;
        private LocationService _locationService;

        public DelegateCommand CreateCmd { get; set; }

        public TourRequestCreationViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();
        }

        private void InstantiateCollections()
        {
            _tourRequestService.InvalidateOldTourRequests();
            _tourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetByTouristID(_loggedInTourist.ID));
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
        }

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

        public int NumOfPeople
        {
            get => _numOfPeople;
            set
            {
                if (_numOfPeople != value)
                {
                    _numOfPeople = value;
                    OnPropertyChanged("NumOfPeople");
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

        public ObservableCollection<TourRequest> TourRequests
        {
            get => _tourRequests;
            set
            {
                if (value != _tourRequests)
                {
                    _tourRequests = value;
                    OnPropertyChanged("TourRequests");
                }
            }
        }

        public bool CanCreateCmdExecute()
        {
            return true;
        }

        private void CreateCmdExecute()
        {
            TourRequest tourRequest = new TourRequest();
            tourRequest.TouristID = _loggedInTourist.ID;
            tourRequest.ShortLocation = new Location(Country, City);
            tourRequest.ShortLocation = _locationService.LocationRepository.Create(tourRequest.ShortLocation);
            tourRequest.ShortLocationID = tourRequest.ShortLocation.Id;
            tourRequest.Language = Language;
            tourRequest.MaxAttendance = NumOfPeople;
            tourRequest.Description = Description;
            tourRequest.StartDate = StartDate;
            tourRequest.EndDate = EndDate;
            _tourRequestService.TourRequestRepository.Create(tourRequest);
            TourRequests.Add(tourRequest);
            MessageBox.Show("You have successfully send request for tour.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}