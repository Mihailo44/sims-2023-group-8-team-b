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
using TouristAgency.Tours.DisplayFeature;
using TouristAgency.Tours.StatisticsFeature;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.QuitFeature;
using TouristAgency.Users.SuperGuideFeature;
using TouristAgency.Users.TutorialFeature;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.Base
{
    public class BurgerMenuViewModelBase : ViewModelBase
    {
        private string _menuVisibility;
        private App _app = (App)Application.Current;

        public DelegateCommand GuideHomeCmd { get; set; }
        public DelegateCommand ShowMenuCmd { get; set; }
        public DelegateCommand HideMenuCmd { get; set; }
        public DelegateCommand CreateTourCmd { get; set; }
        public DelegateCommand CancelTourCmd { get; set; }
        public DelegateCommand LeaveUsCmd { get; set; }
        public DelegateCommand TourStatisticsCmd { get; set; }
        public DelegateCommand GuideProfileCmd { get; set; }
        public DelegateCommand TourRequestCmd { get; set; }
        public DelegateCommand TourRequestStatisticsCmd { get; set; }
        public DelegateCommand ReviewsCmd { get; set; }
        public DelegateCommand SuperGuideCmd { get; set; }

        public DelegateCommand TutorialCmd { get; set; }
        public void InstantiateMenuCommands()
        {
            GuideHomeCmd = new DelegateCommand(param => GuideHomeExecute(), param => AlwaysExecutes());
            CreateTourCmd = new DelegateCommand(param => CreateTourExecute(), param => AlwaysExecutes());
            CancelTourCmd = new DelegateCommand(param => CancelTourExecute(), param => AlwaysExecutes());
            LeaveUsCmd = new DelegateCommand(param => LeaveUsExecute(), param => AlwaysExecutes());
            TourStatisticsCmd = new DelegateCommand(param => TourStatisticsExecute(), param => AlwaysExecutes());
            GuideProfileCmd = new DelegateCommand(param => GuideProfileExecute(), param => AlwaysExecutes());
            TourRequestCmd = new DelegateCommand(param => TourRequestExecute(), param => AlwaysExecutes());
            TourRequestStatisticsCmd = new DelegateCommand(param => TourRequestStatisticsExecute(), param => AlwaysExecutes());
            ReviewsCmd = new DelegateCommand(param => ReviewsExecute(), param => AlwaysExecutes());
            SuperGuideCmd = new DelegateCommand(param => SuperGuideExecute(), param => AlwaysExecutes());
            ShowMenuCmd = new DelegateCommand(param => ShowMenuExecute(), param => AlwaysExecutes());
            HideMenuCmd = new DelegateCommand(param => HideMenuExecute(), param => AlwaysExecutes());
            TutorialCmd = new DelegateCommand(param => TutorialExecute(), param => AlwaysExecutes());
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

        public bool AlwaysExecutes()
        {
            return true;
        }


        public void ShowMenuExecute()
        {
            MenuVisibility = "Visible";
        }


        public void HideMenuExecute()
        {
            MenuVisibility = "Hidden";
        }


        public void GuideHomeExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }


        public void CreateTourExecute()
        {
            _app.CurrentVM = new TourCreationViewModel();
        }


        public void CancelTourExecute()
        {
            _app.CurrentVM = new CancelTourDisplayViewModel();
        }

        public void LeaveUsExecute()
        {
            _app.CurrentVM = new GuideQuitViewModel();
        }


        public void TourStatisticsExecute()
        {
            _app.CurrentVM = new TourStatisticsDisplayViewModel();
        }


        public void GuideProfileExecute()
        {

            _app.CurrentVM = new GuideProfileDisplayViewModel();
        }

        public void TourRequestExecute()
        {
            _app.CurrentVM = new TourRequestDisplayViewModel();
        }

        public void TourRequestStatisticsExecute()
        {
            _app.CurrentVM = new GuideTourRequestStatisticsDisplayViewModel();
        }

        public void ReviewsExecute()
        {
            _app.CurrentVM = new TourDisplayForReviewViewModel();
        }

        public void SuperGuideExecute()
        {
            _app.CurrentVM = new SuperGuideDisplayViewModel();
        }

        public void TutorialExecute()
        {
            _app.CurrentVM = new GuideTutorialViewModel();
        }
    }
}
