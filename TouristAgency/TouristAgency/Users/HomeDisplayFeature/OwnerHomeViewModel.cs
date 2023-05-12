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
using TouristAgency.Review.GuestReviewFeature;
using TouristAgency.Accommodations.NavigationWindow;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.PostponementFeatures;
using TouristAgency.Users.ReviewFeatures.Domain;
using TouristAgency.Accommodations.PostponementFeatures.ManagingFeature;

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
        private string _accountContainerVisibility;
        private string _notificationContainerVisibility;
        private List<string> _notifications;
        private string _btnNewVisibility;
        private string _btnClearNotificationVisibility;
        private bool _isChecked;
        private Dictionary<int, string> _dataGridVisibility = new Dictionary<int, string>()
        {
            {0, "Visible"},
            {1, "Collapsed"},
            {2, "Collapsed"},
            {3, "Collapsed"},
            {4,"Collapsed" }
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

        public List<string> Notifications
        {
            get => _notifications;
            set
            {
                _notifications = value;
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
                OnPropertyChanged(nameof(DataGridVisibility)); 
            }
        }

        public string Status { get; set; }

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }

        public ObservableCollection<PostponementRequest> PostponementRequests { get; set; }
        public PostponementRequest SelectedRequest { get; set; }

        public ObservableCollection<OwnerReview> OwnerReviews { get; set; }

        public Owner LoggedUser { get; set; }

        private Window _window;
        private App app = (App)App.Current;

        public DelegateCommand NewAccommodationCmd { get; set; }
        public DelegateCommand NewReviewCmd { get; set; }
        public DelegateCommand PostponeCmd { get; set; }
        public DelegateCommand PostponeCommentCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand ShowDataGridCmd { get; set; }
        public DelegateCommand ImportantCmd { get; set; }
        public DelegateCommand ShowAccCmd { get; set; }
        public DelegateCommand ShowNotificationsCmd { get; set; }
        public DelegateCommand ShowAccommodationMain { get; set; }
        public DelegateCommand ClearNotificationsCmd { get; set; }

        public OwnerHomeViewModel()
        {
            LoggedUser = app.LoggedUser;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "OwnerStart");

            InstantiateServices();
            SubscribeObservers();

            SetUserStatus();
            Notifications = new();
            ReviewNotification();
            AccountContainerVisibility = "Collapsed";
            NotificationContainerVisibility = "Collapsed";
            BtnNewVisibility = "Collapsed";
            BtnClearNotificationVisibility = "Collapsed";

            InstantiateCollections();
            FillCollections();

            _reservationService.ExpiredReservationsCheck(LoggedUser.ID);
            _renovationService.SetRenovationProgress(_accommodationService);
            _renovationService.CheckAccommodationRenovationStatus(_accommodationService);

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
        }

        private void SubscribeObservers()
        {
            _reservationService.ReservationRepository.Subscribe(this);
            _accommodationService.AccommodationRepository.Subscribe(this);
            _ownerReviewService.OwnerReviewRepository.Subscribe(this);
            _postponementRequestService.PostponementRequestRepository.Subscribe(this);
        }

        private void InstantiateCollections()
        {
            Accommodations = new ObservableCollection<Accommodation>();
            Reservations = new ObservableCollection<Reservation>();
            PostponementRequests = new ObservableCollection<PostponementRequest>();
            OwnerReviews = new ObservableCollection<OwnerReview>();
        }

        private void FillCollections()
        {
            LoadAccommodations(LoggedUser.ID);
            LoadReservations(LoggedUser.ID);
            LoadPostponementRequests(LoggedUser.ID);
            LoadOwnerReviews(LoggedUser.ID);
        }

        private void InstantiateCommands()
        {
            NewAccommodationCmd = new DelegateCommand(param => OpenAccommodationCreationExecute(), param => CanOpenAccommodationCreationExecute());
            NewReviewCmd = new DelegateCommand(param => OpenGuestReviewCreationForm(), param => CanOpenGuestReviewCreationForm());
            PostponeCmd = new DelegateCommand(param => OpenPostponeReservationExecute(), param => CanOpenPostponeReservationExecute());
            //PostponeCommentCmd = new DelegateCommand(param => PostponeReservationExecute(), param => CanOpenPostponeCommentExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(), param => CanCloseWindowExecute());
            ShowDataGridCmd = new DelegateCommand(ShowDataGridExecute, CanShowDataGridExecute);
            ImportantCmd = new DelegateCommand(param => ImportantCmdExecute(), param => CanImportantCmdExecute());
            ShowAccCmd = new DelegateCommand(param => ShowAccountCmdExecute(), param => CanShowAccountCmdExecute());
            ShowAccommodationMain = new DelegateCommand(param => ShowAccommodationMainExecute(), param=> CanShowAccommodationMainExecute());
            ShowNotificationsCmd = new DelegateCommand(param => ShowNotificationsCmdExecute(),param => CanShowNotificationsCmdExecute());
            ClearNotificationsCmd = new DelegateCommand(param => ClearNotificationsCmdExecute(), param => CanClearNotificationsCmdExecute());   
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
            List<PostponementRequest> postponementRequests = _postponementRequestService.GetByOwnerId(ownerId);
            foreach (var postponementRequest in postponementRequests)
            {
                PostponementRequests.Add(postponementRequest);
            }
        }

        public void LoadOwnerReviews(int ownerId)
        {
            OwnerReviews.Clear();
            List<OwnerReview> ownerReviews = _ownerReviewService.GetReviewedReservationsByOwnerId(ownerId);
            foreach (var ownerReview in ownerReviews)
            {
                OwnerReviews.Add(ownerReview);
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
            LoggedUser.SuperOwner = _ownerService.IsSuperOwner(_ownerReviewService.GetByOwnerId(LoggedUser.ID), out average);
            LoggedUser.Average = average;
            if (LoggedUser.SuperOwner)
                Status = $"SUPER OWNER ({LoggedUser.Average:F2})";
            else
                Status = "";

           _ownerService.OwnerRepository.Update(LoggedUser, LoggedUser.ID);
        }

        private void ReviewNotification()
        {
            int changes;
            List<string> notification = _reservationService.ReviewNotification(app.LoggedUser.ID, out changes);
            if (changes > 0)
            {
                Notifications = notification;
                BtnClearNotificationVisibility = "Visible";
            }
        }

        public bool CanOpenAccommodationCreationExecute()
        {
            return true;
        }

        public void OpenAccommodationCreationExecute()
        {
            app.CurrentVM = new AccommodationCreationViewModel();
        }

        public bool CanOpenGuestReviewCreationForm()
        {
            if (SelectedReservation != null)
            {
                DateTime today = DateTime.UtcNow.Date;
                double dateDiff = (today - SelectedReservation.End).TotalDays;

                if (SelectedReservation.Status == ReviewStatus.UNREVIEWED && dateDiff > 0.0 && dateDiff < 5.0)
                {
                    return true;
                }
                else
                {
                    if (dateDiff > 5.0)
                    {
                        MessageBox.Show("Guest review time window expired", "Guest Review Dialogue",MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if(dateDiff < 0.0)
                    {
                        MessageBox.Show("Selected reservation is in progress", "Guest Review Dialogue",MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (SelectedReservation.Status == ReviewStatus.REVIEWED)
                    {
                        MessageBox.Show("Selected guest has already been reviewed", "Guest Review Dialogue",MessageBoxButton.OK,MessageBoxImage.Information);
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
                app.CurrentVM = new GuestReviewViewModel(SelectedReservation);
            }
        }

        public bool CanOpenPostponeReservationExecute()
        {
            return SelectedRequest != null;
        }

        public void OpenPostponeReservationExecute()
        {
            PostponementRequestDialogue x = new PostponementRequestDialogue(SelectedRequest);
            x.Show();
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

            return index >= 0 && index <=4;
        }

        public void ShowDataGridExecute(object parameter)
        {
            int index = int.Parse(parameter.ToString());

            if (index == 1)
                BtnNewVisibility = "Visible";
            else
                BtnNewVisibility = "Collapsed";

            for (int i = 0; i <= 4; i++)
            {
                if(i == index)
                {
                    DataGridVisibility[i] = "Visible";
                }
                else
                {
                    DataGridVisibility[i] = "Collapsed";
                }

                OnPropertyChanged("DataGridVisibility");
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
            if(AccountContainerVisibility == "Visible")
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
            return true ;
        }

        public void ClearNotificationsCmdExecute()
        {
            Notifications.Clear();
            NotificationContainerVisibility = "Collapsed";
        }

        public bool CanShowAccommodationMainExecute()
        {
            return SelectedAccommodation != null;
        }

        public void ShowAccommodationMainExecute()
        {
            AccommodationMain x = new(SelectedAccommodation);
            x.Show();
        }
    }
}
