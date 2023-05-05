using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;

namespace TouristAgency.Vouchers
{
    public class ChooseVoucherDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Voucher _voucher;

        private ObservableCollection<Voucher> _vouchers;

        private TouristService _touristService;
        private VoucherService _voucherService;

        private int _tourID;
        private Window _window;

        public DelegateCommand UseVoucherCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }

        public ChooseVoucherDisplayViewModel(Tourist tourist, int tourID, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _tourID = tourID;
            _window = window;
        }

        private void InstantiateServices()
        {
            _touristService = new TouristService();
            _voucherService = new VoucherService();
        }

        private void InstantiateCollections()
        {
            Vouchers = new ObservableCollection<Voucher>(_touristService.GetValidVouchers(_loggedInTourist));
        }

        private void InstantiateCommands()
        {
            UseVoucherCmd = new DelegateCommand(param => UseVoucherExecute(), param => CanUseVoucherExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
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

        public Voucher SelectedVoucher
        {
            get => _voucher;
            set
            {
                if (value != _voucher)
                {
                    _voucher = value;
                    OnPropertyChanged("SelectedVouchers");
                }
            }
        }

        public bool CanUseVoucherExecute()
        {
            return true;
        }

        public void UseVoucherExecute()
        {
            MessageBoxResult result = MessageBox.Show("Would you like to use the selected voucher for tour?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Voucher voucher = SelectedVoucher;
                _voucherService.UseVoucher(voucher, _tourID);
                Vouchers.Remove(voucher);
                MessageBox.Show("Successfully made a reservation.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }
    }
}
