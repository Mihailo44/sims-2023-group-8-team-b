using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.ViewModel;

namespace TouristAgency.ViewModel
{
    public class NotificationDisplayViewModel : ViewModelBase
    {
        private ObservableCollection<Voucher> _vouchers;
        private Tourist _tourist;
        private App _app;

        public NotificationDisplayViewModel(Tourist tourist, Window window) 
        {
            _app = (App)Application.Current;
            _tourist = tourist;

            Vouchers = new ObservableCollection<Voucher>(_app.TouristService.GetValidVouchers(tourist));
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
