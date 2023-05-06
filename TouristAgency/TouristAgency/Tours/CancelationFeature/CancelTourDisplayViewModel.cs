using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Util;
using TouristAgency.Vouchers;

namespace TouristAgency.Tours.CancelationFeature
{
    public class CancelTourDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _guide;

        private ObservableCollection<Tour> _availableTours;

        private TourService _tourService;
        private VoucherService _voucherService;
        public DelegateCommand CancelExistingTourCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public CancelTourDisplayViewModel()
        {
            _app = (App)Application.Current;
            _guide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _voucherService = new VoucherService();
        }

        private void InstantiateCollections()
        {
            AvailableTours = new ObservableCollection<Tour>(_tourService.GetCancellabeTours());
        }

        private void InstantiateCommands()
        {
            CancelExistingTourCmd = new DelegateCommand(param => CancelToursExecute(), param => CanCancelToursExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
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
                    _tourService.ChangeTourStatus(tour.ID, TourStatus.CANCELLED);
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

        public bool CanCloseExecute()
        {
            return true;
        }
        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }
    }
}
