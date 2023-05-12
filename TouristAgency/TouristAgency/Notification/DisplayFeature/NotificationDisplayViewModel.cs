using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Users;
using TouristAgency.View.Display;

namespace TouristAgency.Vouchers
{
    public class NotificationDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Tourist _loggedInTourist;

        private ObservableCollection<Voucher> _vouchers;
        private ObservableCollection<TouristNotification> _notifications;
        private TouristNotification _selectedNotification;

        private TouristService _touristService;
        private TouristNotificationService _touristNotificationService;
        private TourTouristCheckpointService _ttcService;
        private CheckpointService _checkpointService;

        public DelegateCommand NotifyCmd { get; set; }

        public NotificationDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _touristService = new TouristService();
            _touristNotificationService = new TouristNotificationService();
            _ttcService = new TourTouristCheckpointService();
            _checkpointService = new CheckpointService();
        }

        private void InstantiateCollections()
        {
            Vouchers = new ObservableCollection<Voucher>(_touristService.GetValidVouchers(_loggedInTourist));
            Notifications = new ObservableCollection<TouristNotification>(_touristNotificationService.GetByTouristID(_loggedInTourist.ID));
        }

        private void InstantiateCommands()
        {
            NotifyCmd = new DelegateCommand(param => NotifyExecute(), param => CanNotifyExecute());
        }

        public ObservableCollection<Voucher> Vouchers
        {
            get => _vouchers;
            set
            {
                if (value != _vouchers)
                {
                    _vouchers = value;
                    OnPropertyChanged("Vouchers");
                }
            }
        }

        public ObservableCollection<TouristNotification> Notifications
        {
            get => _notifications;
            set
            {
                if (value != _notifications)
                {
                    _notifications = value;
                    OnPropertyChanged("Notifications");
                }
            }
        }

        public TouristNotification SelectedNotification
        {
            get => _selectedNotification;
            set
            {
                if (value != _selectedNotification) 
                {
                    _selectedNotification = value;
                    OnPropertyChanged("SelectedNotification");
                }
            }
        }

        public bool CanNotifyExecute()
        {
            return true;
        }

        public void NotifyExecute()
        {
            if (SelectedNotification != null)
            {
                switch (SelectedNotification.Type)
                {
                    case Util.TouristNotificationType.ATTENDANCE: AttendanceNotify(); break;
                    case Util.TouristNotificationType.SUGGESTED_TOUR: SuggestedNotify(); break;
                    case Util.TouristNotificationType.TOUR_REQUEST_ACCEPTED: NewNotify(); break;
                }
            }
        }

        private void AttendanceNotify()
        {
            var ttc = _ttcService.GetPendingInvitationsByCheckpoint(SelectedNotification.CheckpointID);
            MessageBoxResult result = MessageBox.Show("The guide has added you as present at the tour. Are you at: " + _checkpointService.CheckpointRepository.GetById(ttc.TourCheckpoint.CheckpointID).AttractionName + "?", "Question", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _ttcService.AcceptInvitation(_loggedInTourist.ID, ttc.TourCheckpoint.CheckpointID);
            }
        }

        public void SuggestedNotify()
        {
            TourDisplay x = new TourDisplay(_loggedInTourist, SelectedNotification.Tour);
            x.Show();
        }

        public void NewNotify()
        {
            TourDisplay x = new TourDisplay(_loggedInTourist, SelectedNotification.Tour);
            x.Show();
        }
    }
}
