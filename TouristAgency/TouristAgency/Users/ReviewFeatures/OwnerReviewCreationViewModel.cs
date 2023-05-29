using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.PostponementFeatures;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.RenovationFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.Users.ForumFeatures.DisplayFeature;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.ReviewFeatures.Domain;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.Util;

namespace TouristAgency.Users.ReviewFeatures
{
    public class OwnerReviewCreationViewModel : HelpMenuViewModelBase, ICreate
    {
        private App _app;
        private Guest _loggedInGuest;
        private readonly Window _window;

        private ObservableCollection<Reservation> _unreviewedReservations;

        private OwnerReview _newOwnerReview;
        private RenovationRecommendation _newRecommendation;
        private string _username;
        private Reservation _selectedReservation;

        private ReservationService _reservationService;
        private OwnerReviewService _ownerReviewService;
        private RenovationRecommendationService _renovationRecommendationService;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand CreateRecommendationCmd { get; set; }
        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand AnywhereAnytimeCreationCmd { get; set; }
        public DelegateCommand ForumDisplayCmd { get; set; }
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
            InstantiateHelpMenuCommands();
            DisplayUser();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _ownerReviewService = new OwnerReviewService();
            _renovationRecommendationService = new RenovationRecommendationService();
        }

        private void InstantiateCollections()
        {
            NewOwnerReview = new OwnerReview();
            NewRecommendation = new RenovationRecommendation();
            UnreviewedReservations = new ObservableCollection<Reservation>(_reservationService.GetUnreviewedByGuestId(_loggedInGuest.ID));
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
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
            AnywhereAnytimeCreationCmd = new DelegateCommand(param => OpenAnywhereAnytimeCreationCmdExecute(), param => CanOpenAnywhereAnytimeCreationCmdExecute());
            ForumDisplayCmd = new DelegateCommand(param => OpenForumDisplayCmdExecute(), param => CanOpenForumDisplayCmdExecute());
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

        public RenovationRecommendation NewRecommendation
        {
            get => _newRecommendation;
            set
            {
                if (value != _newRecommendation)
                {
                    _newRecommendation = value;
                    OnPropertyChanged("NewRecommendation");
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
            get => _selectedReservation;
            set
            {
                if (value != _selectedReservation)
                {
                    _selectedReservation = value;
                    OnPropertyChanged("SelectedReservation");
                }
            }
        }

        public string PhotoLinks
        {
            get;
            set;
        }


        public bool CanCreateCmdExecute()
        {
            return true;
        }

        public void CreateCmdExecute()
        {
            if (SelectedReservation != null)
            {
                CreateReview();
                CreateRecommendation();
            }
        }

        public void CreateReview()
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
                MessageBox.Show("You submitted successfully");
            }
            else
            {
                MessageBox.Show("You can't submit");
            }
        }

        public void CreateRecommendation()
        {
            if(NewRecommendation.UrgencyLevel != 0)
            {
                NewRecommendation.ReservationId = NewOwnerReview.ReservationId;
                NewRecommendation.Reservation = NewOwnerReview.Reservation;
                NewOwnerReview = new OwnerReview();
                if (!_renovationRecommendationService.IsAlreadySubmitted(NewRecommendation))
                {
                    NewRecommendation = _renovationRecommendationService.RenovationRecommendationRepository.Create(NewRecommendation);
                }
                
                NewRecommendation = new RenovationRecommendation();
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

        public bool CanOpenAnywhereAnytimeCreationCmdExecute()
        {
            return true;
        }

        public void OpenAnywhereAnytimeCreationCmdExecute()
        {
            _app.CurrentVM = new AnywhereAnytimeCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenForumDisplayCmdExecute()
        {
            return true;
        }

        public void OpenForumDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestForumDisplayViewModel(_loggedInGuest, _window);
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
