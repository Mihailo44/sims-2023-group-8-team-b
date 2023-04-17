using System;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Service;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ReservationService ReservationService { get; }
        //public AccommodationService AccommodationService { get; }
        //public GuestReviewService GuestReviewService { get; }   
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
        public GuideReviewService GuideReviewService { get; }

        public AccommodationRepository AccommodationRepository { get; }
        public GuestReviewRepository GuestReviewRepository { get; }

        public event Action CurrentVMChanged;

        private ViewModelBase _currentVm;
        public ViewModelBase CurrentVM
        {
            get => _currentVm;
            set
            {
                _currentVm = value;
                OnCurrentVMChanged();
            }
        }

        private void OnCurrentVMChanged()
        {
            if (CurrentVMChanged != null)
            {
                CurrentVMChanged.Invoke();
            }
        }

        public dynamic LoggedUser { get; set; }

        public App()
        {
            GuideService = new(InjectorService.CreateInstance<IStorage<Guide>>());
            TourTouristService = new(InjectorService.CreateInstance<IStorage<TourTourist>>());
            UserService = new(InjectorService.CreateInstance<IStorage<User>>());
            LocationService = new(InjectorService.CreateInstance<IStorage<Location>>());
            ReservationService = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            AccommodationRepository = new(InjectorService.CreateInstance<IStorage<Accommodation>>());
            GuestReviewRepository = new(InjectorService.CreateInstance<IStorage<GuestReview>>());
            OwnerService = new(InjectorService.CreateInstance<IStorage<Owner>>());
            OwnerReviewService = new(InjectorService.CreateInstance<IStorage<OwnerReview>>());
            PostponementRequestService = new(InjectorService.CreateInstance<IStorage<PostponementRequest>>());
            TouristService = new(InjectorService.CreateInstance<IStorage<Tourist>>());
            GuideReviewService = new(InjectorService.CreateInstance<IStorage<GuideReview>>());

            VoucherService = new(InjectorService.CreateInstance<IStorage<Voucher>>());
            TourService = new(InjectorService.CreateInstance<IStorage<Tour>>());
            TourCheckpointService = new(InjectorService.CreateInstance<IStorage<TourCheckpoint>>());
            TourTouristCheckpointService = new(InjectorService.CreateInstance<IStorage<TourTouristCheckpoint>>());
            CheckpointService = new(InjectorService.CreateInstance<IStorage<Checkpoint>>());

            PhotoService = new(InjectorService.CreateInstance<IStorage<Photo>>());
            GuestService = new(InjectorService.CreateInstance<IStorage<Guest>>());

            AccommodationRepository.LoadLocationsToAccommodations(LocationService.GetAll());
            AccommodationRepository.LoadPhotosToAccommodations(PhotoService.GetAll());
            AccommodationRepository.LoadOwnersToAccommodations(OwnerService.GetAll());
            OwnerService.LoadAccommodationsToOwners(AccommodationRepository.GetAll());
            OwnerService.LoadLocationsToOwners(LocationService.GetAll());
            GuestReviewRepository.LoadReservationsToGuestReviews(ReservationService.GetAll());
            CheckpointService.LoadLocationsToCheckpoints(LocationService.GetAll());
            TourService.LoadLocationsToTours(LocationService.GetAll());
            TourService.LoadPhotosToTours(PhotoService.GetAll());
            OwnerReviewService.LoadPhotosToReviews(PhotoService.GetAll());
            ReservationService.LoadAccommodationsToReservations(AccommodationRepository.GetAll());
            ReservationService.LoadGuestsToReservations(GuestService.GetAll());
            TourCheckpointService.LoadCheckpoints(CheckpointService.GetAll());
            GuideService.LoadToursToGuide(TourService.GetAll());
            GuideReviewService.LoadTouristsToReviews(TouristService.GetAll());
            GuideReviewService.LoadToursToGuideReviews(TourService.GetAll());
            TourService.LoadTouristsToTours(TourTouristService.GetAll(), TouristService.GetAll());
            TouristService.LoadToursToTourist(TourTouristService.GetAll(), TourService.GetAll());
            TourService.LoadCheckpointsToTours(TourCheckpointService.GetAll(), CheckpointService.GetAll());
            OwnerReviewService.LoadReservationsToOwnerReviews(ReservationService.GetAll());
            PostponementRequestService.LoadReservationsToPostponementRequests(ReservationService.GetAll());
            TouristService.LoadVouchersToTourist(VoucherService.GetAll());
            GuideReviewService.LoadPhotosToReviews(PhotoService.GetAll());
        }
    }
}
