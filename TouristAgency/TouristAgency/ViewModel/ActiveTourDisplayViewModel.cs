using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.Enums;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class ActiveTourDisplayViewModel : ViewModelBase, ICreate, IObserver
    {
        private App _app;
        private Window _window;
        private Guide _loggedInGuide;

        private TourService _tourService;
        private TourCheckpointService _tourCheckpointService;
        private TourTouristService _tourTouristService;
        private TourTouristCheckpointService _tourTouristCheckpointService;

        private ObservableCollection<Tour> _availableTours;
        private Tour _selectedTour;
        private ObservableCollection<TourCheckpoint> _availableCheckpoints;
        private ObservableCollection<Tourist> _registeredTourists;
        private ObservableCollection<Tourist> _arrivedTourists;
        private bool _listViewEnabled;

        public DelegateCommand CreateCmd { get; }
        public DelegateCommand AddTouristToCheckpointCmd { get; }
        public DelegateCommand RemoveTouristFromCheckpointCmd { get; }
        public DelegateCommand LoadTouristsToCheckpointCmd { get; }
        public DelegateCommand BeginTourCmd { get; }

        public ActiveTourDisplayViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;
            _window = window;
            _loggedInGuide = guide;

            _tourService = new TourService();
            _tourCheckpointService = new TourCheckpointService();
            _tourTouristService = new TourTouristService();

            AvailableTours = new ObservableCollection<Tour>(_tourService.GetTodayTours(_loggedInGuide.ID));
            _arrivedTourists = new ObservableCollection<Tourist>();
            _registeredTourists = new ObservableCollection<Tourist>();
            _app.TourTouristRepository.Subscribe(this);

            if(CheckAndSelectStartedTour())
            {
                ListViewEnabled = false;
            }
            else
            {
                ListViewEnabled = true;
            }

            AddTouristToCheckpointCmd =
                new DelegateCommand(param => AddTouristToCheckpoint(), param => CanAddTouristToCheckpoint());
            RemoveTouristFromCheckpointCmd = new DelegateCommand(param => RemoveTouristFromCheckpoint(),
                param => CanRemoveTouristFromCheckpoint());
            LoadTouristsToCheckpointCmd = new DelegateCommand(param => LoadTouristsToCheckpoint(),
                param => CanLoadTouristsToCheckpoint());
            BeginTourCmd = new DelegateCommand(param => BeginTourCmdExecute(), param => CanBeginTourCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());

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
            get;
            set;
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
                if(value != _listViewEnabled)
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
                    _app.TourTouristCheckpointRepository.Create(new TourTouristCheckpoint(_selectedTour.ID,
                        selectedTourist.ID, selectedTourCheckpoint.CheckpointID));
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
                _app.TourTouristCheckpointRepository.Delete(tourist.ID);
            }
            touristsToDelete.Clear();
        }

        public bool CanLoadTouristsToCheckpoint()
        {
            return true;
        }

        public void LoadTouristsToCheckpoint()
        {
            //TODO REPOSITORY
            /*if (SelectedTour != null && AvailableCheckpoints != null)
            {
                ArrivedTourists.Clear();
                TourCheckpoint selectedTourCheckpoint = SelectedTourCheckpoint;
                ObservableCollection<Tourist> allTourists = new ObservableCollection<Tourist>(_app.TouristService.GetAll());
                if (selectedTourCheckpoint != null && _selectedTour != null)
                {
                    ArrivedTourists = _tourTouristCheckpointService.FilterTouristsOnCheckpoint(_selectedTour.ID,
                        selectedTourCheckpoint.CheckpointID, allTourists);
                }
                foreach (TourCheckpoint tc in AvailableCheckpoints)
                {
                    _app.TourCheckpointRepository.Update(tc);
                }
            }*/
        }

        public bool CanBeginTourCmdExecute()
        {
            return true;
        }

        public void BeginTourCmdExecute()
        {
            _selectedTour = SelectedTour;
            //TODO REPOSITORY
            //RegisteredTourists = new ObservableCollection<Tourist>(_tourTouristService.GetArrivedTourist(_selectedTour.ID, _app.TouristService.GetAll()));
            _tourService.ChangeTourStatus(_selectedTour.ID, STATUS.IN_PROGRESS);
            AvailableCheckpoints = _tourCheckpointService.GetTourCheckpointsByTourID(_selectedTour.ID, _app.CheckpointService.GetAll());
            ListViewEnabled = false;
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
                    _app.TourCheckpointRepository.Update(tourCheckpoint);
                }
                _tourService.ChangeTourStatus(_selectedTour.ID, STATUS.ENDED);

                AvailableTours.Remove(_selectedTour);
                AvailableCheckpoints.Clear();
                ArrivedTourists.Clear();
                RegisteredTourists.Clear();
                ListViewEnabled = true;
                MessageBox.Show("Tour ended!", "Notification");
            }
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

        private bool CheckAndSelectStartedTour()
        {
            foreach (Tour tour in AvailableTours)
            {
                if (tour.Status == STATUS.IN_PROGRESS)
                {
                    _selectedTour = tour;
                    SelectedTour = tour;
                    //TODO REPOSITORY
                    //RegisteredTourists = new ObservableCollection<Tourist>(_tourTouristService.GetArrivedTourist(_selectedTour.ID, _app.TouristService.GetAll()));
                    AvailableCheckpoints = new ObservableCollection<TourCheckpoint>(_app.TourCheckpointRepository.GetByID(_selectedTour.ID));
                    ListViewEnabled = false;
                    return true;
                }
            }
            return false;
        }

        public void Update()
        {
            CheckAndSelectStartedTour();
        }
    }
}
