using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.TourRequests;
using TouristAgency.Users;

namespace TouristAgency.Tours.TourRequestFeatures.CreationFeature
{
    public class NewPartOfComplexTourCreationViewModel : ViewModelBase
    {
        private App _app;
        private Tourist _loggedInTourist;

        public NewPartOfComplexTourCreationViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
        }
    }
}
