using System;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Review;
using TouristAgency.Service;
using TouristAgency.TourRequests;
using TouristAgency.Tours;
using TouristAgency.Users;
using TouristAgency.Util;
using TouristAgency.Vouchers;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ReservationRepository ReservationRepository { get; }
        //public AccommodationService AccommodationService { get; }
        //public GuestReviewService GuestReviewService { get; }   
        public OwnerService OwnerService { get; }
        public UserRepository UserRepository { get; }
        public OwnerReviewRepository OwnerReviewRepository { get; }
        public PostponementRequestRepository PostponementRequestRepository { get; }
        public TouristRepository TouristRepository { get; }
        public VoucherRepository VoucherRepository { get; }
        public TourRepository TourRepository { get; }
        public TourTouristRepository TourTouristRepository { get; }
        public TourCheckpointRepository TourCheckpointRepository { get; }
        public TourTouristCheckpointRepository TourTouristCheckpointRepository { get; }
        public CheckpointRepository CheckpointRepository { get; }
        public PhotoRepository PhotoRepository { get; }
        public GuestRepository GuestRepository { get; }
        public LocationRepository LocationRepository { get; }
        public TourRequestRepository TourRequestRepository { get; }


        public GuideRepository GuideRepository { get; }
        public GuideReviewRepository GuideReviewRepository { get; }

        public AccommodationRepository AccommodationRepository { get; }
        public GuestReviewRepository GuestReviewRepository { get; }
        public OwnerRepository OwnerRepository { get; }

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
            UserRepository = new(InjectorService.CreateInstance<IStorage<User>>());
            LocationRepository = new(InjectorService.CreateInstance<IStorage<Location>>());
            ReservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            AccommodationRepository = new(InjectorService.CreateInstance<IStorage<Accommodation>>());
            GuestReviewRepository = new(InjectorService.CreateInstance<IStorage<GuestReview>>());
            OwnerRepository = new(InjectorService.CreateInstance<IStorage<Owner>>());
            OwnerReviewRepository = new(InjectorService.CreateInstance<IStorage<OwnerReview>>());
            PostponementRequestRepository = new(InjectorService.CreateInstance<IStorage<PostponementRequest>>());
            TouristRepository = new(InjectorService.CreateInstance<IStorage<Tourist>>());
            GuideReviewRepository = new(InjectorService.CreateInstance<IStorage<GuideReview>>());
            TourRequestRepository = new(InjectorService.CreateInstance<IStorage<TourRequest>>());

            VoucherRepository = new(InjectorService.CreateInstance<IStorage<Voucher>>());
            TourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            TourCheckpointRepository = new(InjectorService.CreateInstance<IStorage<TourCheckpoint>>());
            TourTouristCheckpointRepository = new(InjectorService.CreateInstance<IStorage<TourTouristCheckpoint>>());
            CheckpointRepository = new(InjectorService.CreateInstance<IStorage<Checkpoint>>());

            PhotoRepository = new(InjectorService.CreateInstance<IStorage<Photo>>());
            GuestRepository = new(InjectorService.CreateInstance<IStorage<Guest>>());

            AccommodationRepository.LoadLocationsToAccommodations(LocationRepository.GetAll());
            AccommodationRepository.LoadPhotosToAccommodations(PhotoRepository.GetAll());
            AccommodationRepository.LoadOwnersToAccommodations(OwnerRepository.GetAll());
            OwnerRepository.LoadAccommodationsToOwners(AccommodationRepository.GetAll());
            OwnerRepository.LoadLocationsToOwners(LocationRepository.GetAll());
            GuestReviewRepository.LoadReservationsToGuestReviews(ReservationRepository.GetAll());
            CheckpointRepository.LoadLocationsToCheckpoints(LocationRepository.GetAll());
            TourRepository.LoadLocationsToTours(LocationRepository.GetAll());
            TourRepository.LoadPhotosToTours(PhotoRepository.GetAll());
            OwnerReviewRepository.LoadPhotosToReviews(PhotoRepository.GetAll());
            ReservationRepository.LoadAccommodationsToReservations(AccommodationRepository.GetAll());
            ReservationRepository.LoadGuestsToReservations(GuestRepository.GetAll());
            TourCheckpointRepository.LoadCheckpoints(CheckpointRepository.GetAll());
            GuideRepository.LoadToursToGuide(TourRepository.GetAll());
            GuideReviewRepository.LoadTouristsToReviews(TouristRepository.GetAll());
            GuideReviewRepository.LoadToursToGuideReviews(TourRepository.GetAll());
            TourRepository.LoadTouristsToTours(TourTouristRepository.GetAll(), TouristRepository.GetAll());
            TouristRepository.LoadToursToTourist(TourTouristRepository.GetAll(), TourRepository.GetAll());
            TourRepository.LoadCheckpointsToTours(TourCheckpointRepository.GetAll(), CheckpointRepository.GetAll());
            OwnerReviewRepository.LoadReservationsToOwnerReviews(ReservationRepository.GetAll());
            PostponementRequestRepository.LoadReservationsToPostponementRequests(ReservationRepository.GetAll());
            TouristRepository.LoadVouchersToTourist(VoucherRepository.GetAll());
            GuideReviewRepository.LoadPhotosToReviews(PhotoRepository.GetAll());
            TourRequestRepository.LoadLocationsToTourRequests(LocationRepository.GetAll());
        }
    }
}
