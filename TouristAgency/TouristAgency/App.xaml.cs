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
        public CheckpointViewModel CheckpointViewModel { get; set; } = new CheckpointViewModel();    
        public GuestViewModel GuestViewModel { get; set; } = new GuestViewModel();
        public GuestReviewViewModel GuestReviewViewModel { get; set; } = new GuestReviewViewModel();
        public LocationViewModel LocationViewModel { get; set; } = new LocationViewModel();
        public OwnerViewModel OwnerViewModel { get; set; } = new OwnerViewModel();
        public GuideViewModel GuideViewModel { get; set; } = new GuideViewModel();
        public PhotoViewModel PhotoViewModel { get; set; } = new PhotoViewModel();
        public TourCheckpointViewModel TourCheckpointViewModel { get; set; } = new TourCheckpointViewModel();
        public TourViewModel TourViewModel { get; set; } = new TourViewModel();
        public TouristViewModel TouristViewModel { get; set; } = new TouristViewModel();
        public TourTouristCheckpointViewModel TourTouristCheckpointViewModel { get; set; } = new TourTouristCheckpointViewModel();
        public TourTouristViewModel TourTouristViewModel { get; set; } = new TourTouristViewModel();

        public App()
        {
            AccommodationCreationViewModel.LoadLocationsToAccommodations(LocationViewModel.GetAll());
            AccommodationCreationViewModel.LoadPhotosToAccommodations(PhotoViewModel.GetAll());
            CheckpointViewModel.LoadLocationsToCheckpoints(LocationViewModel.GetAll());
            TourViewModel.LoadLocationsToTours(LocationViewModel.GetAll());
            TourViewModel.LoadPhotosToTours(PhotoViewModel.GetAll());
            ReservationViewModel.LoadAccommodationsToReservations(AccommodationCreationViewModel.GetAll());
            AccommodationCreationViewModel.LoadLocationsToAccommodations(LocationViewModel.GetAll());
            AccommodationCreationViewModel.LoadPhotosToAccommodations(PhotoViewModel.GetAll());
            CheckpointViewModel.LoadLocationsToCheckpoints(LocationViewModel.GetAll());
            TourViewModel.LoadLocationsToTours(LocationViewModel.GetAll());
            TourViewModel.LoadPhotosToTours(PhotoViewModel.GetAll());
            ReservationViewModel.LoadAccommodationsToReservations(AccommodationCreationViewModel.GetAll());
            ReservationViewModel.LoadGuestsToReservations(GuestViewModel.GetAll());
            TourCheckpointViewModel.LoadCheckpoints(CheckpointViewModel.GetAll());
            OwnerViewModel.LoadAccommodationsToOwners(AccommodationCreationViewModel.GetAll());
            GuideViewModel.LoadToursToGuide(TourViewModel.GetAll());
            TourViewModel.LoadTouristsToTours(TourTouristViewModel.GetAll(),TouristViewModel.GetAll());
            TouristViewModel.LoadToursToTourist(TourTouristViewModel.GetAll(), TourViewModel.GetAll());
            TourViewModel.LoadCheckpointsToTours(TourCheckpointViewModel.GetAll(), CheckpointViewModel.GetAll());
            OwnerViewModel.LoadLocationsToOwners(LocationViewModel.GetAll());
            TourCheckpointViewModel.LoadCheckpoints(CheckpointViewModel.GetAll());
            OwnerViewModel.LoadAccommodationsToOwners(AccommodationCreationViewModel.GetAll());
            GuideViewModel.LoadToursToGuide(TourViewModel.GetAll());
            TourViewModel.LoadTouristsToTours(TourTouristViewModel.GetAll(),TouristViewModel.GetAll());
            TouristViewModel.LoadToursToTourist(TourTouristViewModel.GetAll(), TourViewModel.GetAll());
            TourViewModel.LoadCheckpointsToTours(TourCheckpointViewModel.GetAll(), CheckpointViewModel.GetAll());
            OwnerViewModel.LoadLocationsToOwners(LocationViewModel.GetAll());
        }
    }
}
