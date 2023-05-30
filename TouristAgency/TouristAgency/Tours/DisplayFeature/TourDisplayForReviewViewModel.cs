using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Review.GuideReviewDisplayFeature;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Util;
using TouristAgency.Vouchers;

namespace TouristAgency.Tours.DisplayFeature
{
    public class TourDisplayForReviewViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<Tour> _availableTours;

        private TourService _tourService;
        private VoucherService _voucherService;
        public DelegateCommand GetReviewsForTourCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public TourDisplayForReviewViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
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
            AvailableTours = new ObservableCollection<Tour>(_tourService.GetFinishedToursByGuide(_loggedInGuide));
        }

        private void InstantiateCommands()
        {
            GetReviewsForTourCmd = new DelegateCommand(param => GetReviewsForTourExecute(), param => CanGetReviewsForTourExecute());
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

        public Tour SelectedTour
        {
            get;
            set;
        }

        public bool CanGetReviewsForTourExecute()
        {
            return true;
        }

        public void GetReviewsForTourExecute()
        {
            if(SelectedTour != null)
            {
                _app.CurrentVM = new GuideReviewDisplayViewModel(SelectedTour);
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
