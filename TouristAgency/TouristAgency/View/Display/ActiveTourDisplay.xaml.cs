﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TouristAgency.Controller;
using TouristAgency.Model;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for ActiveTourDisplay.xaml
    /// </summary>
    public partial class ActiveTourDisplay : Window, INotifyPropertyChanged
    {

        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<Tour> _availableTours;
        private Tour _selectedTour;
        private ObservableCollection<TourCheckpoint> _availableCheckpoints;
        private ObservableCollection<Tourist> _registeredTourists;
        private ObservableCollection<Tourist> _arrivedTourists;
        public event PropertyChangedEventHandler PropertyChanged;

        public ActiveTourDisplay(Guide guide)
        {
            InitializeComponent();
            this.DataContext = this;
            _app = (App)Application.Current;

            _loggedInGuide = guide;
            AvailableTours = _app.TourController.GetTodayTours(_loggedInGuide.ID);
            _arrivedTourists = new ObservableCollection<Tourist>();
            _registeredTourists = new ObservableCollection<Tourist>();
            CheckAndSelectStartedTour();

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

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BeginTourButton_OnClick(object sender, RoutedEventArgs e)
        {
            BeginTourButton.IsEnabled = false;
            AvailableToursListView.IsEnabled = false;
            FinishButton.IsEnabled = true;
            _selectedTour = (Tour)AvailableToursListView.SelectedItem;
            _selectedTour.Status = STATUS.IN_PROGRESS;
            _app.TourController.Update(_selectedTour,_selectedTour.ID);
            RegisteredTourists = _app.TourController.GetTouristsFromTour(_selectedTour.ID);
            //TODO REFAKTORISATI KASNIJE
            ObservableCollection<TourCheckpoint> checkpointsTemp = new ObservableCollection<TourCheckpoint>(_app.TourCheckpointController.FindByID(_selectedTour.ID));
            foreach(TourCheckpoint checkpoint in checkpointsTemp)
            {
                checkpoint.Checkpoint = _app.CheckpointController.FindByID(checkpoint.CheckpointID);
            }
            AvailableCheckpoints = checkpointsTemp;
        }

        private void RightButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Tourist selectedTourist in RegisteredTouristsListView.SelectedItems)
            {
                if (!ArrivedTourists.Contains(selectedTourist))
                {
                    ArrivedTourists.Add(selectedTourist);
                    TourCheckpoint selectedTourCheckpoint = (TourCheckpoint)AvailableCheckpointsListView.SelectedItem;
                    _app.TourTouristCheckpointController.Create(new TourTouristCheckpoint(_selectedTour.ID,
                        selectedTourist.ID, selectedTourCheckpoint.CheckpointID));
                }
            }
        }

        private void AvailableCheckpointsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArrivedTourists.Clear();
            TourCheckpoint selectedTourCheckpoint = (TourCheckpoint)AvailableCheckpointsListView.SelectedItem;
            foreach (TourTouristCheckpoint tourTouristCheckpoint in _app.TourTouristCheckpointController.GetAll())
            {
                if (_selectedTour != null)
                {
                    bool isSameTour = _selectedTour.ID == tourTouristCheckpoint.TourCheckpoint.TourID;
                    bool isSameCheckpoint = selectedTourCheckpoint.CheckpointID == tourTouristCheckpoint.TourCheckpoint.CheckpointID;

                    if (isSameTour && isSameCheckpoint)
                    {
                        ArrivedTourists.Add(_app.TouristController.FindById(tourTouristCheckpoint.TouristID));
                    }
                }
            }
        }

        private void LeftButton_OnClick(object sender, RoutedEventArgs e)
        {
            List<Tourist> touristsToDelete = new List<Tourist>();
            foreach (Tourist tourist in ArrivedTouristListView.SelectedItems)
            {
                touristsToDelete.Add(tourist);
            }

            foreach (Tourist tourist in touristsToDelete)
            {
                ArrivedTourists.Remove(tourist);
                _app.TourTouristCheckpointController.Delete(tourist.ID);
            }
            touristsToDelete.Clear();
        }

        private void FinishButton_OnClick(object sender, RoutedEventArgs e)
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
                AvailableToursListView.IsEnabled = true;
                BeginTourButton.IsEnabled = true;
                FinishButton.IsEnabled = false;
                foreach (TourCheckpoint tourCheckpoint in AvailableCheckpoints)
                {
                    _app.TourCheckpointController.Update(tourCheckpoint);
                }

                _selectedTour.Status = STATUS.ENDED;
                _app.TourController.Update(_selectedTour,_selectedTour.ID);
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

        private void AvailableToursListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTour = (Tour)AvailableToursListView.SelectedItem;
        }

        private void CheckAndSelectStartedTour()
        {
            foreach (Tour tour in AvailableTours)
            {
                if (tour.Status == STATUS.IN_PROGRESS)
                {
                    _selectedTour = tour;
                    AvailableToursListView.IsEnabled = false;
                    BeginTourButton.IsEnabled = false;
                    FinishButton.IsEnabled = true;
                    RegisteredTourists = _app.TourController.GetTouristsFromTour(_selectedTour.ID);
                    AvailableCheckpoints = new ObservableCollection<TourCheckpoint>(_app.TourCheckpointController.FindByID(_selectedTour.ID));
                }
            }
        }
    }
}
