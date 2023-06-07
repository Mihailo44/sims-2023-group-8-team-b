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
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Tours.TourRequestFeatures.Domain;
using TouristAgency.Users;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature
{
    public class ComplexTourDetailsDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Window _window;

        private ComplexTourRequest _request;
        private ObservableCollection<TourRequest> _parts;

        private ComplexTourRequestService _complexTourRequestService;

        public DelegateCommand CloseCmd { get; set; }

        public ComplexTourDetailsDisplayViewModel(Tourist tourist, Window window, ComplexTourRequest request)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            Request = request;       
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands(); 
        }

        public void InstantiateServices()
        {
            _complexTourRequestService = new ComplexTourRequestService();
        }

        public void InstantiateCollections()
        {
            Parts = new ObservableCollection<TourRequest>(Request.Parts);
        }

        public void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public ComplexTourRequest Request
        {
            get => _request;
            set
            {
                if(_request != value) 
                {
                    _request = value;
                    OnPropertyChanged("Request");
                }
            } 
        }

        public ObservableCollection<TourRequest> Parts
        { 
            get { return _parts; }
            set { _parts = value; }
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
