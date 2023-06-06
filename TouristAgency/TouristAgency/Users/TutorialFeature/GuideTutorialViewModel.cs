using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Users.TutorialFeature
{
    public class GuideTutorialViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        public DelegateCommand CloseCmd { get; set; }

        public DelegateCommand PlayVideoCmd { get; set; }

        public GuideTutorialViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            InstantiateCommands();
            InstantiateMenuCommands();
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            PlayVideoCmd = new DelegateCommand(PlayVideoExecute, CanPlayVideoExecute);
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public bool CanPlayVideoExecute(object param)
        {
            return true;
        }

        public void PlayVideoExecute(object param)
        {
            string videoPath = (string)param;
            GuideTutorialDialogue x = new GuideTutorialDialogue(videoPath);
            x.Show();
        }
    }
}
