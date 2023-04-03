﻿using System;
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
    public class ActiveTourDisplayViewModel : ViewModelBase, ICloseable, ICreate
    {
        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<Tour> _availableTours;
        private Tour _selectedTour;
        private ObservableCollection<TourCheckpoint> _availableCheckpoints;
        private ObservableCollection<Tourist> _registeredTourists;
        private ObservableCollection<Tourist> _arrivedTourists;

        public DelegateCommand CloseCmd { get; }
        public DelegateCommand CreateCmd { get; }

        public DelegateCommand AddTouristToCheckpointCmd { get; }
        public DelegateCommand RemoveTouristFromCheckpointCmd { get; }
        public DelegateCommand LoadTouristsToCheckpointCmd { get; }
        public DelegateCommand BeginTourCmd { get; }

        public ActiveTourDisplayViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;

            _loggedInGuide = guide;
            AvailableTours = _app.TourViewModel.GetTodayTours(_loggedInGuide.ID);
            _arrivedTourists = new ObservableCollection<Tourist>();
            _registeredTourists = new ObservableCollection<Tourist>();
            CheckAndSelectStartedTour();
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
        //TODO U WPF POVEZI!
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
                    _app.TourTouristCheckpointViewModel.Create(new TourTouristCheckpoint(_selectedTour.ID,
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
                if(tourist.IsSelected)
                    touristsToDelete.Add(tourist);
            }

            foreach (Tourist tourist in touristsToDelete)
            {
                ArrivedTourists.Remove(tourist);
                _app.TourTouristCheckpointViewModel.Delete(tourist.ID);
            }
            touristsToDelete.Clear();
        }

        public bool CanLoadTouristsToCheckpoint()
        {
            return true;
        }

        public void LoadTouristsToCheckpoint()
        {
            ArrivedTourists.Clear();
            TourCheckpoint selectedTourCheckpoint = SelectedTourCheckpoint;
            ObservableCollection<Tourist> allTourists = new ObservableCollection<Tourist>(_app.TouristViewModel.GetAll());
            if (selectedTourCheckpoint != null && _selectedTour != null)
            {
                ArrivedTourists = _app.TourTouristCheckpointViewModel.FilterTouristsOnCheckpoint(_selectedTour.ID,
                    selectedTourCheckpoint.CheckpointID, allTourists);
            }
        }

        public bool CanBeginTourCmdExecute()
        {
            return true;
        }

        public void BeginTourCmdExecute()
        {
            //BeginTourButton.IsEnabled = false;
            //AvailableToursListView.IsEnabled = false;
            //FinishButton.IsEnabled = true;
            _selectedTour = SelectedTour;
            RegisteredTourists = _app.TourViewModel.GetTouristsFromTour(_selectedTour.ID);
            _app.TourViewModel.ChangeTourStatus(_selectedTour.ID, STATUS.IN_PROGRESS);
            AvailableCheckpoints = _app.TourCheckpointViewModel.GetTourCheckpointsByTourID(_selectedTour.ID, _app.CheckpointViewModel.GetAll());

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
                //AvailableToursListView.IsEnabled = true;
                //BeginTourButton.IsEnabled = true;
                //FinishButton.IsEnabled = false;

                foreach (TourCheckpoint tourCheckpoint in AvailableCheckpoints)
                {
                    _app.TourCheckpointViewModel.Update(tourCheckpoint);
                }
                _app.TourViewModel.ChangeTourStatus(_selectedTour.ID, STATUS.ENDED);

                AvailableTours.Remove(_selectedTour);
                AvailableCheckpoints.Clear();
                ArrivedTourists.Clear();
                RegisteredTourists.Clear();
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

        private void CheckAndSelectStartedTour()
        {
            foreach (Tour tour in AvailableTours)
            {
                if (tour.Status == STATUS.IN_PROGRESS)
                {
                    _selectedTour = tour;
                    //AvailableToursListView.IsEnabled = false;
                    //BeginTourButton.IsEnabled = false;
                    //FinishButton.IsEnabled = true;
                    RegisteredTourists = _app.TourViewModel.GetTouristsFromTour(_selectedTour.ID);
                    AvailableCheckpoints = new ObservableCollection<TourCheckpoint>(_app.TourCheckpointViewModel.FindByID(_selectedTour.ID));
                }
            }
        }
    }
}