using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.Users
{
    public class GuideHomeViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Window _window;
        private Guide _loggedInGuide;
        private string _menuVisibility;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand CreateTourCmd { get; set; }
        public DelegateCommand ActiveTourCmd { get; set; }
        public DelegateCommand CancelTourCmd { get; set; }
        public DelegateCommand TourStatisticsCmd { get; set; }
        public DelegateCommand GuideProfileCmd { get; set; }
        public DelegateCommand ShowMenuCmd { get; set; }
        public DelegateCommand HideMenuCmd { get; set; }

        public string MenuVisibility
        {
            get => _menuVisibility;
            set
            {
                if(value != _menuVisibility)
                {
                    _menuVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public GuideHomeViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuideStart");
            MenuVisibility = "Hidden";
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            CreateTourCmd = new DelegateCommand(param => CreateTourExecute(), param => CanCreateTourExecute());
            ActiveTourCmd = new DelegateCommand(param => ActiveTourExecute(), param => CanActiveTourExecute());
            CancelTourCmd = new DelegateCommand(param => CancelTourExecute(), param => CanCancelTourExecute());
            TourStatisticsCmd = new DelegateCommand(param => TourStatisticsExecute(), param => CanTourStatisticsExecute());
            GuideProfileCmd = new DelegateCommand(param => GuideProfileExecute(), param => CanGuideProfileExecute());
            ShowMenuCmd = new DelegateCommand(param => ShowMenuExecute(), param => CanShowMenuExecute());
            HideMenuCmd = new DelegateCommand(param => HideMenuExecute(), param => CanHideMenuExecute());
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }

        public bool CanCreateTourExecute()
        {
            return true;
        }

        public void CreateTourExecute()
        {
            TourCreation creation = new TourCreation(_loggedInGuide);
            creation.Show();
        }

        public bool CanActiveTourExecute()
        {
            return true;
        }

        public void ActiveTourExecute()
        {
            ActiveTourDisplay activeTour = new ActiveTourDisplay(_loggedInGuide);
            activeTour.Show();
        }

        public bool CanCancelTourExecute()
        {
            return true;
        }

        public void CancelTourExecute()
        {
            CancelTourDisplay cancelTour = new CancelTourDisplay(_loggedInGuide);
            cancelTour.Show();
        }

        public bool CanTourStatisticsExecute()
        {
            return true;
        }

        public void TourStatisticsExecute()
        {
            TourStatisticsDisplay tourStatistics = new TourStatisticsDisplay(_loggedInGuide);
            tourStatistics.Show();
        }

        public bool CanGuideProfileExecute()
        {
            return true;
        }

        public void GuideProfileExecute()
        {

            _app.CurrentVM = new GuideProfileDisplayViewModel();
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
    }
}
