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
using TouristAgency.View.Dialogue;

namespace TouristAgency.ViewModel
{
    public class OwnerHomeViewModel : ViewModelBase, IObserver,ICloseable
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
        public DelegateCommand PostponeCommentCmd { get; }
        public DelegateCommand CloseCmd { get; }

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
            //PostponeCommentCmd = new DelegateCommand(param => OpenPostponeCommentExecute(), param => CanOpenPostponeCommentExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(),param => CanCloseWindowExecute());
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
            double average;
            LoggedUser.SuperOwner = _ownerService.IsSuperOwner(_ownerReviewService.GetByOwnerId(LoggedUser.ID),out average);
            LoggedUser.Average = average;
            if (LoggedUser.SuperOwner)
                Status = $"SUPER OWNER ({LoggedUser.Average})";
            else
                Status = "";

            _ownerService.Update(LoggedUser, LoggedUser.ID);
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
            if (SelectedReservation != null)
            {
                DateTime today = DateTime.UtcNow.Date;
                double dateDif = (today - SelectedReservation.End).TotalDays;

                if (SelectedReservation.Status == ReviewStatus.UNREVIEWED && dateDif < 5.0)
                {
                    return true;
                }
                else
                {
                    if (dateDif > 5.0)
                    {
                        MessageBox.Show("Guest review time window expired");
                    }
                    else if (SelectedReservation.Status == ReviewStatus.REVIEWED)
                    {
                        MessageBox.Show("Guest has already been reviewed");
                    }

                    return false;
                }
            }
            else
            {
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
            if (SelectedRequest != null)
            {
                Reservation reservation = _reservationService.FindById(SelectedRequest.Reservation.Id);
                PostponementRequest request = _postponementRequestService.FindById(SelectedRequest.Id);
                bool accommodationAvailability = _reservationService.IsReserved(reservation.Id, SelectedRequest.Start, SelectedRequest.End);

                if (result == MessageBoxResult.Yes)
                {
                    if (!accommodationAvailability)
                    {
                        reservation.Start = SelectedRequest.Start;
                        reservation.End = SelectedRequest.End;
                        _reservationService.Update(reservation, reservation.Id);

                        request.Status = PostponementRequestStatus.APPROVED;
                        OpenPostponeCommentExecute(request); // ovo ce trebati drugacije
                    }
                    else
                    {
                        MessageBox.Show("Postponement is not possible");
                        request.Status = PostponementRequestStatus.DENIED;
                        request.Comment = "Sorry, the accommodation is reserved in this timeframe";
                        _postponementRequestService.Update(request, request.Id);
                    }
                }
                if (result == MessageBoxResult.No)
                {
                    request.Status = PostponementRequestStatus.DENIED;
                    OpenPostponeCommentExecute(request);
                }
            }
        }

        public bool CanOpenPostponeCommentExecute()
        {
            return true;
        }

        public void OpenPostponeCommentExecute(PostponementRequest postponementRequest)
        {
            PostponementRequestCommentDialogue x = new PostponementRequestCommentDialogue(postponementRequest);
            x.Show();
        }

        private MessageBoxResult ApprovePostponementRequest()
        {
            string sMessageBoxText;
            string sCaption;

            sMessageBoxText = $"Do you want to approve postponement request?\nStart Date:\t{SelectedRequest.Start}\nEnd Date:\t\t{SelectedRequest.End}";
            sCaption = "Postponement Request Dialog";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            _window.Close();
        }
    }
}
