using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Tours.TourRequestFeatures.Domain;
using TouristAgency.Users;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature
{
    public class ComplexTourDetailsDisplayViewModel : ICloseable
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Window _window;

        private ComplexTourRequest _request;

        private ComplexTourRequestService _complexTourRequestService;

        public DelegateCommand CloseCmd { get; set; }

        public ComplexTourDetailsDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
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
            _request = new ComplexTourRequest();
        }

        public void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
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
