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

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    public partial class TourCreation : Window, INotifyPropertyChanged
    {
        private TourController _tourController;
        private CheckpointController _checkpointController;
        private PhotoController _photoController;
        private TourCheckpointController _tourCheckpointController;
        private LocationController _locationController;
        private ObservableCollection<Checkpoint> _availableCheckpoints;
        private ObservableCollection<Checkpoint> _selectedCheckpoints;
        private Tour _newTour;
        private Location _newLocation;
        private string _photoLinks;
        public event PropertyChangedEventHandler PropertyChanged;

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

        public TourCreation(TourController tourController, CheckpointController checkpointController,
            PhotoController photoController, TourCheckpointController tourCheckpointController, LocationController locationController)
        {
            InitializeComponent();
            NewTour = new Tour();
            NewLocation = new Location();
            _tourController = tourController;
            _checkpointController = checkpointController;
            _photoController = photoController;
            _tourCheckpointController = tourCheckpointController;
            _locationController = locationController;
            _availableCheckpoints = new ObservableCollection<Checkpoint>();
            _selectedCheckpoints = new ObservableCollection<Checkpoint>();
            this.DataContext = this;
        }

        private void LoadCheckpoints()
        {
            AvailableCheckpoints =
                new ObservableCollection<Checkpoint>(_checkpointController.FindSuitableByLocation(NewLocation));
        }

        private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewLocation.Country != "" && NewLocation.City != "")
            {
                LoadCheckpoints();
            }
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO Implementirati proveru da li postoji vec slika u PhotoDAO!
            PrepareLocation();
            _tourController.Create(_newTour);
            AddPhotos();
            BindCheckpointAndTour();
            PrepareLocation();
        }


        private void PrepareLocation()
        {
            int locationID = _locationController.FindLocationID(NewLocation);
            NewLocation.Id = locationID;
            if (locationID == -1)
            {
                _locationController.Create(NewLocation);
            }

            _newTour.ShortLocation = NewLocation;
            _newTour.ShortLocationID = NewLocation.Id;
            }

        private void AddPhotos()
        {
            PhotoLinks = PhotoLinks.Replace("\r\n", "|");
            string[] photoLinks = PhotoLinks.Split("|");
            foreach (string photoLink in photoLinks)
            {
                Photo photo = new Photo(photoLink, 'T', _newTour.ID);
                _newTour.Photos.Add(photo);
                _photoController.Create(photo);
            }
        }

        private void BindCheckpointAndTour()
        {
            foreach (Checkpoint checkpoint in SelectedCheckpoints)
            {
                _tourCheckpointController.Create(new TourCheckpoint(_newTour.ID,checkpoint.ID));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RightButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Checkpoint checkpoint in AvailableListView.SelectedItems)
            {
                SelectedCheckpoints.Add(checkpoint);
            }
        }

        private void LeftButton_OnClick(object sender, RoutedEventArgs e)
        {
            List<Checkpoint> checkpointsToDelete = new List<Checkpoint>();
            foreach (Checkpoint checkpoint in SelectedListView.SelectedItems)
            {
                checkpointsToDelete.Add(checkpoint);
            }

            foreach (Checkpoint checkpoint in checkpointsToDelete)
            {
                SelectedCheckpoints.Remove(checkpoint);
            }
            checkpointsToDelete.Clear();
        }
    }
}