using System;
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

        private TourController _tourController;
        private TourCheckpointController _tourCheckpointController;
        private CheckpointController _checkpointController;
        private TouristController _touristController;
        private TourTouristCheckpointController _tourTouristCheckpointController;
        private ObservableCollection<Tour> _availableTours;
        private ObservableCollection<TourCheckpoint> _availableCheckpoints;
        private ObservableCollection<Tourist> _registeredTourists;
        private ObservableCollection<Tourist> _arrivedTourists;
        public event PropertyChangedEventHandler PropertyChanged;

        public ActiveTourDisplay()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = (App)Application.Current;

            _tourController = app.TourController;
            _tourCheckpointController = app.TourCheckpointController;
            _checkpointController = app.CheckpointController;
            _touristController = app.TouristController;
            _tourTouristCheckpointController = app.TourTouristCheckpointController;
            AvailableTours = _tourController.GetTodayTours();
            _arrivedTourists = new ObservableCollection<Tourist>();
            _registeredTourists = new ObservableCollection<Tourist>();
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
            //TODO
            //Do ovog momenta bi trebalo da su ucitane sve ture, pa logika moze da ide u DAO
            //Pogledaj ostale metode i primeni slican princip
            Tour selectedTour = (Tour)AvailableToursListView.SelectedItem;
            RegisteredTourists = _tourController.GetTouristsFromTour(selectedTour.ID);
            AvailableCheckpoints = new ObservableCollection<TourCheckpoint>(_tourCheckpointController.FindByID(selectedTour.ID));
        }

        private void RightButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Tourist selectedTourist in RegisteredTouristsListView.SelectedItems)
            {
                if (!ArrivedTourists.Contains(selectedTourist))
                {
                    ArrivedTourists.Add(selectedTourist);
                    Tour selectedTour = (Tour)AvailableToursListView.SelectedItem;
                    TourCheckpoint selectedTourCheckpoint = (TourCheckpoint)AvailableCheckpointsListView.SelectedItem;
                    _tourTouristCheckpointController.Create(new TourTouristCheckpoint(selectedTour.ID,
                        selectedTourist.ID, selectedTourCheckpoint.CheckpointID));
                }
            }
        }

        private void AvailableCheckpointsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArrivedTourists.Clear();
            Tour selectedTour = (Tour)AvailableToursListView.SelectedItem;
            TourCheckpoint selectedTourCheckpoint = (TourCheckpoint)AvailableCheckpointsListView.SelectedItem;
            foreach (TourTouristCheckpoint tourTouristCheckpoint in _tourTouristCheckpointController.GetAll())
            {
                bool isSameTour = selectedTour.ID == tourTouristCheckpoint.TourCheckpoint.TourID;
                bool isSameCheckpoint = selectedTourCheckpoint.CheckpointID == tourTouristCheckpoint.TourCheckpoint.CheckpointID;
                if (isSameTour && isSameCheckpoint)
                {
                    ArrivedTourists.Add(_touristController.FindById(tourTouristCheckpoint.TouristID));
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
                _tourTouristCheckpointController.Delete(tourist.ID);
            }
            touristsToDelete.Clear();
        }

        private void FinishButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to finish the tour?","Question",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                foreach (TourCheckpoint tourCheckpoint in AvailableCheckpoints)
                {
                    _tourCheckpointController.Update(tourCheckpoint);
                }
                //TODO Tour isFinished property
                MessageBox.Show("Tour ended!");
            }
        }
    }
}
