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
using TouristAgency.Service;
using TouristAgency.ViewModel;

namespace TouristAgency.ViewModel
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
