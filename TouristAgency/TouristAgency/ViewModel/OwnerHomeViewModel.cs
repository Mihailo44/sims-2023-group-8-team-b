using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.Enums;
using TouristAgency.Service;
using TouristAgency.View.Creation;

namespace TouristAgency.ViewModel
{
    public class OwnerHomeViewModel : ViewModelBase, IObserver
    {
        private ReservationService _reservationService;
        private AccommodationService _accommodationService;
        private OwnerReviewService _ownerReviewService;
        private OwnerService _ownerService;
        private PostponementRequestService _postponementRequestService;

        public string Status { get; set; }

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }

        public ObservableCollection<PostponementRequest> PostponementRequests { get; set; }
        public PostponementRequest SelectedRequest { get; set; }

        public ObservableCollection<OwnerReview> OwnerReviews { get; set; }

        public Owner LoggedUser { get; set; }

        private readonly Window _window;
        private App app = (App)App.Current;

        public DelegateCommand NewAccommodationCmd { get; }
        public DelegateCommand NewReviewCmd { get; }
        public DelegateCommand PostponeCmd { get; }

        public OwnerHomeViewModel()
        {

        }

        public OwnerHomeViewModel(Window window, Owner owner)
        {
            _window = window;
            LoggedUser = owner;

            _reservationService = app.ReservationService;
            _reservationService.Subscribe(this);

            _accommodationService = app.AccommodationService;
            _accommodationService.Subscribe(this);

            _ownerReviewService = app.OwnerReviewService;
            _ownerReviewService.Subscribe(this);

            _postponementRequestService = app.PostponementRequestService;
            _postponementRequestService.Subscribe(this);

            _ownerService = app.OwnerService;

            SetUserStatus();

            Accommodations = new ObservableCollection<Accommodation>();
            LoadAccommodations(LoggedUser.ID);

            Reservations = new ObservableCollection<Reservation>();
            LoadReservations(LoggedUser.ID);

            PostponementRequests = new ObservableCollection<PostponementRequest>();
            LoadPostponementRequests(LoggedUser.ID);

            OwnerReviews = new ObservableCollection<OwnerReview>();
            LoadOwnerReviews(LoggedUser.ID);

            ReviewNotification();

            NewAccommodationCmd = new DelegateCommand(param => OpenAccommodationCreationExecute(), param => CanOpenAccommodationCreationExecute());
            NewReviewCmd = new DelegateCommand(param => OpenGuestReviewCreation(), param => CanOpenGuestReviewCreation());
            PostponeCmd = new DelegateCommand(param => PostponeReservationExecute(), param => CanPostponeReservationExecute()); 
        }

        private void LoadAccommodations(int ownerId = 0)
        {
            Accommodations.Clear();
            List<Accommodation> accommodations = _accommodationService.GetByOwnerId(ownerId);
            foreach (var accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }

        private void LoadReservations(int ownerId = 0)
        {
            Reservations.Clear();
            List<Reservation> reservations = _reservationService.GetByOwnerId(ownerId);
            foreach (var reservation in reservations)
            {
                Reservations.Add(reservation);
            }
        }

        public void LoadPostponementRequests(int ownerId = 0)
        {
            PostponementRequests.Clear();
            List<PostponementRequest> postponementRequests = _postponementRequestService.GetByOwnerId(ownerId);
            foreach (var postponementRequest in postponementRequests)
            {
                PostponementRequests.Add(postponementRequest);
            }
        }

        public void LoadOwnerReviews(int ownerId = 0)
        {
            OwnerReviews.Clear();
            List<OwnerReview> ownerReviews = _ownerReviewService.GetReviewedReservationsByOwnerId(ownerId);
            foreach (var ownerReview in ownerReviews)
            {
                OwnerReviews.Add(ownerReview);
            }
        }

        private void ReviewNotification()
        {
            int changes;
            string notification = _reservationService.ReviewNotification(LoggedUser.ID, out changes);
            if (changes > 0)
            {
                MessageBox.Show(notification);
            }
        }

        public void Update()
        {
            LoadAccommodations(LoggedUser.ID);
            LoadReservations(LoggedUser.ID);
            LoadOwnerReviews(LoggedUser.ID);
            LoadPostponementRequests(LoggedUser.ID);
        }

        public void SetUserStatus()
        {
            LoggedUser.SuperUser = _ownerService.IsSuperOwner(_ownerReviewService.GetByOwnerId(LoggedUser.ID));
            if (LoggedUser.SuperUser)
                Status = "SUPER OWNER";
            else
                Status = "";
        }

        public bool CanOpenAccommodationCreationExecute()
        {
            return true;
        }

        public void OpenAccommodationCreationExecute()
        {
            AccommodationCreation x = new AccommodationCreation(LoggedUser);
            x.Show();
        }

        public bool CanOpenGuestReviewCreation()
        {
            DateTime today = DateTime.UtcNow.Date;
            double dateDif = (today - SelectedReservation.End).TotalDays;

            if (SelectedReservation.Status == GuestReviewStatus.UNREVIEWED && dateDif < 5.0)
            {
                return true;
            }
            else
            {
                if (dateDif > 5.0)
                {
                    MessageBox.Show("Guest review time window expired");
                }
                else if (SelectedReservation.Status == GuestReviewStatus.REVIEWED)
                {
                    MessageBox.Show("Guest has already been reviewed");
                }

                return false;
            }
        }

        public void OpenGuestReviewCreation()
        {
            if (SelectedReservation != null)
            {
                GuestReviewCreation x = new GuestReviewCreation(SelectedReservation);
                x.Show();
            }
        }

        public bool CanPostponeReservationExecute()
        {
            if (SelectedRequest != null)
                return true;
            else
                return false;
        }

        public void PostponeReservationExecute()
        {
            MessageBoxResult result = ApprovePostponementRequest();
            if (result == MessageBoxResult.Yes)
            {
                Reservation postponed = _reservationService.FindById(SelectedRequest.Reservation.Id);
                if (postponed != null)
                {
                    postponed.Start = SelectedRequest.Start;
                    postponed.End = SelectedRequest.End;
                    _reservationService.Update(postponed, postponed.Id);
                    SelectedRequest.Status = PostponementRequestStatus.APPROVED;
                    _postponementRequestService.Update(SelectedRequest, SelectedRequest.Id);
                }
            }
        }

        private MessageBoxResult ApprovePostponementRequest()
        {
            string sMessageBoxText;
            string sCaption;

            sMessageBoxText = $"Do you want to approve postponement request?\n{SelectedRequest.Start}\n{SelectedRequest.End}";
            sCaption = "Postponement Request Dialog";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }
    }
}
