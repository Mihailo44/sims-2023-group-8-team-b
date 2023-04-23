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
        public TourRepository TourRepository { get; }
        public TourTouristRepository TourTouristRepository { get; }
        public TourCheckpointRepository TourCheckpointRepository { get; }
        public TourTouristCheckpointRepository TourTouristCheckpointRepository { get; }
        public CheckpointService CheckpointService { get; }
        public PhotoRepository PhotoRepository { get; }
        public GuestService GuestService { get; }
        public LocationService LocationService { get; }


        public GuideRepository GuideRepository { get; }
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
            GuideRepository = new (InjectorService.CreateInstance<IStorage<Guide>>());
            TourTouristRepository = new(InjectorService.CreateInstance<IStorage<TourTourist>>());
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
            TourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            TourCheckpointRepository = new(InjectorService.CreateInstance<IStorage<TourCheckpoint>>());
            TourTouristCheckpointRepository = new(InjectorService.CreateInstance<IStorage<TourTouristCheckpoint>>());
            CheckpointService = new(InjectorService.CreateInstance<IStorage<Checkpoint>>());

            PhotoRepository = new(InjectorService.CreateInstance<IStorage<Photo>>());
            GuestService = new(InjectorService.CreateInstance<IStorage<Guest>>());

            AccommodationRepository.LoadLocationsToAccommodations(LocationService.GetAll());
            AccommodationRepository.LoadPhotosToAccommodations(PhotoRepository.GetAll());
            AccommodationRepository.LoadOwnersToAccommodations(OwnerService.GetAll());
            OwnerService.LoadAccommodationsToOwners(AccommodationRepository.GetAll());
            OwnerService.LoadLocationsToOwners(LocationService.GetAll());
            GuestReviewRepository.LoadReservationsToGuestReviews(ReservationService.GetAll());
            CheckpointService.LoadLocationsToCheckpoints(LocationService.GetAll());
            TourRepository.LoadLocationsToTours(LocationService.GetAll());
            TourRepository.LoadPhotosToTours(PhotoRepository.GetAll());
            OwnerReviewService.LoadPhotosToReviews(PhotoRepository.GetAll());
            ReservationService.LoadAccommodationsToReservations(AccommodationRepository.GetAll());
            ReservationService.LoadGuestsToReservations(GuestService.GetAll());
            TourCheckpointRepository.LoadCheckpoints(CheckpointService.GetAll());
            GuideRepository.LoadToursToGuide(TourRepository.GetAll());
            GuideReviewService.LoadTouristsToReviews(TouristService.GetAll());
            GuideReviewService.LoadToursToGuideReviews(TourRepository.GetAll());
            TourRepository.LoadTouristsToTours(TourTouristRepository.GetAll(), TouristService.GetAll());
            TouristService.LoadToursToTourist(TourTouristRepository.GetAll(), TourRepository.GetAll());
            TourRepository.LoadCheckpointsToTours(TourCheckpointRepository.GetAll(), CheckpointService.GetAll());
            OwnerReviewService.LoadReservationsToOwnerReviews(ReservationService.GetAll());
            PostponementRequestService.LoadReservationsToPostponementRequests(ReservationService.GetAll());
            TouristService.LoadVouchersToTourist(VoucherService.GetAll());
            GuideReviewService.LoadPhotosToReviews(PhotoRepository.GetAll());
        }
    }
}
