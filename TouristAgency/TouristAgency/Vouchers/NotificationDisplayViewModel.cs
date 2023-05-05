using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Users;

namespace TouristAgency.Vouchers
{
    public class NotificationDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Tourist _loggedInTourist;

        private ObservableCollection<Voucher> _vouchers;
        private ObservableCollection<TouristNotification> _notifications;  

        private TouristService _touristService;
        private TouristNotificationService _touristNotificationService;


        public NotificationDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            InstantiateServices();
            InstantiateCollections();
        }

        private void InstantiateServices()
        {
            _touristService = new TouristService();
            _touristNotificationService = new TouristNotificationService();
        }

        private void InstantiateCollections()
        {
            Vouchers = new ObservableCollection<Voucher>(_touristService.GetValidVouchers(_loggedInTourist));
            Notifications = new ObservableCollection<TouristNotification>(_touristNotificationService.GetByTouristID(_loggedInTourist.ID));
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
    }
}
