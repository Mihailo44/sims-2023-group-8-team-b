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

        private TouristService _touristService;


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
        }

        private void InstantiateCollections()
        {
            Vouchers = new ObservableCollection<Voucher>(_touristService.GetValidVouchers(_loggedInTourist));
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
    }
}
