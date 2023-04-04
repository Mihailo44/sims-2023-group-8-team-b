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
       
        public CheckpointViewModel CheckpointViewModel { get; set; } = new CheckpointViewModel();
        public GuestViewModel GuestViewModel { get; set; } = new GuestViewModel();
        public LocationViewModel LocationViewModel { get; set; } = new LocationViewModel();
        public GuideViewModel GuideViewModel { get; set; } = new GuideViewModel();
        public PhotoViewModel PhotoViewModel { get; set; } = new PhotoViewModel();
        public TourCheckpointViewModel TourCheckpointViewModel { get; set; } = new TourCheckpointViewModel();
        public TourViewModel TourViewModel { get; set; } = new TourViewModel();
        public TouristViewModel TouristViewModel { get; set; } = new TouristViewModel();
        public TourTouristCheckpointViewModel TourTouristCheckpointViewModel { get; set; } = new TourTouristCheckpointViewModel();
        public TourTouristViewModel TourTouristViewModel { get; set; } = new TourTouristViewModel();
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
            VoucherService = new();
            TourService = new();

            //TODO Preci na servise
            AccommodationService.LoadLocationsToAccommodations(LocationViewModel.GetAll());
            AccommodationService.LoadPhotosToAccommodations(PhotoViewModel.GetAll());
            OwnerService.LoadAccommodationsToOwners(AccommodationService.GetAll());
            OwnerService.LoadLocationsToOwners(LocationViewModel.GetAll());
            GuestReviewService.LoadGuestsToGuestReviews(GuestViewModel.GetAll());
            CheckpointViewModel.LoadLocationsToCheckpoints(LocationViewModel.GetAll());
            TourViewModel.LoadLocationsToTours(LocationViewModel.GetAll());
            TourViewModel.LoadPhotosToTours(PhotoViewModel.GetAll());
            ReservationService.LoadAccommodationsToReservations(AccommodationService.GetAll());
            ReservationService.LoadGuestsToReservations(GuestViewModel.GetAll());
            TourCheckpointViewModel.LoadCheckpoints(CheckpointViewModel.GetAll());
            GuideViewModel.LoadToursToGuide(TourViewModel.GetAll());
            TourViewModel.LoadTouristsToTours(TourTouristViewModel.GetAll(), TouristViewModel.GetAll());
            TouristViewModel.LoadToursToTourist(TourTouristViewModel.GetAll(), TourViewModel.GetAll());
            TourViewModel.LoadCheckpointsToTours(TourCheckpointViewModel.GetAll(), CheckpointViewModel.GetAll());
            OwnerReviewService.LoadReservationsToOwnerReviews(ReservationService.GetAll());
            PostponementRequestService.LoadReservationsToPostponementRequests(ReservationService.GetAll());
            TouristService.LoadVouchersToTourist(VoucherService.GetAll());
        }
    }
}
