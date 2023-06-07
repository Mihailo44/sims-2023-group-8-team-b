using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Notifications.Domain;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Util;

namespace TouristAgency.Tours.BeginTourFeature
{
    public class ActiveTourDisplayViewModel : BurgerMenuViewModelBase, ICreate, IObserver
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<Tour> _availableTours;
        private ObservableCollection<TourCheckpoint> _availableCheckpoints;
        private ObservableCollection<Tourist> _registeredTourists;
        private ObservableCollection<Tourist> _arrivedTourists;
        private Tour _selectedTour;
        private bool _listViewEnabled;

        private TourService _tourService;
        private CheckpointService _checkpointService;
        private TourCheckpointService _tourCheckpointService;
        private TourTouristService _tourTouristService;
        private TourTouristCheckpointService _tourTouristCheckpointService;
        private TouristService _touristService;
        private TouristNotificationService _touristNotificationService;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand AddTouristToCheckpointCmd { get; set; }
        public DelegateCommand RemoveTouristFromCheckpointCmd { get; set; }
        public DelegateCommand LoadTouristsToCheckpointCmd { get; set; }
        public DelegateCommand SaveCmd { get; set; }

        public ActiveTourDisplayViewModel(Guide guide, Tour tour)
        {
            _app = (App)Application.Current;
            _loggedInGuide = guide;
            MenuVisibility = "Hidden";
            SelectedTour = tour;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
            StartTour(tour);
            _app.TourTouristRepository.Subscribe(this);
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _checkpointService = new CheckpointService();
            _tourCheckpointService = new TourCheckpointService();
            _tourTouristService = new TourTouristService();
            _tourTouristCheckpointService = new TourTouristCheckpointService();
            _touristService = new TouristService();
            _touristNotificationService = new TouristNotificationService();
        }

        private void InstantiateCollections()
        {
            ArrivedTourists = new ObservableCollection<Tourist>();
            RegisteredTourists = new ObservableCollection<Tourist>();
        }

        private void InstantiateCommands()
        {
            AddTouristToCheckpointCmd =
                new DelegateCommand(param => AddTouristToCheckpoint(), param => CanAddTouristToCheckpoint());
            RemoveTouristFromCheckpointCmd = new DelegateCommand(param => RemoveTouristFromCheckpoint(),
                param => CanRemoveTouristFromCheckpoint());
            LoadTouristsToCheckpointCmd = new DelegateCommand(param => LoadTouristsToCheckpoint(),
                param => CanLoadTouristsToCheckpoint());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
            SaveCmd = new DelegateCommand(param => SaveExecute(), param => CanSaveExecute());
        }

        public void StartTour(Tour tour)
        {
            _selectedTour = tour;
            SelectedTour = tour;
            RegisteredTourists = new ObservableCollection<Tourist>(_tourTouristService.GetArrivedTourist(tour.ID, _touristService.TouristRepository.GetAll()));
            _tourService.ChangeTourStatus(tour.ID, TourStatus.IN_PROGRESS);
            AvailableCheckpoints = _tourCheckpointService.GetTourCheckpointsByTourID(tour.ID, _checkpointService.CheckpointRepository.GetAll());
        }

        public ObservableCollection<Tour> AvailableTours
        {
            get => _availableTours;
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("ActiveTours");
                }
            }
        }

        public ObservableCollection<TourCheckpoint> AvailableCheckpoints
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

        public ObservableCollection<Tourist> RegisteredTourists
        {
            get => _registeredTourists;
            set
            {
                if (value != _registeredTourists)
                {
                    _registeredTourists = value;
                    OnPropertyChanged("RegisteredTourists");
                }
            }
        }

        public ObservableCollection<Tourist> ArrivedTourists
        {
            get => _arrivedTourists;
            set
            {
                if (value != _arrivedTourists)
                {
                    _arrivedTourists = value;
                    OnPropertyChanged("ArrivedTourists");
                }
            }
        }
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged("SelectedTour");
                }
            }
        }

        public TourCheckpoint SelectedTourCheckpoint
        {
            get;
            set;
        }

        public bool ListViewEnabled
        {
            get => _listViewEnabled;
            set
            {
                if (value != _listViewEnabled)
                {
                    _listViewEnabled = value;
                    OnPropertyChanged("ListViewEnabled");
                }
            }
        }

        public bool CanAddTouristToCheckpoint()
        {
            return true;
        }

        public void AddTouristToCheckpoint()
        {
            foreach (Tourist selectedTourist in RegisteredTourists)
            {
                if (!ArrivedTourists.Contains(selectedTourist) && selectedTourist.IsSelected)
                {
                    ArrivedTourists.Add(selectedTourist);
                    TourCheckpoint selectedTourCheckpoint = SelectedTourCheckpoint;
                    _tourTouristCheckpointService.TourTouristCheckpointRepository.Create(new TourTouristCheckpoint(_selectedTour.ID,
                        selectedTourist.ID, selectedTourCheckpoint.CheckpointID));
                    _touristNotificationService.NotifyAboutAttendance(selectedTourist.ID, SelectedTourCheckpoint.Checkpoint, SelectedTourCheckpoint.CheckpointID);
                }
            }
        }

        public bool CanRemoveTouristFromCheckpoint()
        {
            return true;
        }

        public void RemoveTouristFromCheckpoint()
        {
            List<Tourist> touristsToDelete = new List<Tourist>();
            foreach (Tourist tourist in ArrivedTourists)
            {
                if (tourist.IsSelected)
                    touristsToDelete.Add(tourist);
            }

            foreach (Tourist tourist in touristsToDelete)
            {
                ArrivedTourists.Remove(tourist);
                _tourTouristCheckpointService.TourTouristCheckpointRepository.Delete(tourist.ID);
            }
            touristsToDelete.Clear();
        }

        public bool CanLoadTouristsToCheckpoint()
        {
            return true;
        }

        public void LoadTouristsToCheckpoint()
        {
            if (SelectedTour != null && AvailableCheckpoints != null)
            {
                ArrivedTourists.Clear();
                TourCheckpoint selectedTourCheckpoint = SelectedTourCheckpoint;
                ObservableCollection<Tourist> allTourists = new ObservableCollection<Tourist>(_touristService.TouristRepository.GetAll());
                if (selectedTourCheckpoint != null && _selectedTour != null)
                {
                    ArrivedTourists = _tourTouristCheckpointService.FilterTouristsOnCheckpoint(_selectedTour.ID,
                        selectedTourCheckpoint.CheckpointID, allTourists);
                }
                foreach (TourCheckpoint tc in AvailableCheckpoints)
                {
                    _tourCheckpointService.TourCheckpointRepository.Update(tc);
                }
            }
        }

        public bool CanCreateCmdExecute()
        {
            return true;
        }

        public void CreateCmdExecute()
        {
            MessageBoxResult result;
            if (AllCheckpointsVisited())
            {
                result = MessageBox.Show("Do you want to finish the tour?", "Question", MessageBoxButton.YesNo);
            }

            else
            {
                result = MessageBox.Show("Not all checkpoints were visited, would you like to end the tour?",
                    "Question", MessageBoxButton.YesNo);
            }

            if (result == MessageBoxResult.Yes)
            {

                foreach (TourCheckpoint tourCheckpoint in AvailableCheckpoints)
                {
                    _tourCheckpointService.TourCheckpointRepository.Update(tourCheckpoint);
                }
                _tourService.ChangeTourStatus(_selectedTour.ID, TourStatus.ENDED);

                AvailableCheckpoints.Clear();
                ArrivedTourists.Clear();
                RegisteredTourists.Clear();
                MessageBox.Show("Tour ended!", "Notification");
                _app.CurrentVM = new GuideHomeViewModel();
            }
        }

        public bool CanSaveExecute()
        {
            return true;
        }

        public void SaveExecute()
        {
            MessageBox.Show("Succesfully saved started tour data!");
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public bool AllCheckpointsVisited()
        {
            foreach (TourCheckpoint tourCheckpoint in AvailableCheckpoints)
            {
                if (tourCheckpoint.IsVisited == false)
                {
                    return false;
                }
            }

            return true;
        }

        public void Update()
        {
            //CheckAndSelectStartedTour();
        }
    }
}
