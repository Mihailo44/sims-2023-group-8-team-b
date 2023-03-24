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
using TouristAgency.ViewModel;
using TouristAgency.Model;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    public partial class TourCreation : Window, INotifyPropertyChanged, IDataErrorInfo
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
        public event PropertyChangedEventHandler PropertyChanged;

        public TourCreation(Guide guide)
        {
            InitializeComponent();
            this.DataContext = this;
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
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "PhotoLinks")
                {
                    if (string.IsNullOrEmpty(PhotoLinks))
                        return "Required field";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "PhotoLinks" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
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

        private void LoadCheckpoints()
        {
            AvailableCheckpoints =
                new ObservableCollection<Checkpoint>(_app.CheckpointController.FindSuitableByLocation(NewLocation));
        }

        private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectedCheckpoints = new ObservableCollection<Checkpoint>();
            if (NewLocation.Country != "" && NewLocation.City != "")
            {
                LoadCheckpoints();
            }
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO Implementirati proveru da li postoji vec slika u PhotoService!
            foreach (DateTime dateTime in _multipleDateTimes)
            {
                PrepareLocation();
                _newTour.StartDateTime = dateTime;
                _newTour.RemainingCapacity = _newTour.MaxAttendants;
                _app.TourController.Create(new Tour(_newTour));
                AddPhotos();
                LoadToursToCheckpoints();
            }
            MessageBox.Show("Successfully created tour!", "Success");
        }


        private void PrepareLocation()
        {
            int locationID = _app.LocationController.FindLocationID(NewLocation);
            NewLocation.Id = locationID;
            if (locationID == -1)
            {
                _app.LocationController.Create(NewLocation);
            }

            _newTour.ShortLocation = NewLocation;
            _newTour.ShortLocationID = NewLocation.Id;
            }

        private void AddPhotos()
        {
            int tourID = _app.TourController.GenerateID() - 1;
            PhotoLinks = PhotoLinks.Replace("\r\n", "|");
            string[] photoLinks = PhotoLinks.Split("|");
            foreach (string photoLink in photoLinks)
            {
                Photo photo = new Photo(photoLink, 'T', tourID);
                _newTour.Photos.Add(photo);
                _app.PhotoViewModel.Create(photo);
            }
        }

        private void LoadToursToCheckpoints()
        {
            int tourID = _app.TourController.GenerateID() - 1;
            int i = 0;
            bool firstVisit = true;
            foreach (Checkpoint checkpoint in SelectedCheckpoints)
            {
                if (i != 0)
                {
                    firstVisit = false;
                }
                _newTour.Checkpoints.Add(checkpoint); //!
                _app.TourCheckpointController.Create(new TourCheckpoint(tourID,checkpoint.ID, firstVisit));
                i++;
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
                if (!SelectedCheckpoints.Contains(checkpoint))
                {
                    SelectedCheckpoints.Add(checkpoint);
                }
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

        private void AddDateButton_OnClick(object sender, RoutedEventArgs e)
        {
            _multipleDateTimes.Add(NewTour.StartDateTime);
            DateCount = _multipleDateTimes.Count;
        }

        private void RemoveDateButton_OnClick(object sender, RoutedEventArgs e)
        {
            _multipleDateTimes.Remove(NewTour.StartDateTime);
            DateCount = _multipleDateTimes.Count;
        }

    }
}