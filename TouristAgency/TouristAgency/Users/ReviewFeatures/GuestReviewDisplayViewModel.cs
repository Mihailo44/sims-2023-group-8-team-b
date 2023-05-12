using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Review.Domain;
using TouristAgency.Tours;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.ReviewFeatures.Domain;
using TouristAgency.Users.SuperGuestFeature;

namespace TouristAgency.Users.ReviewFeatures
{
    public class GuestReviewDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Guest _loggedInGuest;
        private Window _window;

        private string _username;
        private ObservableCollection<GuestReviewAccommodation> _reviews;

        private ReservationService _reservationService;
        private GuestReviewService _guestReviewService;

        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }

        public GuestReviewDisplayViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = window;
            Reviews = new ObservableCollection<GuestReviewAccommodation>();
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            DisplayUser();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _guestReviewService = new GuestReviewService();
        }

        private void InstantiateCollections()
        {
            List<Reservation> reservations = _reservationService.GetOwnerReviewed(_loggedInGuest.ID);
            List<GuestReview> guestReviews = _guestReviewService.GetValidByReservationId(reservations);

            foreach (GuestReview guestReview in guestReviews)
            {
                Reviews.Add(new GuestReviewAccommodation(guestReview.Reservation.Accommodation, guestReview));
            }

        }

        private void InstantiateCommands()
        {
            AccommodationDisplayCmd = new DelegateCommand(param => OpenAccommodationDisplayCmdExecute(),
                param => CanOpenAccommodationDisplayCmdExecute());
            PostponementRequestDisplayCmd = new DelegateCommand(param => OpenPostponementRequestDisplayCmdExecute(),
                param => CanOpenPostponementRequestDisplayCmdExecute());
            OwnerReviewCreationCmd = new DelegateCommand(param => OpenOwnerReviewCreationCmdExecute(),
                param => CanOpenOwnerReviewCreationCmdExecute());
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            SuperGuestDisplayCmd = new DelegateCommand(param => OpenSuperGuestDisplayCmdExecute(), param => CanOpenSuperGuestDisplayCmdExecute());
            HomeCmd = new DelegateCommand(param => OpenHomeCmdExecute(), param => CanOpenHomeCmdExecute());
            GuestReviewDisplayCmd = new DelegateCommand(param => OpenGuestReviewDisplayCmdExecute(), param => CanOpenGuestReviewDisplayCmdExecute());
        }

        private void DisplayUser()
        {
            Username = "Username: " + _loggedInGuest.Username;

        }

        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public ObservableCollection<GuestReviewAccommodation> Reviews
        {
            get => _reviews;
            set
            {
                if (value != _reviews)
                {
                    _reviews = value;
                    OnPropertyChanged("Reviews");
                }
            }
        }

        public bool CanOpenAccommodationDisplayCmdExecute()
        {
            return true;
        }

        public void OpenAccommodationDisplayCmdExecute()
        {
            _app.CurrentVM = new ReservationCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenPostponementRequestDisplayCmdExecute()
        {
            return true;
        }

        public void OpenPostponementRequestDisplayCmdExecute()
        {
            _app.CurrentVM = new PostponementRequestCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenOwnerReviewCreationCmdExecute()
        {
            return true;
        }

        public void OpenOwnerReviewCreationCmdExecute()
        {
            _app.CurrentVM = new OwnerReviewCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenSuperGuestDisplayCmdExecute()
        {
            return true;
        }

        public void OpenSuperGuestDisplayCmdExecute()
        {
            _app.CurrentVM = new SuperGuestDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenGuestReviewDisplayCmdExecute()
        {
            return true;
        }

        public void OpenGuestReviewDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestReviewDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenHomeCmdExecute()
        {
            return true;
        }

        public void OpenHomeCmdExecute()
        {
            _app.CurrentVM = new GuestHomeViewModel(_loggedInGuest, _window);
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }
    }
}
