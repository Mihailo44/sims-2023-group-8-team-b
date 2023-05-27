using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Util;
using TouristAgency.Accommodations.CreationFeature;
using TouristAgency.Accommodations.NavigationWindow;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Users.ReviewFeatures.Domain;
using TouristAgency.Accommodations.PostponementFeatures.ManagingFeature;
using TouristAgency.Accommodations.ForumFeatures.Domain;
using GalaSoft.MvvmLight.Messaging;
using TouristAgency.Accommodations.ForumFeatures.DisplayFeature;
using TouristAgency.Notifications.Domain;

namespace TouristAgency.Users.HomeDisplayFeature
{
    public class OwnerHomeViewModel : ViewModelBase, IObserver, ICloseable
    {
        private ReservationService _reservationService;
        private AccommodationService _accommodationService;
        private OwnerReviewService _ownerReviewService;
        private OwnerService _ownerService;
        private PostponementRequestService _postponementRequestService;
        private RenovationService _renovationService;
        private GuestReviewNotificationService _guestReviewNotificationService;
        private PhotoRepository _photoRepository;
        private LocationService _locationService;
        private GuestReviewService _guestReviewService;
        private RenovationRecommendationService _recommendationService;
        private ForumService _forumService;

        private string _photoLinks;
        private string _searchInput;
        private string _accountContainerVisibility;
        private string _notificationContainerVisibility;
        private string _btnNewVisibility;
        private string _btnClearNotificationVisibility;
        private string _inputFormVisibility;
        private string _reviewFormVisibility;
        private string _notificationCountVisibility;
        private string _comment;
        private bool _isChecked;
        private Dictionary<int, string> _dataGridVisibility = new Dictionary<int, string>()
        {
            {0, "Visible"},
            {1, "Collapsed"},
            {2, "Collapsed"},
            {3, "Collapsed"},
            {4, "Collapsed"}
        };

        public string AccountContainerVisibility
        {
            get => _accountContainerVisibility;
            set
            {
                _accountContainerVisibility = value;
                OnPropertyChanged();
            }
        }

        public string NotificationContainerVisibility
        {
            get => _notificationContainerVisibility;
            set
            {
                _notificationContainerVisibility = value;
                OnPropertyChanged();
            }
        }

        public string BtnNewVisibility
        {
            get => _btnNewVisibility;
            set
            {
                _btnNewVisibility = value;
                OnPropertyChanged();
            }
        }

        public string BtnClearNotificationVisibility
        {
            get => _btnClearNotificationVisibility;
            set
            {
                if (_btnClearNotificationVisibility != value)
                {
                    _btnClearNotificationVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public string InputFormVisibility
        {
            get => _inputFormVisibility;
            set
            {
                if (_inputFormVisibility != value)
                {
                    _inputFormVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ReviewFormVisibility
        {
            get => _reviewFormVisibility;
            set
            {
                if (_reviewFormVisibility != value)
                {
                    _reviewFormVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NotificationCountVisibility
        {
            get => _notificationCountVisibility;
            set
            {
                if (_notificationCountVisibility != value)
                {
                    _notificationCountVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<int, string> DataGridVisibility
        {
            get => _dataGridVisibility;
            set
            {
                _dataGridVisibility = value;
                OnPropertyChanged();
            }
        }

        public string PhotoLinks
        {
            get => _photoLinks;
            set
            {
                if (_photoLinks != value)
                {
                    _photoLinks = value;
                }
            }
        }

        public string SearchInput
        {
            get => _searchInput;
            set
            {
                if (_searchInput != value)
                {
                    _searchInput = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                }
            }
        }

        private ViewModelBase _currentVm;

        public ViewModelBase CurrentVM
        {
            get => _currentVm;
            set
            {
                if (_currentVm != value)
                {
                    _currentVm = value;
                    OnPropertyChanged();
                }
            }
        }

        private void HandleSwitchViewModelMessage(SwitchViewModelMessage message)
        {
            CurrentVM = message.ViewModel;
        }

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }

        public ObservableCollection<PostponementRequest> PostponementRequests { get; set; }
        public PostponementRequest SelectedRequest { get; set; }

        public ObservableCollection<OwnerReview> OwnerReviews { get; set; }

        public ObservableCollection<Notification> Notifications { get; set; }
        public int NotificationCount { get; set; }

        public ObservableCollection<Forum> Forums { get; set; }
        public Forum SelectedForum { get; set; }

        public Owner LoggedUser { get; set; }
        public string Status { get; set; }

        public List<string> TypeComboValues { get; set; } = new();
        public List<int> ComboNumbers { get; set; } = new();

        public Accommodation NewAccommodation { get; set; }
        public Location NewLocation { get; set; }
        public GuestReview NewGuestReview { get; set; }

        private Window _window;
        private readonly App _app;

        public DelegateCommand CreateAccommodationCmd { get; set; }
        public DelegateCommand OpenNewReviewCmd { get; set; }
        public DelegateCommand PostponeCmd { get; set; }
        public DelegateCommand OpenAccommodationCreationCmd { get; set; }
        public DelegateCommand CloseAccommodationCreationCmd { get; set; }
        public DelegateCommand PostponeCommentCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand ShowDataGridCmd { get; set; }
        public DelegateCommand ImportantCmd { get; set; }
        public DelegateCommand ShowAccCmd { get; set; }
        public DelegateCommand ShowNotificationsCmd { get; set; }
        public DelegateCommand ShowAccommodationMain { get; set; }
        public DelegateCommand ClearNotificationsCmd { get; set; }
        public DelegateCommand SearchCmd { get; set; }
        public DelegateCommand CreateNewReviewCmd { get; set; }
        public DelegateCommand CloseReviewCmd { get; set; }
        public DelegateCommand OpenForumCmd { get; set; }

        public OwnerHomeViewModel()
        {
            _app = (App)App.Current;
            LoggedUser = _app.LoggedUser;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "OwnerStart");
            Messenger.Default.Register<SwitchViewModelMessage>(this, HandleSwitchViewModelMessage);
            InstantiateServices();
            SubscribeObservers();
            InstantiateCollections();

            SetUserStatus();
            Notifications = new();
            NewAccommodation = new();
            NewLocation = new();
            NewGuestReview = new();

            _guestReviewNotificationService.ManageNotifications(LoggedUser.ID, _reservationService);

            AccountContainerVisibility = "Collapsed";
            NotificationContainerVisibility = "Collapsed";
            BtnNewVisibility = "Collapsed";
            BtnClearNotificationVisibility = "Collapsed";
            InputFormVisibility = "Collapsed";
            ReviewFormVisibility = "Collapsed";

            _reservationService.ExpiredReservationsCheck(LoggedUser.ID);
            _renovationService.SetRenovationProgress(_accommodationService);
            _renovationService.CheckAccommodationRenovationStatus(_accommodationService);
            _accommodationService.SetHotLocationsStatus(_locationService, _accommodationService, _reservationService, _postponementRequestService, _recommendationService);

            FillCollections();

            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _reservationService = new();
            _accommodationService = new();
            _ownerReviewService = new();
            _postponementRequestService = new();
            _ownerService = new();
            _renovationService = new();
            _guestReviewNotificationService = new();
            _photoRepository = _app.PhotoRepository;
            _locationService = new();
            _guestReviewService = new();
            _recommendationService = new();
            _forumService = new();
        }

        private void SubscribeObservers()
        {
            _reservationService.ReservationRepository.Subscribe(this);
            _accommodationService.AccommodationRepository.Subscribe(this);
            _ownerReviewService.OwnerReviewRepository.Subscribe(this);
            _postponementRequestService.PostponementRequestRepository.Subscribe(this);
            _forumService.ForumRepository.Subscribe(this);
        }

        private void InstantiateCollections()
        {
            Accommodations = new ObservableCollection<Accommodation>();
            Reservations = new ObservableCollection<Reservation>();
            PostponementRequests = new ObservableCollection<PostponementRequest>();
            OwnerReviews = new ObservableCollection<OwnerReview>();
            Forums = new ObservableCollection<Forum>();
        }

        private void FillCollections()
        {
            LoadAccommodations(LoggedUser.ID);
            LoadReservations(LoggedUser.ID);
            LoadPostponementRequests(LoggedUser.ID);
            LoadOwnerReviews(LoggedUser.ID);
            LoadNotifications(LoggedUser.ID);
            LoadForums();
        }

        private void FillCombos()
        {
            ComboNumbers.Clear();

            for (int i = 1; i <= 5; i++)
            {
                ComboNumbers.Add(i);
            }

            ComboNumbers.Reverse();
        }

        private void InstantiateCommands()
        {
            CreateAccommodationCmd = new DelegateCommand(param => CreateAccommodationExecute(), param => CanCreateAccommodationExecute());
            CreateNewReviewCmd = new DelegateCommand(param => CreateNewReviewCmdExecute(), param => CanCreateNewReviewCmdExecute());
            OpenNewReviewCmd = new DelegateCommand(param => OpenGuestReviewCreationForm(), param => CanOpenGuestReviewFormExecute());
            CloseReviewCmd = new DelegateCommand(param => CloseReviewCmdExecute(), param => CanCloseReviewCmdExecute());
            PostponeCmd = new DelegateCommand(param => OpenPostponeReservationExecute(), param => CanOpenPostponeReservationExecute());
            OpenAccommodationCreationCmd = new DelegateCommand(param => OpenAccommodationCreationCmdExecute(), param => CanOpenAccommodationCreationCmdExecute());
            CloseAccommodationCreationCmd = new DelegateCommand(param => CloseAccommodationCreationCmdExecute(), param => CanCloseAccommodationCreationCmdExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(), param => CanCloseWindowExecute());
            ShowDataGridCmd = new DelegateCommand(ShowDataGridExecute, CanShowDataGridExecute);
            ImportantCmd = new DelegateCommand(param => ImportantCmdExecute(), param => CanImportantCmdExecute());
            ShowAccCmd = new DelegateCommand(param => ShowAccountCmdExecute(), param => CanShowAccountCmdExecute());
            ShowAccommodationMain = new DelegateCommand(param => ShowAccommodationMainExecute(), param => CanShowAccommodationMainExecute());
            ShowNotificationsCmd = new DelegateCommand(param => ShowNotificationsCmdExecute(), param => CanShowNotificationsCmdExecute());
            ClearNotificationsCmd = new DelegateCommand(param => ClearNotificationsCmdExecute(), param => CanClearNotificationsCmdExecute());
            SearchCmd = new DelegateCommand(param => SearchCmdExecute(), param => CanSearchCmdExecute());
            OpenForumCmd = new DelegateCommand(param => OpenForumCmdExecute(), param => CanOpenForumCmdExecute());
        }


        private void LoadAccommodations(int ownerId)
        {
            Accommodations.Clear();
            List<Accommodation> accommodations = _accommodationService.GetByOwnerId(ownerId);
            foreach (var accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }

        private void LoadReservations(int ownerId)
        {
            Reservations.Clear();
            List<Reservation> reservations = _reservationService.GetByOwnerId(ownerId);
            foreach (var reservation in reservations)
            {
                Reservations.Add(reservation);
            }
        }

        public void LoadPostponementRequests(int ownerId)
        {
            PostponementRequests.Clear();
            PostponementRequests.AddRange(_postponementRequestService.GetByOwnerId(ownerId));
        }

        public void LoadOwnerReviews(int ownerId)
        {
            OwnerReviews.Clear();
            OwnerReviews.AddRange(_ownerReviewService.GetReviewedReservationsByOwnerId(ownerId));
        }

        private void LoadNotifications(int ownerId)
        {
            List<GuestReviewNotification> notifications = _guestReviewNotificationService.GetByOwnerId(ownerId);
            if (notifications.Count() > 0)
            {
                Notifications.AddRange(notifications);
                BtnClearNotificationVisibility = "Visible";
                NotificationCountVisibility = "Visible";
                NotificationCount = notifications.Count();
            }
            else
            {
                NotificationCount = 0;
                NotificationCountVisibility = "Collapsed";
            }
        }

        private void LoadForums()
        {
            List<Forum> forums = _forumService.GetAll();
            Forums.AddRange(forums);
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
            double ownerScore;
            LoggedUser.SuperOwner = _ownerService.IsSuperOwner(_ownerReviewService.GetByOwnerId(LoggedUser.ID), out ownerScore);
            LoggedUser.Average = ownerScore;
            if (LoggedUser.SuperOwner)
                Status = $"SUPER OWNER ({LoggedUser.Average:F2})";
            else
                Status = "";

            _ownerService.OwnerRepository.Update(LoggedUser, LoggedUser.ID);
        }

        public bool CanOpenGuestReviewFormExecute()
        {
            if (SelectedReservation != null)
            {
                DateTime today = DateTime.UtcNow.Date;
                double dateDiff = (today - SelectedReservation.End).TotalDays;

                if (SelectedReservation.Status == ReviewStatus.UNREVIEWED && dateDiff > 0.0 && dateDiff <= 5.0)
                {
                    return true;
                }
                else
                {
                    if (dateDiff > 5.0)
                    {
                        MessageBox.Show("Guest review time window expired", "Guest Review Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (dateDiff < 0.0)
                    {
                        MessageBox.Show("Selected reservation is in progress", "Guest Review Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (SelectedReservation.Status == ReviewStatus.REVIEWED)
                    {
                        MessageBox.Show("Selected guest has already been reviewed", "Guest Review Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void OpenGuestReviewCreationForm()
        {
            if (SelectedReservation != null)
            {
                // _app.CurrentVM = new GuestReviewCreationViewModel(SelectedReservation);
                FillCombos();
                NewGuestReview.Reservation = SelectedReservation;
                NewGuestReview.ReservationId = SelectedReservation.Id;
                DataGridVisibility[0] = "Collapsed";
                OnPropertyChanged(nameof(DataGridVisibility));
                ReviewFormVisibility = "Visible";
            }
        }

        public bool CanCreateNewReviewCmdExecute()
        {
            /*if (!string.IsNullOrEmpty(Comment))
            {
                return true;
            }
            else
            {
                return false;
            }*/

            return true;
        }

        public void CreateNewReviewCmdExecute()
        {
            try
            {
                if (NewGuestReview.Reservation.Status == ReviewStatus.UNREVIEWED)
                {
                    NewGuestReview.Comment = Comment;
                    NewGuestReview.Reservation.Status = ReviewStatus.REVIEWED;
                    _guestReviewService.GuestReviewRepository.Create(NewGuestReview);
                    _reservationService.ReservationRepository.Update(NewGuestReview.Reservation, NewGuestReview.ReservationId);
                    MessageBox.Show("Guest review created successfully", "Guest Review Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("This guest has already been reviewed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ReviewFormVisibility = "Collapsed";
                DataGridVisibility[0] = "Visible";
                OnPropertyChanged(nameof(DataGridVisibility));
            }
        }

        public bool CanCloseReviewCmdExecute()
        {
            return true;
        }

        public void CloseReviewCmdExecute()
        {
            ReviewFormVisibility = "Collapsed";
            DataGridVisibility[0] = "Visible";
            OnPropertyChanged(nameof(DataGridVisibility));
        }

        public bool CanOpenPostponeReservationExecute()
        {
            return SelectedRequest != null;
        }

        public void OpenPostponeReservationExecute()
        {
            Messenger.Reset();
            PostponementRequestDialogue x = new PostponementRequestDialogue(SelectedRequest);
            x.ShowDialog();
        }

        public bool CanOpenPostponeCommentExecute()
        {
            return true;
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            _window.Close();
        }

        public bool CanShowDataGridExecute(object parameter)
        {
            if (parameter == null || !int.TryParse(parameter.ToString(), out int index))
            {
                return false;
            }

            return index >= 0 && index <= 4;
        }

        public void ShowDataGridExecute(object parameter)
        {
            Messenger.Default.Send(new SwitchViewModelMessage(null));
            int index = int.Parse(parameter.ToString());

            InputFormVisibility = "Collapsed";
            ReviewFormVisibility = "Collapsed";

            if (index == 1)
                BtnNewVisibility = "Visible";
            else
                BtnNewVisibility = "Collapsed";

            for (int i = 0; i < 5; i++)
            {
                if (i == index)
                {
                    DataGridVisibility[i] = "Visible";
                }
                else
                {
                    DataGridVisibility[i] = "Collapsed";
                }

                OnPropertyChanged(nameof(DataGridVisibility));
            }
        }

        public bool CanImportantCmdExecute()
        {
            return true;
        }

        public void ImportantCmdExecute()
        {
            //string paris = "https://youtu.be/gG_dA32oH44?t=22";
            string blood = "https://youtu.be/0-Tm65i96TY?t=15";
            ProcessStartInfo ps = new ProcessStartInfo
            {
                FileName = blood,
                UseShellExecute = true
            };
            Process.Start(ps);
        }

        public bool CanShowAccountCmdExecute()
        {
            return true;
        }

        public void ShowAccountCmdExecute()
        {
            if (AccountContainerVisibility == "Visible")
            {
                AccountContainerVisibility = "Collapsed";
            }
            else
            {
                AccountContainerVisibility = "Visible";
                NotificationContainerVisibility = "Collapsed";
            }
        }

        public bool CanShowNotificationsCmdExecute()
        {
            return true;
        }

        public void ShowNotificationsCmdExecute()
        {
            if (NotificationContainerVisibility == "Visible")
            {
                NotificationContainerVisibility = "Collapsed";
                if (Notifications.Count == 0)
                {
                    BtnClearNotificationVisibility = "Collapsed";
                }
            }
            else
            {
                NotificationContainerVisibility = "Visible";
                AccountContainerVisibility = "Collapsed";
                if (Notifications.Count > 0)
                    BtnClearNotificationVisibility = "Visible";
                else
                    BtnClearNotificationVisibility = "Collapsed";
            }
        }

        public bool CanClearNotificationsCmdExecute()
        {
            return true;
        }

        public void ClearNotificationsCmdExecute()
        {
            Notifications.Clear();
            NotificationCountVisibility = "Collapsed";
            NotificationCount = 0;
            //NotificationContainerVisibility = "Collapsed";
        }

        public bool CanShowAccommodationMainExecute()
        {
            return SelectedAccommodation != null;
        }

        public void ShowAccommodationMainExecute()
        {
            AccommodationMain x = new(SelectedAccommodation);
            x.ShowDialog();
        }

        public bool CanOpenAccommodationCreationCmdExecute()
        {
            return true;
        }

        public void OpenAccommodationCreationCmdExecute()
        {
            FillTypeCombo();
            InputFormVisibility = "Visible";
            DataGridVisibility[1] = "Collapsed";
            BtnNewVisibility = "Hidden";
            OnPropertyChanged(nameof(DataGridVisibility));
        }

        private void FillTypeCombo()
        {
            TypeComboValues.Clear();
            TypeComboValues.Add(TYPE.APARTMENT.ToString());
            TypeComboValues.Add(TYPE.HOTEL.ToString());
            TypeComboValues.Add(TYPE.HUT.ToString());
        }

        private void AddPhotos()
        {
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'A', NewAccommodation.Id);
                    NewAccommodation.Photos.Add(photo);
                    _photoRepository.Create(photo);
                }
            }
        }

        private void PrepareAccommodationForCreation()
        {
            NewAccommodation.OwnerId = LoggedUser.ID;
            NewAccommodation.Owner = LoggedUser;
            NewAccommodation.Location = _locationService.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim());
            NewAccommodation.LocationId = _locationService.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim()).Id;
        }

        public bool CanCreateAccommodationExecute()
        {
            return true;
        }

        public void CreateAccommodationExecute()
        {
            PrepareAccommodationForCreation();
            try
            {
                _accommodationService.AccommodationRepository.Create(NewAccommodation);
                AddPhotos();
                MessageBox.Show("Accommodation created successfully", "Accommodation Creation Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
                InputFormVisibility = "Collapsed";
                DataGridVisibility[1] = "Visible";
                BtnNewVisibility = "Visible";
                OnPropertyChanged(nameof(DataGridVisibility));
            }
            catch
            {
                MessageBox.Show("Something went wrong, please try again", "Accommodation Creation Dialogue", MessageBoxButton.OK, MessageBoxImage.Error);
                InputFormVisibility = "Collapsed";
                DataGridVisibility[1] = "Visible";
                BtnNewVisibility = "Visible";
                OnPropertyChanged(nameof(DataGridVisibility));
            }
        }

        public bool CanCloseAccommodationCreationCmdExecute()
        {
            return true;
        }

        public void CloseAccommodationCreationCmdExecute()
        {
            InputFormVisibility = "Collapsed";
            DataGridVisibility[1] = "Visible";
            BtnNewVisibility = "Visible";
            OnPropertyChanged(nameof(DataGridVisibility));
        }

        public bool CanSearchCmdExecute()
        {
            return true;
        }

        public void SearchCmdExecute()
        {
            ShowDataGridExecute(0);

            Reservations.Clear();
            Reservations.AddRange(_reservationService.SearchReservations(SearchInput));
        }

        public bool CanOpenForumCmdExecute()
        {
            return SelectedForum != null;
        }

        public void OpenForumCmdExecute()
        {
            DataGridVisibility[4] = "Collapsed";
            OnPropertyChanged(nameof(DataGridVisibility));
            Messenger.Default.Register<SwitchViewModelMessage>(this, HandleSwitchViewModelMessage);
            CurrentVM = new ForumDisplayViewModel(SelectedForum);
        }
    }
}
