using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours.TourRequestFeatures.DisplayFeature
{
    public class TourRequestDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Window _window;

        private ObservableCollection<TourRequest> _tourRequests;
        private TourRequest _tourRequest;

        private TourRequestService _tourRequestService;
        private LocationService _locationService;

        public DelegateCommand CloseCmd { get; set; }

        public TourRequestDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _window = window;
            _tourRequest = new TourRequest();
            _loggedInTourist = tourist;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();
        }

        private void InstantiateCollections()
        {
            _tourRequestService.InvalidateOldTourRequests();
            _tourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetByTouristID(_loggedInTourist.ID));
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public TourRequest TourRequest
        {
            get => _tourRequest;
            set
            {
                if (value != _tourRequest)
                {
                    _tourRequest = value;
                    OnPropertyChanged("TourRequest");
                }
            }
        }

        public ObservableCollection<TourRequest> TourRequests
        {
            get => _tourRequests;
            set
            {
                if (value != _tourRequests)
                {
                    _tourRequests = value;
                    OnPropertyChanged("TourRequests");
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
