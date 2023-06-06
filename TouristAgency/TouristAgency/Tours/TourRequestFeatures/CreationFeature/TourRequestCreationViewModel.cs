using System;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours.TourRequestFeatures.DisplayFeature;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.TourRequests
{
    public class TourRequestCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;

        private ObservableCollection<TourRequest> _tourRequests;
        private TourRequest _tourRequest;

        private TourRequestService _tourRequestService;
        private LocationService _locationService;

        private string _validationError;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand ListOfTourRequestsCmd { get; set; }

        public TourRequestCreationViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _tourRequest = new TourRequest();
            _loggedInTourist = tourist;
            ValidationError = "Hidden";

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
            ListOfTourRequestsCmd = new DelegateCommand(param => ListOfTourRequestsExecute(), param => CanListOfTourRequestsExecute());
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

        public string ValidationError
        {
            get => _validationError;
            set
            {
                if (value != _validationError)
                {
                    _validationError = value;
                    OnPropertyChanged("ValidationError");
                }
            }
        }

        public bool CanCreateCmdExecute()
        {
            return true;
        }

        private void CreateCmdExecute()
        {
            TourRequest.ValidationClear();
            TourRequest.ValidateSelf();
            if(TourRequest.IsValid)
            {
                ValidationError = "Hidden";
                TourRequest.TouristID = _loggedInTourist.ID;
                TourRequest.ShortLocation = _locationService.LocationRepository.Create(TourRequest.ShortLocation);
                TourRequest.ShortLocationID = TourRequest.ShortLocation.ID;
                TimeSpan ts = TourRequest.StartDate - DateTime.Today;
                if (TourRequest.EndDate < TourRequest.StartDate)
                {
                    MessageBox.Show("End date cannot be before a start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ts.TotalHours <= 48)
                {
                    MessageBox.Show("Cannot create request 48 hours before start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TourRequest.IsValid && ts.TotalHours > 48)
                {
                    _tourRequestService.TourRequestRepository.Create(TourRequest);
                    TourRequests.Add(TourRequest);
                    TourRequest = new TourRequest();
                    MessageBox.Show("You have successfully send request for tour.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            else
            {
                ValidationError = "Visible";
                MessageBox.Show("Invalid request, check your inputs and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanListOfTourRequestsExecute()
        {
            return true;
        }

        public void ListOfTourRequestsExecute()
        {
            TourRequestDisplay display = new TourRequestDisplay(_loggedInTourist);
            display.Show();
        }
    }
}