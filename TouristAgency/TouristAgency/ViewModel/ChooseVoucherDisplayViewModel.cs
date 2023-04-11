using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class ChooseVoucherDisplayViewModel : ViewModelBase, ICloseable
    { 
        private ObservableCollection<Voucher> _vouchers;
        private Voucher _voucher;
        private Tourist _tourist;
        private int _tourID;
        private Window _window;
        private App _app;

        public DelegateCommand UseVoucherCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public ChooseVoucherDisplayViewModel(Tourist tourist, int tourID, Window window)
        {
            _app = (App)Application.Current;
            _tourist = tourist;
            _tourID = tourID;
            _window = window;

            Vouchers = new ObservableCollection<Voucher>(_app.TouristService.GetValidVouchers(tourist));
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
                _app.VoucherService.UseVoucher(voucher, _tourID);
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
