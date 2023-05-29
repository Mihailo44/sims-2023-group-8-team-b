using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Tours.TourRequestFeatures.Domain;
using TouristAgency.Users;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature
{
    public class ComplexTourDetailsDisplayViewModel
    {
        private App _app;
        private Tourist _loggedInTourist;

        private ComplexTourRequest _request;

        private ComplexTourRequestService _complexTourRequestService;

        public ComplexTourDetailsDisplayViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
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

        }
    }
}
