using System;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Tours.DetailsFeature;
using TouristAgency.Tours.TourRequestFeatures.Domain;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours.TourRequestFeatures.CreationFeature
{
    public class ComplexTourRequestCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;

        private ComplexTourRequestService _complexTourRequestService;
        private TourRequestService _tourRequestService;
        private LocationService _locationService;

        private TourRequest _newTourRequest;
        private ComplexTourRequest _newComplexTourRequest;
        private ObservableCollection<TourRequest> _parts;

        private string _validationError;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand AddPartCmd { get; set; }
        public DelegateCommand RemovePartCmd { get; set; }
        public DelegateCommand ListOfPartsCmd { get; set; }
        public DelegateCommand DetailsCmd { get; set; }

        public ComplexTourRequestCreationViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _newTourRequest = new TourRequest();
            _newComplexTourRequest = new ComplexTourRequest();
            ValidationError = "Hidden";

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        public void InstantiateServices()
        {
            _complexTourRequestService = new ComplexTourRequestService();
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();
        }

        public void InstantiateCollections()
        {
            Parts = new ObservableCollection<TourRequest>();
        }

        public void InstantiateCommands()
        {
            AddPartCmd = new DelegateCommand(param => AddPartExecute(), param => CanAddPartExecute());
            RemovePartCmd = new DelegateCommand(param => RemovePartExecute(), param => CanRemovePartExecute());
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
            DetailsCmd = new DelegateCommand(param => DetailsExecute(), param => CanDetailsExecute());
        }

        public TourRequest NewTourRequest
        {
            get => _newTourRequest;
            set
            {
                if (value != _newTourRequest)
                {
                    _newTourRequest = value;
                    OnPropertyChanged("NewTourRequest");
                }
            }
        }

        public ComplexTourRequest NewComplexTourRequest
        {
            get => _newComplexTourRequest;
            set
            {
                if (value != _newComplexTourRequest)
                {
                    _newComplexTourRequest = value;
                    OnPropertyChanged("NewComplexTourRequest");
                }
            }
        }

        public ObservableCollection<TourRequest> Parts
        {
            get => _parts;
            set
            {
                if (value != _parts)
                {
                    _parts = value;
                    OnPropertyChanged("Parts");
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

        public TourRequest SelectedPart
        {
            get;
            set;
        }

        public bool CanAddPartExecute()
        {
            return true;
        }

        public void AddPartExecute()
        {
            NewTourRequest.ValidationClear();
            NewTourRequest.ValidateSelf();
            NewComplexTourRequest.ValidationClear();
            NewComplexTourRequest.ValidateSelf();
            if (NewTourRequest.IsValid && NewComplexTourRequest.IsValid)
            {
                TimeSpan ts = NewTourRequest.StartDate - DateTime.Today;
                if (NewTourRequest.EndDate < NewTourRequest.StartDate)
                {
                    MessageBox.Show("End date cannot be before a start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ts.TotalHours <= 48)
                {
                    MessageBox.Show("Cannot create request 48 hours before start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ts.TotalHours > 48)
                {
                    ValidationError = "Hidden";
                    Parts.Add(NewTourRequest);
                    NewTourRequest = new TourRequest();
                }
            }
            else
            {
                ValidationError = "Visible";
                MessageBox.Show("Invalid request, check your inputs and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanRemovePartExecute()
        {
            return true;
        }

        public void RemovePartExecute()
        {
            if(SelectedPart != null)
            {
                var result = MessageBox.Show("Are you sure you want to remove this part?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    Parts.Remove(SelectedPart);
                    MessageBox.Show("You have successfully removed this part.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public bool CanCreateExecute()
        {
            return true;
        }

        public void CreateExecute()
        {
            NewComplexTourRequest.ValidationClear();
            NewComplexTourRequest.ValidateSelf();
            if(NewComplexTourRequest.IsValid)
            {
                ValidationError = "Hidden";
                if (Parts.Count >= 2)
                {
                    NewComplexTourRequest.TouristID = _loggedInTourist.ID;
                    NewComplexTourRequest = _complexTourRequestService.Create(NewComplexTourRequest);
                    foreach (TourRequest request in Parts)
                    {
                        request.TouristID = _loggedInTourist.ID;
                        request.Tourist = _loggedInTourist;
                        request.ComplexTourRequestID = NewComplexTourRequest.ID;
                        Location location = _locationService.FindByCountryAndCity(request.ShortLocation.Country, request.ShortLocation.City);
                        if (location == null)
                        {
                            location = _locationService.Create(request.ShortLocation);
                        }
                        request.ShortLocationID = location.ID;
                        TourRequest tempRequest = _tourRequestService.Create(request);
                        request.ID = tempRequest.ID;
                        NewComplexTourRequest.Parts.Add(request);
                    }
                    MessageBox.Show("You have successfully created complex tour request.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Parts.Clear();
                    NewTourRequest = new();
                    NewComplexTourRequest = new();
                }
                else
                {
                    MessageBox.Show("A complex tour must have at least two parts.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                ValidationError = "Visible";
                MessageBox.Show("Invalid request, check your inputs and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanDetailsExecute()
        {
            return true;
        }

        public void DetailsExecute()
        {
            if(SelectedPart != null)
            {
                RequestPartDetailsDisplay display = new RequestPartDetailsDisplay(NewComplexTourRequest.Name, SelectedPart);
                display.Show();
            }
        }
    }
}
