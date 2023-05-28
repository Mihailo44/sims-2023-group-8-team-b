using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.Users.ReviewFeatures;

namespace TouristAgency.Accommodations.PostponementFeatures.CreationFeature
{
    public class PostponementRequestCreationViewModel : HelpMenuViewModelBase, ICreate
    {
        private App _app;
        private Guest _loggedInGuest;

        private ObservableCollection<Reservation> _reservations;
        private ObservableCollection<PostponementRequest> _requests;

        private DateTime _start;
        private DateTime _end;
        private string _username;
        private Window _window;

        private ReservationService _reservationService;
        private PostponementRequestService _postponementRequestService;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand CancelCmd { get; set; }
        public DelegateCommand NotificationCmd { get; set; }
        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand AnywhereAnytimeCreationCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }

        public PostponementRequestCreationViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = window;
            _username = "";

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateHelpMenuCommands();
            DisplayUser();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _postponementRequestService = new PostponementRequestService();
        }

        private void InstantiateCollections()
        {
            Start = DateTime.Now;
            End = DateTime.Now;

            Reservations = new ObservableCollection<Reservation>(_reservationService.GetByGuestId(_loggedInGuest.ID));
            Requests = new ObservableCollection<PostponementRequest>(_postponementRequestService.GetByGuestId(_loggedInGuest.ID));
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
            NotificationCmd = new DelegateCommand(param => NotificationExecute(), param => CanNotificationExecute());
            CancelCmd = new DelegateCommand(param => CancelExecute(), param => CanCancelExecute());
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
        }

        private void DisplayUser()
        {
            Username = "Username: " + _loggedInGuest.Username;

        }

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                if (value != _reservations)
                {
                    _reservations = value;
                    OnPropertyChanged("Reservations");
                }
            }
        }

        public ObservableCollection<PostponementRequest> Requests
        {
            get => _requests;
            set
            {
                if (value != _requests)
                {
                    _requests = value;
                    OnPropertyChanged("Requests");
                }
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                }
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                {
                    _end = value;
                    OnPropertyChanged("End");
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

        bool CanCreateExecute()
        {
            return true;
        }

        void CreateExecute()
        {
            DateTime today = DateTime.Now;
            if (Start >= today && End >= today && End >= Start)
            {
                if (SelectedReservation != null)
                {
                    PostponementRequest request = new PostponementRequest(SelectedReservation, Start, End);
                    _postponementRequestService.PostponementRequestRepository.Create(request);
                    Requests.Add(request);
                }
            }
            else
            {
                MessageBox.Show("You have entered invalid dates. Try again.");
            }

        }

        bool CanNotificationExecute()
        {
            return true;
        }

        void NotificationExecute()
        {
            MessageBox.Show(_postponementRequestService.ShowNotifications(_loggedInGuest.ID));
        }

        bool CanCancelExecute()
        {
            return true;
        }

        void CancelExecute()
        {
            if (SelectedReservation != null)
            {
                bool result;
                result = _reservationService.CancelReservation(SelectedReservation);
                if (result == true)
                {
                    Reservations.Remove(SelectedReservation);
                    MessageBox.Show("Your reservation was successfully canceled");
                }
                else
                {
                    MessageBox.Show(
                        "Reservation couldn't be canceled. You can only cancel 24 hours before the start of the reservation");
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
