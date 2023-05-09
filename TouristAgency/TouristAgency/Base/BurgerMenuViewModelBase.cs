using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.CreationFeature;
using TouristAgency.Statistics;
using TouristAgency.TourRequests;
using TouristAgency.TourRequests.AcceptRequestFeature;
using TouristAgency.TourRequests.StatisticsFeature;
using TouristAgency.Tours;
using TouristAgency.Tours.CancelationFeature;
using TouristAgency.Tours.StatisticsFeature;
using TouristAgency.Users;
using TouristAgency.Users.SuperGuideFeature;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.Base
{
    public class BurgerMenuViewModelBase : ViewModelBase
    {
        private string _menuVisibility;
        private App _app = (App)Application.Current;
        public DelegateCommand ShowMenuCmd { get; set; }
        public DelegateCommand HideMenuCmd { get; set; }
        public DelegateCommand CreateTourCmd { get; set; }
        public DelegateCommand ActiveTourCmd { get; set; }
        public DelegateCommand CancelTourCmd { get; set; }
        public DelegateCommand TourStatisticsCmd { get; set; }
        public DelegateCommand GuideProfileCmd { get; set; }
        public DelegateCommand TourRequestCmd { get; set; }
        public DelegateCommand TourRequestStatisticsCmd { get; set; }

        public void InstantiateMenuCommands()
        {
            CreateTourCmd = new DelegateCommand(param => CreateTourExecute(), param => CanCreateTourExecute());
            ActiveTourCmd = new DelegateCommand(param => ActiveTourExecute(), param => CanActiveTourExecute());
            CancelTourCmd = new DelegateCommand(param => CancelTourExecute(), param => CanCancelTourExecute());
            TourStatisticsCmd = new DelegateCommand(param => TourStatisticsExecute(), param => CanTourStatisticsExecute());
            GuideProfileCmd = new DelegateCommand(param => GuideProfileExecute(), param => CanGuideProfileExecute());
            TourRequestCmd = new DelegateCommand(param => TourRequestExecute(), param => CanTourRequestExecute());
            TourRequestStatisticsCmd = new DelegateCommand(param => TourRequestStatisticsExecute(), param => CanTourRequestStatisticsExecute());
            ShowMenuCmd = new DelegateCommand(param => ShowMenuExecute(), param => CanShowMenuExecute());
            HideMenuCmd = new DelegateCommand(param => HideMenuExecute(), param => CanHideMenuExecute());
        }

        public string MenuVisibility
        {
            get => _menuVisibility;
            set
            {
                if (value != _menuVisibility)
                {
                    _menuVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CanShowMenuExecute()
        {
            return true;
        }

        public void ShowMenuExecute()
        {
            MenuVisibility = "Visible";
        }

        public bool CanHideMenuExecute()
        {
            return true;
        }

        public void HideMenuExecute()
        {
            MenuVisibility = "Hidden";
        }

        public bool CanCreateTourExecute()
        {
            return true;
        }

        public void CreateTourExecute()
        {
            _app.CurrentVM = new TourCreationViewModel();
        }

        public bool CanActiveTourExecute()
        {
            return true;
        }

        public void ActiveTourExecute()
        {
            //ActiveTourDisplay activeTour = new ActiveTourDisplay(_loggedInGuide);
            //activeTour.Show();
        }

        public bool CanCancelTourExecute()
        {
            return true;
        }

        public void CancelTourExecute()
        {
            _app.CurrentVM = new CancelTourDisplayViewModel();
        }

        public bool CanTourStatisticsExecute()
        {
            return true;
        }

        public void TourStatisticsExecute()
        {
            _app.CurrentVM = new TourStatisticsDisplayViewModel();
        }

        public bool CanGuideProfileExecute()
        {
            return true;
        }

        public void GuideProfileExecute()
        {

            _app.CurrentVM = new GuideProfileDisplayViewModel();
        }

        public bool CanTourRequestExecute()
        {
            return true;
        }
        public void TourRequestExecute()
        {
            _app.CurrentVM = new TourRequestDisplayViewModel();
        }

        public bool CanTourRequestStatisticsExecute()
        {
            return true;
        }

        public void TourRequestStatisticsExecute()
        {
            _app.CurrentVM = new GuideTourRequestStatisticsDisplayViewModel();
        }
    }
}
