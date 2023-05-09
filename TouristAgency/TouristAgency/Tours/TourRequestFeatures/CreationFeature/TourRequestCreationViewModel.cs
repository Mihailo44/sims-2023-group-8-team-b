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
        private TourRequest _tourRequest;

        private TourRequestService _tourRequestService;
        private LocationService _locationService;

        public DelegateCommand CreateCmd { get; set; }

        public TourRequestCreationViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _tourRequest = new TourRequest();
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

        public TourRequest TourRequest
        {
            get => _tourRequest;
            set
            {
                if (value != _tourRequest)
                {
                    _tourRequest = value;
                    OnPropertyChanged("TourRequest");
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
            TourRequest.TouristID = _loggedInTourist.ID;
            TourRequest.ShortLocation = _locationService.LocationRepository.Create(TourRequest.ShortLocation);
            if(TourRequest.IsValid)
            {
                _tourRequestService.TourRequestRepository.Create(TourRequest);
                TourRequests.Add(TourRequest);
                MessageBox.Show("You have successfully send request for tour.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid request");
            }
        }
    }
}