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
    public partial class MainWindow : Window
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

        public MainWindow()
        {
            InitializeComponent();
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

            _accommodationController.LoadLocationsToAccommodations(_locationController.GetAll());
            _accommodationController.LoadPhotosToAccommodations(_photoController.GetAll());
            _checkpointController.LoadLocationsToCheckpoints(_locationController.GetAll());
            _tourController.LoadLocationsToTours(_locationController.GetAll());
            _photoController.LoadToursToPhotos(_tourController.GetAll());
            _reservationController.LoadAccommodationsToReservations(_accommodationController.GetAll());
           // _reservationController.LoadGuestsToReservations(_guestController.GetAll());
            _accommodationController.LoadLocationsToAccommodations(_locationController.GetAll());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerHome x = new OwnerHome(_reservationController,_accommodationController,_ownerController,_locationController,_photoController);
            x.Show();
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
            AccommodationDisplay display = new AccommodationDisplay(_accommodationController);
            display.Show();
        }

        private void ActiveTourDisplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            ActiveTourDisplay y = new ActiveTourDisplay(_tourController,_tourCheckpointController,_checkpointController, _touristController);
            y.Show();
        }
    }
}
