using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.PostponementFeatures;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.ReviewFeatures.Domain;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.Util;

namespace TouristAgency.Users.ReviewFeatures
{
    public class OwnerReviewCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Guest _loggedInGuest;
        private readonly Window _window;

        private ObservableCollection<Reservation> _unreviewedReservations;

        private OwnerReview _newOwnerReview;
        private string _username;

        private ReservationService _reservationService;
        private OwnerReviewService _ownerReviewService;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }

        public OwnerReviewCreationViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = window;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            DisplayUser();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _ownerReviewService = new OwnerReviewService();
        }

        private void InstantiateCollections()
        {
            NewOwnerReview = new OwnerReview();

            UnreviewedReservations = new ObservableCollection<Reservation>(_reservationService.GetUnreviewedByGuestId(_loggedInGuest.ID));
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
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

        public ObservableCollection<Reservation> UnreviewedReservations
        {
            get => _unreviewedReservations;
            set
            {
                if (value != _unreviewedReservations)
                {
                    _unreviewedReservations = value;
                    OnPropertyChanged("UnreviewedReservations");
                }
            }
        }

        public OwnerReview NewOwnerReview
        {
            get => _newOwnerReview;
            set
            {
                if (value != _newOwnerReview)
                {
                    _newOwnerReview = value;
                    OnPropertyChanged("NewOwnerReview");
                }
            }
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

        public Reservation SelectedReservation
        {
            get;
            set;
        }

        public string PhotoLinks
        {
            get;
            set;
        }

        public bool CanCreateExecute()
        {
            return true;
        }

        public void CreateExecute()
        {
            if (SelectedReservation != null)
            {
                if (SelectedReservation.OStatus == ReviewStatus.UNREVIEWED)
                {
                    NewOwnerReview.ReviewDate = DateTime.Now;
                    NewOwnerReview.ReservationId = SelectedReservation.Id;
                    NewOwnerReview.Reservation = SelectedReservation;
                    NewOwnerReview.Reservation.OStatus = ReviewStatus.REVIEWED;

                    _reservationService.ReservationRepository.Update(NewOwnerReview.Reservation, NewOwnerReview.ReservationId);

                    AddPhotos();
                    _ownerReviewService.OwnerReviewRepository.Create(NewOwnerReview);
                    UnreviewedReservations.Remove(SelectedReservation);
                    MessageBox.Show("Owner review is submitted successfully");
                }
                else
                {
                    MessageBox.Show("Owner is already reviewed");
                }
            }
        }

        public void AddPhotos()
        {
            int ownerReviewID = _ownerReviewService.OwnerReviewRepository.GenerateId() - 1;
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'O', ownerReviewID);
                    NewOwnerReview.Photos.Add(photo);
                    _app.PhotoRepository.Create(photo);
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
