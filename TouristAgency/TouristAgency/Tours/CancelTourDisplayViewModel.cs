using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Users;
using TouristAgency.Vouchers;

namespace TouristAgency.Tours
{
    public class CancelTourDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Guide _guide;

        private ObservableCollection<Tour> _availableTours;

        private TourService _tourService;
        private VoucherService _voucherService;
        public DelegateCommand CancelTourCmd { get; set; }

        public CancelTourDisplayViewModel(Guide guide)
        {
            _app = (App)Application.Current;
            _guide = guide;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
        }

        private void InstantiateCollections()
        {
            AvailableTours = new ObservableCollection<Tour>(_tourService.GetCancellabeTours());
        }

        private void InstantiateCommands()
        {
            CancelTourCmd = new DelegateCommand(param => CancelToursExecute(), param => CanCancelToursExecute());
        }

        public ObservableCollection<Tour> AvailableTours
        {
            get => _availableTours;
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("AvailableTours");
                }
            }
        }

        public bool CanCancelToursExecute()
        {
            return true;
        }

        public void CancelToursExecute()
        {
            List<Tour> tourToBeDeleted = new List<Tour>();

            foreach (Tour tour in AvailableTours)
            {
                if (tour.IsSelected)
                {
                    tourToBeDeleted.Add(tour);
                    _tourService.ChangeTourStatus(tour.ID, STATUS.CANCELLED);
                    foreach (Tourist tourist in tour.RegisteredTourists)
                    {
                        DateTime oneYear = DateTime.Now.AddYears(1);
                        Voucher newVoucher = new Voucher(tourist.ID, tour.ID, "Compensation voucher", false, oneYear);
                        _voucherService.VoucherRepository.Create(newVoucher);
                        tourist.WonVouchers.Add(newVoucher);
                    }
                }
            }

            foreach (Tour tour in tourToBeDeleted)
            {
                AvailableTours.Remove(tour);
            }
        }
    }
}
