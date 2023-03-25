using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.ViewModel;
using TouristAgency.Model;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ReservationViewModel ReservationViewModel { get; set; } = new ReservationViewModel();
        public AccommodationCreationViewModel AccommodationCreationViewModel { get; set; } = new AccommodationCreationViewModel();
        public CheckpointController CheckpointController { get; set; } = new CheckpointController();    
        public GuestController GuestController { get; set; } = new GuestController();
        public GuestReviewViewModel GuestReviewViewModel { get; set; } = new GuestReviewViewModel();
        public LocationController LocationController { get; set; } = new LocationController();
        public OwnerViewModel OwnerViewModel { get; set; } = new OwnerViewModel();
        public GuideController GuideController { get; set; } = new GuideController();
        public PhotoViewModel PhotoViewModel { get; set; } = new PhotoViewModel();
        public TourCheckpointController TourCheckpointController { get; set; } = new TourCheckpointController();
        public TourController TourController { get; set; } = new TourController();
        public TouristController TouristController { get; set; } = new TouristController();
        public TourTouristCheckpointController TourTouristCheckpointController { get; set; } = new TourTouristCheckpointController();
        public TourTouristController TourTouristController { get; set; } = new TourTouristController();

        public App()
        {
            AccommodationCreationViewModel.LoadLocationsToAccommodations(LocationController.GetAll());
            AccommodationCreationViewModel.LoadPhotosToAccommodations(PhotoViewModel.GetAll());
            CheckpointController.LoadLocationsToCheckpoints(LocationController.GetAll());
            TourController.LoadLocationsToTours(LocationController.GetAll());
            TourController.LoadPhotosToTours(PhotoViewModel.GetAll());
            ReservationViewModel.LoadAccommodationsToReservations(AccommodationCreationViewModel.GetAll());
            ReservationViewModel.LoadGuestsToReservations(GuestController.GetAll());
            TourCheckpointController.LoadCheckpoints(CheckpointController.GetAll());
            OwnerViewModel.LoadAccommodationsToOwners(AccommodationCreationViewModel.GetAll());
            GuideController.LoadToursToGuide(TourController.GetAll());
            TourController.LoadTouristsToTours(TourTouristController.GetAll(),TouristController.GetAll());
            TouristController.LoadToursToTourist(TourTouristController.GetAll(), TourController.GetAll());
            TourController.LoadCheckpointsToTours(TourCheckpointController.GetAll(), CheckpointController.GetAll());
            OwnerViewModel.LoadLocationsToOwners(LocationController.GetAll());
        }
    }
}
