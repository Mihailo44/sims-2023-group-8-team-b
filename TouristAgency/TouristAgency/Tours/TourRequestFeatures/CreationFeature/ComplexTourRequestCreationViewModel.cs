using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Users;

namespace TouristAgency.Tours.TourRequestFeatures.CreationFeature
{
    public class ComplexTourRequestCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;

        public DelegateCommand CreateCmd { get; set; }

        public ComplexTourRequestCreationViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;

            InstantiateCommands();
        }

        public void InstantiateCommands()
        {

        }
    }
}
