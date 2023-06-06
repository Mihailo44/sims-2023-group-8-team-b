using System;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.RenovationFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Notifications;
using TouristAgency.Notifications.Domain;
using TouristAgency.Review.Domain;
using TouristAgency.TourRequests;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Users;
using TouristAgency.Users.Domain;
using TouristAgency.Users.ForumFeatures.Domain;
using TouristAgency.Users.ReviewFeatures.Domain;
using TouristAgency.Users.SuperGuestFeature.Domain;
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
        public TouristNotificationRepository TouristNotificationRepository { get; }
        public GuideRepository GuideRepository { get; }
        public GuideReviewRepository GuideReviewRepository { get; }
        public AccommodationRepository AccommodationRepository { get; }
        public GuestReviewRepository GuestReviewRepository { get; }
        public OwnerRepository OwnerRepository { get; }
        public RenovationRepository RenovationRepository { get; }
        public RenovationRecommendationRepository RenovationRecommendationRepository { get; }
        public GuestReviewNotificationRepository GuestReviewNotificationRepository { get; }
        public SuperGuestTitleRepository SuperGuestTitleRepository { get; }
        public ForumRepository ForumRepository { get; }
        public ForumCommentRepository ForumCommentRepository { get; }
        public ForumNotificationRepository ForumNotificationRepository { get; }
        public UserCommentRepository UserCommentRepository { get; }
        public ComplexTourRequestRepository ComplexTourRequestRepository { get; }

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
            GuideRepository = new(InjectorService.CreateInstance<IStorage<Guide>>());
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
            TouristNotificationRepository = new(InjectorService.CreateInstance<IStorage<TouristNotification>>());
            PhotoRepository = new(InjectorService.CreateInstance<IStorage<Photo>>());
            GuestRepository = new(InjectorService.CreateInstance<IStorage<Guest>>());
            RenovationRepository = new(InjectorService.CreateInstance<IStorage<Renovation>>());
            RenovationRecommendationRepository = new(InjectorService.CreateInstance<IStorage<RenovationRecommendation>>());
            GuestReviewNotificationRepository = new(InjectorService.CreateInstance<IStorage<GuestReviewNotification>>());
            SuperGuestTitleRepository = new(InjectorService.CreateInstance<IStorage<SuperGuestTitle>>());
            ForumRepository = new(InjectorService.CreateInstance<IStorage<Forum>>());
            ForumCommentRepository = new(InjectorService.CreateInstance<IStorage<ForumComment>>());
            ForumNotificationRepository = new(InjectorService.CreateInstance<IStorage<ForumNotification>>());
            UserCommentRepository = new(InjectorService.CreateInstance<IStorage<UserComment>>());
            ComplexTourRequestRepository = new(InjectorService.CreateInstance<IStorage<ComplexTourRequest>>());

            LoadData();
        }

        private void LoadData() 
        {
            AccommodationRepository.LoadLocationsToAccommodations(LocationRepository.GetAll());
            AccommodationRepository.LoadPhotosToAccommodations(PhotoRepository.GetAll());
            AccommodationRepository.LoadOwnersToAccommodations(OwnerRepository.GetAll());
            OwnerRepository.LoadAccommodationsToOwners(AccommodationRepository.GetAll());
            OwnerRepository.LoadLocationsToOwners(LocationRepository.GetAll());
            OwnerRepository.LoadOwnerCredentials(UserRepository.GetAll());
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
            TourRepository.LoadGuidesToTours(GuideRepository.GetAll());
            TouristRepository.LoadToursToTourist(TourTouristRepository.GetAll(), TourRepository.GetAll());
            TourRepository.LoadCheckpointsToTours(TourCheckpointRepository.GetAll(), CheckpointRepository.GetAll());
            OwnerReviewRepository.LoadReservationsToOwnerReviews(ReservationRepository.GetAll());
            PostponementRequestRepository.LoadReservationsToPostponementRequests(ReservationRepository.GetAll());
            TouristRepository.LoadVouchersToTourist(VoucherRepository.GetAll());
            GuideReviewRepository.LoadPhotosToReviews(PhotoRepository.GetAll());
            TourRequestRepository.LoadLocationsToTourRequests(LocationRepository.GetAll());
            RenovationRepository.LoadAccommodationsToRenovations(AccommodationRepository.GetAll());
            TouristRepository.LoadUsersToTourists(UserRepository.GetAll());
            RenovationRecommendationRepository.LoadReservationsToRenovationRecommendation(ReservationRepository.GetAll());
            TouristNotificationRepository.LoadToursToNotifications(TourRepository.GetAll());
            TouristNotificationRepository.LoadCheckpointsToNotifications(CheckpointRepository.GetAll());
            GuestReviewNotificationRepository.LoadReservationsToNotifications(ReservationRepository.GetAll());
            SuperGuestTitleRepository.LoadGuestsToSuperGuestTitles(GuestRepository.GetAll());
            ForumRepository.LoadLocationsToForums(LocationRepository.GetAll());
            ForumCommentRepository.LoadForumsToComments(ForumRepository.GetAll());
            ForumCommentRepository.LoadUsersToComments(UserRepository.GetAll());
            ForumNotificationRepository.LoadForumsToNotifications(ForumRepository.GetAll());
            ComplexTourRequestRepository.LoadTouristsToComplexTourRequests(TouristRepository.GetAll());
            ComplexTourRequestRepository.LoadTourRequestsToComplexTourRequests(TourRequestRepository.GetAll());
            TourRequestRepository.LoadTouristsToTourRequests(TouristRepository.GetAll());
            GuestRepository.LoadReservationsToGuests(ReservationRepository.GetAll());
            TouristRepository.LoadLocationsToTourists(LocationRepository.GetAll());
        }
    }
}
