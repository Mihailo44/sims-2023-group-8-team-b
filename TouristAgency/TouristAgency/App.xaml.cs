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


        public GuideService GuideService { get; }

        //public CheckpointService CheckpointService { get; set; } = new CheckpointService();
        public GuestViewModel GuestViewModel { get; set; } = new GuestViewModel();
        public LocationViewModel LocationViewModel { get; set; } = new LocationViewModel();
        //public GuideService GuideService { get; set; } = new GuideService();
        //public PhotoService PhotoService { get; set; } = new PhotoService();
        //public TourCheckpointService TourCheckpointService { get; set; } = new TourCheckpointService();
        //public TourService TourService { get; set; } = new TourService();
        public TouristViewModel TouristViewModel { get; set; } = new TouristViewModel();
        //public TourTouristCheckpointService TourTouristCheckpointService { get; set; } = new TourTouristCheckpointService();
        //public TourTouristService TourTouristService { get; set; } = new TourTouristService();
        public VoucherViewModel VoucherViewModel { get; set; } = new VoucherViewModel();

        public App()
        {
            ReservationService = new();
            AccommodationService = new();
            GuestReviewService = new();
            OwnerService = new();
            UserService = new();
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

            //TODO Preci na servise
            AccommodationService.LoadLocationsToAccommodations(LocationViewModel.GetAll());
            AccommodationService.LoadPhotosToAccommodations(PhotoService.GetAll());
            OwnerService.LoadAccommodationsToOwners(AccommodationService.GetAll());
            OwnerService.LoadLocationsToOwners(LocationViewModel.GetAll());
            GuestReviewService.LoadGuestsToGuestReviews(GuestViewModel.GetAll());
            CheckpointService.LoadLocationsToCheckpoints(LocationViewModel.GetAll());
            TourService.LoadLocationsToTours(LocationViewModel.GetAll());
            TourService.LoadPhotosToTours(PhotoService.GetAll());
            ReservationService.LoadAccommodationsToReservations(AccommodationService.GetAll());
            ReservationService.LoadGuestsToReservations(GuestViewModel.GetAll());
            TourCheckpointService.LoadCheckpoints(CheckpointService.GetAll());
            GuideService.LoadToursToGuide(TourService.GetAll());
            TourService.LoadTouristsToTours(TourTouristService.GetAll(), TouristViewModel.GetAll());
            TouristViewModel.LoadToursToTourist(TourTouristService.GetAll(), TourService.GetAll());
            TourService.LoadCheckpointsToTours(TourCheckpointService.GetAll(), CheckpointService.GetAll());
            OwnerReviewService.LoadReservationsToOwnerReviews(ReservationService.GetAll());
            PostponementRequestService.LoadReservationsToPostponementRequests(ReservationService.GetAll());
            TouristService.LoadVouchersToTourist(VoucherService.GetAll());
        }
    }
}
