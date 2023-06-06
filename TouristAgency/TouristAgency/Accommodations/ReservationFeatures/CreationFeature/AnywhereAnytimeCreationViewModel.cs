using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.ReportFeature;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.Users.ForumFeatures.DisplayFeature;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.ReviewFeatures;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.Users.SuperGuestFeature.Domain;

namespace TouristAgency.Accommodations.ReservationFeatures.CreationFeature
{
    public class AnywhereAnytimeCreationViewModel : HelpMenuViewModelBase, ICloseable, ICreate
    {
        private App _app;
        private Guest _loggedInGuest;
        private Window _window;
        private string _username;
        private string _welcomeUsername;

        private int _numOfPeople;
        private int _numOfDays;
        private DateTime? _start;
        private DateTime? _end;
        private ObservableCollection<Accommodation> _accommodations;
        private ObservableCollection<Reservation> _reservations;

        private ReservationService _reservationService;
        private AccommodationService _accommodationService;
        private SuperGuestTitleService _superGuestTitleService;

        public DelegateCommand SearchCmd { get; set; }
        public DelegateCommand SearchDatesCmd { get; set; }
        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand AnywhereAnytimeCreationCmd { get; set; }
        public DelegateCommand ForumDisplayCmd { get; set; }
        public DelegateCommand GuestReportDisplayCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }


        public AnywhereAnytimeCreationViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuestHome");

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateHelpMenuCommands();
            ShowUser();
            WelcomeUser();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _accommodationService = new AccommodationService();
            _superGuestTitleService = new SuperGuestTitleService();
        }

        private void InstantiateCollections()
        {
            Start = null;
            End = null;

            Accommodations = new ObservableCollection<Accommodation>();
            Reservations = new ObservableCollection<Reservation>();
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
            AnywhereAnytimeCreationCmd = new DelegateCommand(param => OpenAnywhereAnytimeCreationCmdExecute(), param => CanOpenAnywhereAnytimeCreationCmdExecute());
            ForumDisplayCmd = new DelegateCommand(param => OpenForumDisplayCmdExecute(), param => CanOpenForumDisplayCmdExecute());
            SearchCmd = new DelegateCommand(param =>  SearchCmdExecute(), param => CanSearchCmdExecute());
            SearchDatesCmd = new DelegateCommand(param => SearchDatesCmdExecute(), param => CanSearchDatesCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
            GuestReportDisplayCmd = new DelegateCommand(param => OpenGuestReportDisplayCmdExecute(), param => CanOpenGuestReportDisplayCmdExecute());
        }

        private void ShowUser()
        {
            Username = "Username: " + _loggedInGuest.Username;
        }

        private void WelcomeUser()
        {
            WelcomeUsername = "Welcome " + _loggedInGuest.Username + "!!!";
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

        public string WelcomeUsername
        {
            get => _welcomeUsername;
            set
            {
                if (value != _welcomeUsername)
                {
                    _welcomeUsername = value;
                    OnPropertyChanged("WelcomeUsername");
                }
            }
        }

        public ObservableCollection<Accommodation> Accommodations
        {
            get => _accommodations;
            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged("Accommodations");
                }
            }
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

        public int NumOfDays
        {
            get => _numOfDays;
            set
            {
                if (_numOfDays != value)
                {
                    _numOfDays = value;
                    OnPropertyChanged("NumOfDays");
                }
            }
        }

        public DateTime? Start
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

        public DateTime? End
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

        public int NumOfPeople
        {
            get => _numOfPeople;
            set
            {
                if (_numOfPeople != value)
                {
                    _numOfPeople = value;
                    OnPropertyChanged("NumOfPeople");
                }
            }
        }

        public Accommodation SelectedAccommodation
        {
            get;
            set;
        }

        public Reservation SelectedReservation
        {
            get;
            set;
        }

        public bool CanCreateCmdExecute()
        {
            return true;
        }
        private void CreateCmdExecute()
        {
            Reservation newReservation = SelectedReservation;
            Accommodation selectedAccommodation = SelectedAccommodation;

            if (selectedAccommodation.MaxGuestNum >= NumOfPeople && selectedAccommodation.MinNumOfDays <= NumOfDays && newReservation != null)
            {
                newReservation.Accommodation = selectedAccommodation;
                newReservation.Accommodation.Id = selectedAccommodation.Id;
                newReservation.Guest = _loggedInGuest;
                newReservation.Guest.ID = _loggedInGuest.ID;
                _reservationService.Create(newReservation);
                Reservations.Remove(newReservation);
                _superGuestTitleService.UsePoint(_loggedInGuest.ID);
                MessageBox.Show("Successfully reserved");
            }
        }

        public bool CanSearchCmdExecute()
        {
            return true;
        }

        public void SearchCmdExecute()
        {
            if(Start == null || End == null)
            {
                Accommodations = new ObservableCollection<Accommodation>(_accommodationService.AccommodationRepository.GetAll().FindAll(a => a.MinNumOfDays <= NumOfDays && a.MaxGuestNum >= NumOfPeople));
            }
            else if(Start != null && End != null)
            {
                int numOfReservations = ((DateTime)End - (DateTime)Start).Days - NumOfDays + 2;
                Accommodations = new ObservableCollection<Accommodation>(_reservationService.GetAllFreeAccommodation((DateTime)Start, (DateTime)End, _accommodationService.AccommodationRepository.GetAll(), NumOfDays, NumOfPeople, _loggedInGuest, numOfReservations));
            }
        }

        public bool CanSearchDatesCmdExecute()
        {
            return true;
        }

        public void SearchDatesCmdExecute()
        {
            if(Start == null || End == null)
            {
                Reservations.Clear();

                int numOfReservations = (DateTime.Now.AddYears(1) - DateTime.Now).Days - NumOfDays + 2;
                Reservations = _reservationService.GenerateRandomPotentionalReservations(DateTime.Now, NumOfDays, numOfReservations, SelectedAccommodation, _loggedInGuest);
            }
            else if(Start != null && End != null)
            {
                Reservations.Clear();

                int numOfReservations = ((DateTime)End - (DateTime)Start).Days - NumOfDays + 2;
                Reservations = _reservationService.GeneratePotentionalReservations((DateTime)Start, NumOfDays, numOfReservations, SelectedAccommodation, _loggedInGuest);
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

        public bool CanOpenGuestReportDisplayCmdExecute()
        {
            return true;
        }

        public void OpenGuestReportDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestReportDisplayViewModel(_loggedInGuest, _window);
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
