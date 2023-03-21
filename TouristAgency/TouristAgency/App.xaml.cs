using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Controller;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ReservationController ReservationController { get; set; } = new ReservationController();
        public AccommodationController AccommodationController { get; set; } = new AccommodationController();
        public CheckpointController CheckpointController { get; set; } = new CheckpointController();    
        public GuestController GuestController { get; set; } = new GuestController();
        public GuestReviewController GuestReviewController { get; set; } = new GuestReviewController();
        public LocationController LocationController { get; set; } = new LocationController();
        public OwnerController OwnerController { get; set; } = new OwnerController();
        public GuideController GuideController { get; set; } = new GuideController();
        public PhotoController PhotoController { get; set; } = new PhotoController();
        public TourCheckpointController TourCheckpointController { get; set; } = new TourCheckpointController();
        public TourController TourController { get; set; } = new TourController();
        public TouristController TouristController { get; set; } = new TouristController();
        public TourTouristCheckpointController TourTouristCheckpointController { get; set; } = new TourTouristCheckpointController();
        public TourTouristController TourTouristController { get; set; } = new TourTouristController();

        public App()
        {

            AccommodationController.LoadLocationsToAccommodations(LocationController.GetAll());
            AccommodationController.LoadPhotosToAccommodations(PhotoController.GetAll());
            CheckpointController.LoadLocationsToCheckpoints(LocationController.GetAll());
            TourController.LoadLocationsToTours(LocationController.GetAll());
            TourController.LoadPhotosToTours(PhotoController.GetAll());
            ReservationController.LoadAccommodationsToReservations(AccommodationController.GetAll());
            ReservationController.LoadGuestsToReservations(GuestController.GetAll());
            TourCheckpointController.LoadCheckpoints(CheckpointController.GetAll());
            OwnerController.LoadAccommodationsToOwners(AccommodationController.GetAll());
            GuideController.LoadToursToGuide(TourController.GetAll());

            TourController.LoadTouristsToTours(TourTouristController.GetAll(),TouristController.GetAll());
            TouristController.LoadToursToTourist(TourTouristController.GetAll(), TourController.GetAll());
            TourController.LoadCheckpointsToTours(TourCheckpointController.GetAll(), CheckpointController.GetAll());
        }
    }
}
