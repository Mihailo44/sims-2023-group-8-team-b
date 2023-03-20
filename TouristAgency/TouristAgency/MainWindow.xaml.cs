using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TouristAgency.Controller;
using TouristAgency.Model;
using TouristAgency.Test;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;
using TouristAgency.View.Home;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged,IDataErrorInfo
    {
        private CheckpointController _checkpointController;
        private TourController _tourController;
        private AccommodationController _accommodationController;
        private LocationController _locationController;
        private PhotoController _photoController;
        private TourCheckpointController _tourCheckpointController;
        private OwnerController _ownerController;
        private ReservationController _reservationController;
        private TouristController _touristController;
        private TourTouristController _tourTouristController;
        private TourTouristCheckpointController _tourTouristCheckpointController;
        private GuestController _guestController;

        public object User { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if(_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if(_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if(columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Required field";
                }
                else if(columnName == "Password")
                {
                    if (string.IsNullOrEmpty(Password))
                        return "Required field";
                }

                return null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _checkpointController = new CheckpointController();
            _tourController = new TourController();
            _locationController = new LocationController();
            _accommodationController = new AccommodationController();
            _photoController = new PhotoController();
            _tourCheckpointController = new TourCheckpointController();
            _ownerController = new OwnerController();
            _reservationController = new ReservationController();
            _touristController = new TouristController();
            _tourTouristController = new TourTouristController();
            _tourTouristCheckpointController = new TourTouristCheckpointController();
            _guestController = new GuestController();

            _accommodationController.LoadLocationsToAccommodations(_locationController.GetAll());
            _accommodationController.LoadPhotosToAccommodations(_photoController.GetAll());
            _checkpointController.LoadLocationsToCheckpoints(_locationController.GetAll());
            _tourController.LoadLocationsToTours(_locationController.GetAll());
            _photoController.LoadToursToPhotos(_tourController.GetAll());
            _reservationController.LoadAccommodationsToReservations(_accommodationController.GetAll());
            _reservationController.LoadGuestsToReservations(_guestController.GetAll());
            _accommodationController.LoadLocationsToAccommodations(_locationController.GetAll());
            _tourCheckpointController.LoadCheckpoints(_checkpointController.GetAll());
            _ownerController.LoadAccommodationsToOwners(_accommodationController.GetAll());

            LoadTourToTourist(_tourTouristController.GetAll());
            LoadCheckpointToTourist(_tourCheckpointController.GetAll());
        }

        public void LoadCheckpointToTourist(List<TourCheckpoint> tourCheckpoints)
        {
            foreach (TourCheckpoint tourCheckpoint in tourCheckpoints)
            {
                Tour tour = _tourController.FindById(tourCheckpoint.TourID);
                Checkpoint checkpoint = _checkpointController.FindByID(tourCheckpoint.CheckpointID);
                tour.Checkpoints.Add(checkpoint);
                _tourController.Update(tour, tour.ID);
            }
        }

        public void LoadTourToTourist(List<TourTourist> tourTourists)
        {
            foreach (TourTourist tourTourist in tourTourists)
            {
                Tour tour = _tourController.FindById(tourTourist.TourID);
                Tourist tourist = _touristController.FindById(tourTourist.TouristID);
                if (tourist != null)
                {
                    tour.RegisteredTourists.Add(tourist);
                    tourist.AppliedTours.Add(tour);
                }
               
            }
        }

        private void TourButton_Click(object sender, RoutedEventArgs e)
        {
            TourCreation creation = new TourCreation(_tourController, _checkpointController, _photoController, _tourCheckpointController, _locationController);
            creation.Show();
        }

        private void TourDisplay_Click(object sender, RoutedEventArgs e)
        {
            TourDisplay display = new TourDisplay(_tourController, _tourTouristController, _touristController);
            display.Show();
        }

        private void AccommodationDisplay_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDisplay display = new AccommodationDisplay(_accommodationController, _reservationController);
            display.Show();
        }

        private void ActiveTourDisplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            ActiveTourDisplay y = new ActiveTourDisplay(_tourController,_tourCheckpointController,_checkpointController, _touristController, _tourTouristCheckpointController);
            y.Show();
        }

        private string GetUserType()
        {
            User = _ownerController.GetAll().Find(o => o.Username == Username && o.Password == Password);
            if (User != null)
            {
                return User.GetType().ToString();
            }
            /*User = GuestController.GetAll().Find(g => g.Username == Username && g.Password == Password);
            if (User != null)
            {
                return User.GetType().ToString();
            }*/

            return null;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string type = GetUserType();

            if (type != null)
            {
                switch (type)
                {
                    case "TouristAgency.Model.Owner":
                        {
                            OwnerHome x = new OwnerHome(_reservationController, _accommodationController,_ownerController, _locationController, _photoController, (Owner)User);
                            x.Show();
                        }
                        break;
                    case "TouristAgency.Model.Guest":
                        {
                            //TourDisplay x = new TourDisplay((Guest)User);
                        }
                        break;
                    default: MessageBox.Show("Failure"); break;
                }
            }
            else
            {
                MessageBox.Show("User is not registered");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
