using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Notifications.Domain;
using TouristAgency.TourRequests;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Util;

namespace TouristAgency.CreationFeature
{
    public class TourCreationViewModel : BurgerMenuViewModelBase, ICreate, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<Checkpoint> _availableCheckpoints;
        private ObservableCollection<Checkpoint> _selectedCheckpoints;
        private ObservableCollection<DateWrapper> _multipleDates;
        private ObservableCollection<Photo> _photos;

        private int _datecount;
        private Tour _newTour;
        private TourRequest _tourRequest;
        private Location _newLocation;
        private TourCreationScenario _scenario;
        private List<string> _paths;
        private string _photoLinks;

        private TourService _tourService;
        private LocationService _locationService;
        private CheckpointService _checkpointService;
        private TourCheckpointService _tourCheckpointService;
        private TourRequestService _tourRequestService;
        private TouristNotificationService _touristNotificationService;
        private PhotoService _photoService;
        public DelegateCommand AddCheckpointCmd { get; set; }
        public DelegateCommand RemoveCheckpointCmd { get; set; }
        public DelegateCommand AddMultipleDatesCmd { get; set; }
        public DelegateCommand RemoveMultipleDatesCmd { get; set; }
        public DelegateCommand LoadCheckpointsIntoListViewCmd { get; set; }
        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand LoadPhotoLinksCmd { get; set; }

        public DelegateCommand DeletePhotoCmd { get; set; }

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
            _photoService = new PhotoService();
        }

        private void InstantiateCollections()
        {
            NewTour = new Tour();
            NewLocation = new Location();
            AvailableCheckpoints = new ObservableCollection<Checkpoint>();
            SelectedCheckpoints = new ObservableCollection<Checkpoint>();
            _multipleDates = new ObservableCollection<DateWrapper>();
            _photos = new ObservableCollection<Photo>();
            _datecount = _multipleDates.Count;
        }

        private void InstantiateCommands()
        {
            AddCheckpointCmd =
                    new DelegateCommand(param => AddCheckpointsExecute(), param => AlwaysExecutes());
            RemoveCheckpointCmd = new DelegateCommand(param => RemoveCheckpointsExecute(),
                param => AlwaysExecutes());
            AddMultipleDatesCmd =
                new DelegateCommand(param => AddMultipleDatesExecute(), param => AlwaysExecutes());
            RemoveMultipleDatesCmd = new DelegateCommand(RemoveMultipleDatesExecute, CanRemoveMultipleDatesExecute);
            LoadCheckpointsIntoListViewCmd = new DelegateCommand(param => LoadCheckpointsIntoListView(),
                param => AlwaysExecutes());
            CreateCmd = new DelegateCommand(param => CreateTourExecute(), param => AlwaysExecutes());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => AlwaysExecutes());
            LoadPhotoLinksCmd = new DelegateCommand(param => LoadPhotoLinksExecute(), param => AlwaysExecutes());
            DeletePhotoCmd = new DelegateCommand(DeletePhotoExecute, CanDeletePhotoExecute);
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

        public ObservableCollection<DateWrapper> MultipleDates
        {
            get => _multipleDates;
            set
            {
                if (value != _multipleDates)
                {
                    _multipleDates = value;
                    OnPropertyChanged("MultipleDates");
                }
            }
        }

        public ObservableCollection<Photo> Photos
        {
            get => _photos;
            set
            {
                if (value != _photos)
                {
                    _photos = value;
                    OnPropertyChanged("Photos");
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
            NewLocation.ID = locationID;

            if (locationID == -1)
            {
                _locationService.Create(NewLocation);
            }

            _newTour.ShortLocation = NewLocation;
            _newTour.ShortLocationID = NewLocation.ID;
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
                    Photo photo = new Photo(photoLink, 'T', NewTour.ID);
                    _newTour.Photos.Add(photo);
                    //_app.PhotoRepository.Create(photo);
                    _photoService.Create(photo);
                }
            }
        }

        public void AddPhotos(List<string> paths)
        {

            /*int tourID = _tourService.TourRepository.GenerateId() - 1;
            if (tourID == -1)
                tourID = 0;*/
            _newTour.Photos.Clear();
            foreach (string photoLink in paths)
            {
                Photo photo = new Photo(photoLink, 'T', NewTour.ID);
                _newTour.Photos.Add(photo);
                //_app.PhotoRepository.Create(photo);
                _photoService.Create(photo);
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

        public new void CreateTourExecute()
        {
            //TODO Implementirati proveru da li postoji vec slika u PhotoRepository!
            if (SelectedCheckpoints.Count < 2)
            {
                MessageBox.Show("Must have selected at least 2 checkpoints!");
                return;
            }

            foreach (DateWrapper dateWrapper in _multipleDates)
            {
                PrepareLocation();
                NewTour.AssignedGuideID = _loggedInGuide.ID;
                NewTour.AssignedGuide = _loggedInGuide;
                NewTour.StartDateTime = dateWrapper.Date;
                NewTour.RemainingCapacity = NewTour.MaxAttendants;
                bool passed = false;
                bool canHandleTourRequest = HandleTourRequest(dateWrapper.Date);
                if (NewTour.IsValid)
                {
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

                        if (_paths != null)
                            AddPhotos(_paths);

                        else if (PhotoLinks != null && PhotoLinks != "")
                            AddPhotos();

                        if (!passed)
                            MessageBox.Show("Successfully created tour!", "Success");
                        passed = true;
                    }
                    else if(!canHandleTourRequest && _scenario == TourCreationScenario.DEFAULT)
                    {
                        NewTour = _tourService.Create(new Tour(_newTour));

                        if (_paths != null)
                            AddPhotos(_paths);

                        else if (PhotoLinks != null && PhotoLinks != "")
                            AddPhotos();

                        _touristNotificationService.NotifyAboutNewTour(NewTour, _tourRequestService.GetInvalidTourRequests());
                        _touristNotificationService.NotifyAboutNewTour(NewTour, _tourRequestService.GetAcceptedTourRequests());
                        
                        if(!passed)
                            MessageBox.Show("Successfully created tour!", "Success");
                        passed = true;
                    }
                    else
                    {
                        MessageBox.Show("An error occured!");
                        return;
                    }
                }
            }
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

        public void AddMultipleDatesExecute()
        {
            _multipleDates.Add(new(NewTour.StartDateTime));
            DateCount = _multipleDates.Count;
        }

        public bool CanRemoveMultipleDatesExecute(object param)
        {
            return true;
        }
        public void RemoveMultipleDatesExecute(object param)
        {
            DateTime timestamp = (DateTime)param;
            DateWrapper selectedDate = _multipleDates.First(d => d.Timestamp == timestamp);
            MultipleDates.Remove(selectedDate);
            DateCount = MultipleDates.Count;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public void LoadPhotoLinksExecute()
        {
            List<String> selectedPaths = _photoService.SelectPhotoPaths();
            _paths = _photoService.CopyToResourceDirectory(selectedPaths);
            foreach(String path in selectedPaths)
            {
                Photos.Add(new Photo(path, 'T', -1));
            }
            //AddPhotos(selectedPaths);
        }

        public bool CanDeletePhotoExecute(object param)
        {
            return true;
        }

        public void DeletePhotoExecute(object param)
        {
            string link = (string)param;
            Photo photo = Photos.First(p => p.Link == link);
            string path = _paths.First(p => p.Split("\\")[^1] == link.Split("\\")[^1]);
            _paths.Remove(path);
            _newTour.Photos.Remove(photo);
            Photos.Remove(photo);
            //_photoService.Delete(id);
        }
    }
}