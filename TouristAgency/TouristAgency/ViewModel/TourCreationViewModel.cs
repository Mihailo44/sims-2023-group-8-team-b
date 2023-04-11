using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class TourCreationViewModel : ViewModelBase, ICloseable, ICreate
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

        public TourCreationViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;

            NewTour = new Tour();
            NewLocation = new Location();
            _availableCheckpoints = new ObservableCollection<Checkpoint>();
            _selectedCheckpoints = new ObservableCollection<Checkpoint>();
            _multipleDateTimes = new List<DateTime>();
            _datecount = _multipleDateTimes.Count;
            _loggedInGuide = guide;
            _newTour.AssignedGuideID = guide.ID;
            _newTour.AssignedGuide = guide;
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

        public DelegateCommand AddCheckpointCmd { get; }
        public DelegateCommand RemoveCheckpointCmd { get; }
        public DelegateCommand AddMultipleDatesCmd { get; }
        public DelegateCommand RemoveMultipleDatesCmd { get; }
        public DelegateCommand LoadCheckpointsIntoListViewCmd { get; }
        public DelegateCommand CloseCmd { get; }
        public DelegateCommand CreateCmd { get; }

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

        public void LoadCheckpoints()
        {
            AvailableCheckpoints =
                new ObservableCollection<Checkpoint>(_app.CheckpointService.FindSuitableByLocation(NewLocation));
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
                LoadCheckpoints();
            }
        }

        public void PrepareLocation()
        {
            int locationID = _app.LocationService.FindLocationId(NewLocation);
            NewLocation.Id = locationID;

            if (locationID == -1)
            {
                _app.LocationService.Create(NewLocation);
            }

            _newTour.ShortLocation = NewLocation;
            _newTour.ShortLocationID = NewLocation.Id;
        }

        public void AddPhotos()
        {
            int tourID = _app.TourService.GenerateId() - 1;
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'T', tourID);
                    _newTour.Photos.Add(photo);
                    _app.PhotoService.Create(photo);
                }
            }
        }

        public void LoadToursToCheckpoints()
        {
            int tourID = _app.TourService.GenerateId() - 1;
            int i = 0;
            bool firstVisit = true;
            foreach (Checkpoint checkpoint in SelectedCheckpoints)
            {
                if (i != 0)
                {
                    firstVisit = false;
                }

                _newTour.Checkpoints.Add(checkpoint);
                _app.TourCheckpointService.Create(new TourCheckpoint(tourID, checkpoint.ID, firstVisit));
                i++;
            }
        }

        public bool CanCreateTourExecute()
        {
            return true;
        }

        public void CreateTourExecute()
        {
            //TODO Implementirati proveru da li postoji vec slika u PhotoService!
            if (SelectedCheckpoints.Count < 2)
            {
                MessageBox.Show("Must have selected at least 2 checkpoints!");
                return;
            }

            foreach (DateTime dateTime in _multipleDateTimes)
            {
                PrepareLocation();
                _newTour.StartDateTime = dateTime;
                _newTour.RemainingCapacity = _newTour.MaxAttendants;
                _app.TourService.Create(new Tour(_newTour));
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