using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.Vouchers;

namespace TouristAgency.Tours.VoucherFeatures.DisplayFeature
{
    public class VoucherDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Window _window;

        private ObservableCollection<Voucher> _vouchers;

        private TouristService _touristService;

        public DelegateCommand CloseCmd { get; set; }

        public VoucherDisplayViewModel(Tourist tourist, Window window) 
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _touristService = new TouristService();
        }

        private void InstantiateCollections()
        {
            Vouchers = new ObservableCollection<Voucher>(_touristService.GetValidVouchers(_loggedInTourist));
        }

        private void InstantiateCommands()
        {
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
