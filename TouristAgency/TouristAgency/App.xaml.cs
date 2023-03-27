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
        public AccommodationViewModel AccommodationViewModel { get; set; } = new AccommodationViewModel();
        public CheckpointViewModel CheckpointViewModel { get; set; } = new CheckpointViewModel();    
        public GuestController GuestController { get; set; } = new GuestController();
        public GuestReviewViewModel GuestReviewViewModel { get; set; } = new GuestReviewViewModel();
        public LocationController LocationController { get; set; } = new LocationController();
        public OwnerViewModel OwnerViewModel { get; set; } = new OwnerViewModel();
        public GuideViewModel GuideViewModel { get; set; } = new GuideViewModel();
        public PhotoViewModel PhotoViewModel { get; set; } = new PhotoViewModel();
        public TourCheckpointViewModel TourCheckpointViewModel { get; set; } = new TourCheckpointViewModel();
        public TourViewModel TourViewModel { get; set; } = new TourViewModel();
        public TouristController TouristController { get; set; } = new TouristController();
        public TourTouristCheckpointViewModel TourTouristCheckpointViewModel { get; set; } = new TourTouristCheckpointViewModel();
        public TourTouristViewModel TourTouristViewModel { get; set; } = new TourTouristViewModel();

        public App()
        {
            AccommodationViewModel.LoadLocationsToAccommodations(LocationController.GetAll());
            AccommodationViewModel.LoadPhotosToAccommodations(PhotoViewModel.GetAll());
            CheckpointViewModel.LoadLocationsToCheckpoints(LocationController.GetAll());
            TourViewModel.LoadLocationsToTours(LocationController.GetAll());
            TourViewModel.LoadPhotosToTours(PhotoViewModel.GetAll());
            ReservationViewModel.LoadAccommodationsToReservations(AccommodationViewModel.GetAll());
            ReservationViewModel.LoadGuestsToReservations(GuestController.GetAll());
            TourCheckpointViewModel.LoadCheckpoints(CheckpointViewModel.GetAll());
            OwnerViewModel.LoadAccommodationsToOwners(AccommodationViewModel.GetAll());
            GuideViewModel.LoadToursToGuide(TourViewModel.GetAll());
            TourViewModel.LoadTouristsToTours(TourTouristViewModel.GetAll(),TouristController.GetAll());
            TouristController.LoadToursToTourist(TourTouristViewModel.GetAll(), TourViewModel.GetAll());
            TourViewModel.LoadCheckpointsToTours(TourCheckpointViewModel.GetAll(), CheckpointViewModel.GetAll());
            OwnerViewModel.LoadLocationsToOwners(LocationController.GetAll());
        }
    }
}
