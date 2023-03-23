using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.ViewModel;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ReservationViewModel ReservationViewModel { get; set; } = new ReservationViewModel();
        public AccommodationViewModel AccommodationViewModel { get; set; } = new AccommodationViewModel();
        public CheckpointController CheckpointController { get; set; } = new CheckpointController();    
        public GuestController GuestController { get; set; } = new GuestController();
        public GuestReviewViewModel GuestReviewViewModel { get; set; } = new GuestReviewViewModel();
        public LocationController LocationController { get; set; } = new LocationController();
        public OwnerViewModel OwnerViewModel { get; set; } = new OwnerViewModel();
        public GuideController GuideController { get; set; } = new GuideController();
        public PhotoController PhotoController { get; set; } = new PhotoController();
        public TourCheckpointController TourCheckpointController { get; set; } = new TourCheckpointController();
        public TourController TourController { get; set; } = new TourController();
        public TouristController TouristController { get; set; } = new TouristController();
        public TourTouristCheckpointController TourTouristCheckpointController { get; set; } = new TourTouristCheckpointController();
        public TourTouristController TourTouristController { get; set; } = new TourTouristController();

        public App()
        {
            AccommodationViewModel.LoadLocationsToAccommodations(LocationController.GetAll());
            AccommodationViewModel.LoadPhotosToAccommodations(PhotoController.GetAll());
            CheckpointController.LoadLocationsToCheckpoints(LocationController.GetAll());
            TourController.LoadLocationsToTours(LocationController.GetAll());
            TourController.LoadPhotosToTours(PhotoController.GetAll());
            ReservationViewModel.LoadAccommodationsToReservations(AccommodationViewModel.GetAll());
            ReservationViewModel.LoadGuestsToReservations(GuestController.GetAll());
            TourCheckpointController.LoadCheckpoints(CheckpointController.GetAll());
            OwnerViewModel.LoadAccommodationsToOwners(AccommodationViewModel.GetAll());
            GuideController.LoadToursToGuide(TourController.GetAll());
            TourController.LoadTouristsToTours(TourTouristController.GetAll(),TouristController.GetAll());
            TouristController.LoadToursToTourist(TourTouristController.GetAll(), TourController.GetAll());
            TourController.LoadCheckpointsToTours(TourCheckpointController.GetAll(), CheckpointController.GetAll());
            OwnerViewModel.LoadLocationsToOwners(LocationController.GetAll());
        }
    }
}
