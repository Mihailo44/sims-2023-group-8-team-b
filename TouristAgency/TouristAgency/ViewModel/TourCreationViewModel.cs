using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class TourCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<Checkpoint> _availableCheckpoints;
        private ObservableCollection<Checkpoint> _selectedCheckpoints;
        private List<DateTime> _multipleDateTimes;

        private int _datecount;
        private Tour _newTour;
        private Location _newLocation;
        private string _photoLinks;

        private TourService _tourService;
        private LocationService _locationService;
        private CheckpointService _checkpointService;
        private TourCheckpointService _tourCheckpointService;
        public DelegateCommand AddCheckpointCmd { get; set; }
        public DelegateCommand RemoveCheckpointCmd { get; set; }
        public DelegateCommand AddMultipleDatesCmd { get; set; }
        public DelegateCommand RemoveMultipleDatesCmd { get; set; }
        public DelegateCommand LoadCheckpointsIntoListViewCmd { get; set; }
        public DelegateCommand CreateCmd { get; set; }

        public TourCreationViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuide = guide;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }
        private void InstantiateServices()
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            _checkpointService = new CheckpointService();
            _tourCheckpointService = new TourCheckpointService();
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
                _locationService.LocationRepository.Create(NewLocation);
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

        public void LoadToursToCheckpoints()
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
                _tourCheckpointService.TourCheckpointRepository.Create(new TourCheckpoint(tourID, checkpoint.ID, firstVisit));
                i++;
            }
        }

        public bool CanCreateTourExecute()
        {
            return true;
        }

        public void CreateTourExecute()
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
                _tourService.TourRepository.Create(new Tour(_newTour));
                AddPhotos();
                LoadToursToCheckpoints();
            }

            MessageBox.Show("Successfully created tour!", "Success");
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
    }
}