using System;
using System.Windows;
using TouristAgency.Service;
using TouristAgency.ViewModel;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ReservationService ReservationService { get; }
        public AccommodationService AccommodationService { get; }
        public GuestReviewService GuestReviewService { get; }   
        public OwnerService OwnerService { get; }
        public UserService UserService { get; }
        public OwnerReviewService OwnerReviewService { get; }
        public PostponementRequestService PostponementRequestService { get; }
        public TouristService TouristService { get; }
        public VoucherService VoucherService { get; }

        public TourService TourService { get; }
        public TourTouristService TourTouristService { get; }
        public TourCheckpointService TourCheckpointService { get; }
        public TourTouristCheckpointService TourTouristCheckpointService { get; }
        public CheckpointService CheckpointService { get; }
        public PhotoService PhotoService { get; }
        public GuestService GuestService { get; }
        public LocationService LocationService { get; }


        public GuideService GuideService { get; }

        //public CheckpointService CheckpointService { get; set; } = new CheckpointService();
        //public GuestViewModel GuestViewModel { get; set; } = new GuestViewModel();
        //public LocationService LocationService { get; set; } = new LocationService();
        //public GuideService GuideService { get; set; } = new GuideService();
        //public PhotoService PhotoService { get; set; } = new PhotoService();
        //public TourCheckpointService TourCheckpointService { get; set; } = new TourCheckpointService();
        //public TourService TourService { get; set; } = new TourService();
        //public TouristService TouristService { get; set; } = new TouristService();
        //public TourTouristCheckpointService TourTouristCheckpointService { get; set; } = new TourTouristCheckpointService();
        //public TourTouristService TourTouristService { get; set; } = new TourTouristService();
        //public VoucherService VoucherService { get; set; } = new VoucherService();

        public App()
        {
            LocationService = new();
            ReservationService = new();
            AccommodationService = new();
            GuestReviewService = new();
            OwnerService = new();
            OwnerReviewService = new();
            PostponementRequestService = new();
            TouristService = new();
            TourTouristService = new();
            VoucherService = new();
            TourService = new();
            TourCheckpointService = new();
            TourTouristCheckpointService = new();
            CheckpointService = new();
            GuideService = new();
            PhotoService = new();
            GuestService = new();
            UserService = new();

            //TODO Preci na servise
            AccommodationService.LoadLocationsToAccommodations(LocationService.GetAll());
            AccommodationService.LoadPhotosToAccommodations(PhotoService.GetAll());
            OwnerService.LoadAccommodationsToOwners(AccommodationService.GetAll());
            OwnerService.LoadLocationsToOwners(LocationService.GetAll());
            GuestReviewService.LoadReservationsToGuestReviews(ReservationService.GetAll());
            CheckpointService.LoadLocationsToCheckpoints(LocationService.GetAll());
            TourService.LoadLocationsToTours(LocationService.GetAll());
            TourService.LoadPhotosToTours(PhotoService.GetAll());
            ReservationService.LoadAccommodationsToReservations(AccommodationService.GetAll());
            ReservationService.LoadGuestsToReservations(GuestService.GetAll());
            TourCheckpointService.LoadCheckpoints(CheckpointService.GetAll());
            GuideService.LoadToursToGuide(TourService.GetAll());
            TourService.LoadTouristsToTours(TourTouristService.GetAll(), TouristService.GetAll());
            TouristService.LoadToursToTourist(TourTouristService.GetAll(), TourService.GetAll());
            TourService.LoadCheckpointsToTours(TourCheckpointService.GetAll(), CheckpointService.GetAll());
            OwnerReviewService.LoadReservationsToOwnerReviews(ReservationService.GetAll());
            PostponementRequestService.LoadReservationsToPostponementRequests(ReservationService.GetAll());
            TouristService.LoadVouchersToTourist(VoucherService.GetAll());
            Console.WriteLine("test");
        }
    }
}
