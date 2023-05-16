using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Util;
using TouristAgency.Vouchers;

namespace TouristAgency.CreationFeature
{
    public class TourCreationViewModel : BurgerMenuViewModelBase, ICreate, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<Checkpoint> _availableCheckpoints;
        private ObservableCollection<Checkpoint> _selectedCheckpoints;
        private List<DateTime> _multipleDateTimes;

        private int _datecount;
        private Tour _newTour;
        private TourRequest _tourRequest;
        private Location _newLocation;
        private string _photoLinks;
        private TourCreationScenario _scenario;

        private TourService _tourService;
        private LocationService _locationService;
        private CheckpointService _checkpointService;
        private TourCheckpointService _tourCheckpointService;
        private TourRequestService _tourRequestService;
        private TouristNotificationService _touristNotificationService;
        public DelegateCommand AddCheckpointCmd { get; set; }
        public DelegateCommand RemoveCheckpointCmd { get; set; }
        public DelegateCommand AddMultipleDatesCmd { get; set; }
        public DelegateCommand RemoveMultipleDatesCmd { get; set; }
        public DelegateCommand LoadCheckpointsIntoListViewCmd { get; set; }
        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }

        public TourCreationViewModel(TourRequest tourRequest = null, TourCreationScenario scenario = TourCreationScenario.DEFAULT)
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            _tourRequest = tourRequest;
            _scenario = scenario;
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
            FillFromTourRequest(tourRequest);
            LoadCheckpointsIntoListView();
            ChangeControlEnabledStatus(scenario);
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            _checkpointService = new CheckpointService();
            _tourCheckpointService = new TourCheckpointService();
            _tourRequestService = new TourRequestService();
            _touristNotificationService = new TouristNotificationService();
        }

        private void InstantiateCollections()
        {
            NewTour = new Tour();
            NewLocation = new Location();
            AvailableCheckpoints = new ObservableCollection<Checkpoint>();
            SelectedCheckpoints = new ObservableCollection<Checkpoint>();
            _multipleDateTimes = new List<DateTime>();
            _datecount = _multipleDateTimes.Count;
        }

        private void InstantiateCommands()
        {
            AddCheckpointCmd =
                    new DelegateCommand(param => AddCheckpointsExecute(), param => CanAddCheckpointsExecute());
            RemoveCheckpointCmd = new DelegateCommand(param => RemoveCheckpointsExecute(),
                param => CanRemoveCheckpointsExecute());
            AddMultipleDatesCmd =
                new DelegateCommand(param => AddMultipleDatesExecute(), param => CanAddMultipleDatesExecute());
            RemoveMultipleDatesCmd = new DelegateCommand(param => RemoveMultipleDatesExecute(),
                param => CanRemoveMultipleDatesExecute());
            LoadCheckpointsIntoListViewCmd = new DelegateCommand(param => LoadCheckpointsIntoListView(),
                param => CanLoadCheckpointsIntoListView());
            CreateCmd = new DelegateCommand(param => CreateTourExecute(), param => CanCreateTourExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public void FillFromTourRequest(TourRequest tourRequest)
        {
            if (tourRequest != null)
            {
                NewTour.ShortLocation = tourRequest.ShortLocation;
                NewLocation = tourRequest.ShortLocation;
                NewTour.ShortLocation.City = tourRequest.ShortLocation.City;
                NewTour.Language = tourRequest.Language;
                if(_scenario == TourCreationScenario.ACCEPT_TOURREQ)
                {
                    NewTour.Description = tourRequest.Description;
                    NewTour.MaxAttendants = tourRequest.MaxAttendants;
                }
            }
        }

        public void ChangeControlEnabledStatus(TourCreationScenario scenario)
        {
            if(scenario == TourCreationScenario.DEFAULT)
            {
                CountryEnabled = "true";
                CityEnabled = "true";
                LanguageEnabled = "true";
                CapacityEnabled = "true";
                DescriptionEnabled = "true";
            }
            else if (scenario == TourCreationScenario.ACCEPT_TOURREQ)
            {
                CountryEnabled = "false";
                CityEnabled = "false";
                LanguageEnabled = "false";
                CapacityEnabled = "false";
                DescriptionEnabled = "false";
            }
            else
            {
                CountryEnabled = "false";
                CityEnabled = "false";
                LanguageEnabled = "false";
                CapacityEnabled = "true";
                DescriptionEnabled = "true";
            }
        }

        public Tour NewTour
        {
            get => _newTour;
            set => _newTour = value;
        }

        public Location NewLocation
        {
            get => _newLocation;
            set => _newLocation = value;
        }

        public int DateCount
        {
            get => _datecount;
            set
            {
                if (value != _datecount)
                {
                    _datecount = value;
                    OnPropertyChanged("DateCount");
                }
            }
        }

        public ObservableCollection<Checkpoint> AvailableCheckpoints
        {
            get => _availableCheckpoints;
            set
            {
                if (value != _availableCheckpoints)
                {
                    _availableCheckpoints = value;
                    OnPropertyChanged("AvailableCheckpoints");
                }
            }
        }

        public ObservableCollection<Checkpoint> SelectedCheckpoints
        {
            get => _selectedCheckpoints;
            set
            {
                if (value != _selectedCheckpoints)
                {
                    _selectedCheckpoints = value;
                    OnPropertyChanged("SelectedCheckpoints");
                }
            }
        }

        public string PhotoLinks
        {
            get => _photoLinks;
            set => _photoLinks = value;
        }

        public string CountryEnabled { get; set; }
        public string CityEnabled { get; set; }
        public string DescriptionEnabled { get; set; }
        public string LanguageEnabled { get; set; }
        public string CapacityEnabled { get; set; }

        public bool CanLoadCheckpointsIntoListView()
        {
            return true;
        }

        public void LoadCheckpointsIntoListView()
        {
            SelectedCheckpoints = new ObservableCollection<Checkpoint>();
            if (NewLocation.Country != "" && NewLocation.City != "")
            {
                AvailableCheckpoints =
               new ObservableCollection<Checkpoint>(_checkpointService.GetSuitableByLocation(NewLocation));
            }
        }

        public void PrepareLocation()
        {
            int locationID = _locationService.FindLocationId(NewLocation);
            NewLocation.Id = locationID;

            if (locationID == -1)
            {
                _locationService.Create(NewLocation);
            }

            _newTour.ShortLocation = NewLocation;
            _newTour.ShortLocationID = NewLocation.Id;
        }

        public void AddPhotos()
        {
            int tourID = _tourService.TourRepository.GenerateId() - 1;
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'T', tourID);
                    _newTour.Photos.Add(photo);
                    _app.PhotoRepository.Create(photo);
                }
            }
        }

        public bool HandleTourRequest(DateTime startDate)
        {
            if (_tourRequest != null)
            {
                if ((startDate.Date < _tourRequest.StartDate.Date || startDate.Date > _tourRequest.EndDate.Date) && _scenario == TourCreationScenario.ACCEPT_TOURREQ)
                {
                    MessageBox.Show("The dates must be in range of tour request (" + _tourRequest.StartDate.ToShortDateString() + " - " + _tourRequest.EndDate.ToShortDateString() + ")");
                    return false;
                }
                _tourRequest.Status = TourRequestStatus.ACCEPTED;
                _tourRequest.GuideID = _loggedInGuide.ID;
                _tourRequestService.Update(_tourRequest, _tourRequest.ID);
                return true;
            }
            else
                return false;
        }

        public void LoadCheckpointsToTours()
        {
            int tourID = _tourService.TourRepository.GenerateId() - 1;
            int i = 0;
            bool firstVisit = true;
            foreach (Checkpoint checkpoint in SelectedCheckpoints)
            {
                if (i != 0)
                {
                    firstVisit = false;
                }

                NewTour.Checkpoints.Add(checkpoint);
                _tourCheckpointService.Create(new TourCheckpoint(tourID, checkpoint.ID, firstVisit));
                i++;
            }
        }

        public new bool CanCreateTourExecute()
        {
            return true;
        }

        public new void CreateTourExecute()
        {
            //TODO Implementirati proveru da li postoji vec slika u PhotoRepository!
            if (SelectedCheckpoints.Count < 2)
            {
                MessageBox.Show("Must have selected at least 2 checkpoints!");
                return;
            }

            foreach (DateTime dateTime in _multipleDateTimes)
            {
                PrepareLocation();
                NewTour.AssignedGuideID = _loggedInGuide.ID;
                NewTour.AssignedGuide = _loggedInGuide;
                NewTour.StartDateTime = dateTime;
                NewTour.RemainingCapacity = NewTour.MaxAttendants;
                bool canHandleTourRequest = HandleTourRequest(dateTime);
                if (NewTour.IsValid)
                {
                    AddPhotos();
                    LoadCheckpointsToTours();
                    if (canHandleTourRequest && _scenario != TourCreationScenario.DEFAULT)
                    {
                        if (_scenario != TourCreationScenario.MOST_POPULAR_TOURREQ)
                        {
                            TouristNotification notification = new TouristNotification(_tourRequest.TouristID, TouristNotificationType.TOUR_REQUEST_ACCEPTED, "Tour request accepted: " + NewTour.Name);
                            notification.Tour = NewTour;
                            notification.TourID = NewTour.ID;
                            _touristNotificationService.Create(notification);
                        }
                        _touristNotificationService.NotifyAboutNewTour(NewTour, _tourRequestService.GetInvalidTourRequests());
                        _touristNotificationService.NotifyAboutNewTour(NewTour, _tourRequestService.GetAcceptedTourRequests());
                        NewTour = _tourService.Create(new Tour(_newTour));
                        MessageBox.Show("Successfully created tour!", "Success");
                    }
                    else if(!canHandleTourRequest && _scenario == TourCreationScenario.DEFAULT)
                    {
                        NewTour = _tourService.Create(new Tour(_newTour));
                        _touristNotificationService.NotifyAboutNewTour(NewTour, _tourRequestService.GetInvalidTourRequests());
                        _touristNotificationService.NotifyAboutNewTour(NewTour, _tourRequestService.GetAcceptedTourRequests());
                        MessageBox.Show("Successfully created tour!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("An error occured!");
                    }
                }
            }
        }

        public bool CanAddCheckpointsExecute()
        {
            return true;
        }

        public void AddCheckpointsExecute()
        {
            foreach (Checkpoint checkpoint in AvailableCheckpoints)
            {
                if (!SelectedCheckpoints.Contains(checkpoint) && checkpoint.IsSelected == true)
                {
                    SelectedCheckpoints.Add(checkpoint);
                }
            }
        }

        public bool CanRemoveCheckpointsExecute()
        {
            return true;
        }

        public void RemoveCheckpointsExecute()
        {
            List<Checkpoint> checkpointsToDelete = new List<Checkpoint>();
            foreach (Checkpoint checkpoint in SelectedCheckpoints)
            {
                if (checkpoint.IsSelected == true)
                    checkpointsToDelete.Add(checkpoint);
            }

            foreach (Checkpoint checkpoint in checkpointsToDelete)
            {
                SelectedCheckpoints.Remove(checkpoint);
            }

            checkpointsToDelete.Clear();
        }

        public bool CanAddMultipleDatesExecute()
        {
            return true;
        }

        public void AddMultipleDatesExecute()
        {
            _multipleDateTimes.Add(NewTour.StartDateTime);
            DateCount = _multipleDateTimes.Count;
        }

        public bool CanRemoveMultipleDatesExecute()
        {
            return true;
        }

        public void RemoveMultipleDatesExecute()
        {
            _multipleDateTimes.Remove(NewTour.StartDateTime);
            DateCount = _multipleDateTimes.Count;
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }
    }
}