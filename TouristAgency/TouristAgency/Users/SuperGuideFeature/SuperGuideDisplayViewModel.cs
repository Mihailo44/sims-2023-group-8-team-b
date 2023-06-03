using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Tours;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Util;
using TouristAgency.Vouchers;

namespace TouristAgency.Users.SuperGuideFeature
{
    public class SuperGuideDisplayViewModel : BurgerMenuViewModelBase
    {
        private App _app;
        private Guide _guide;
        public DelegateCommand CloseCmd { get; set; }
        public SuperGuideDisplayViewModel()
        {
            _app = (App)Application.Current;
            _guide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            InstantiateCommands();
            InstantiateMenuCommands();
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public bool CanCloseExecute()
        {
            return true;
        }
        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }
    }
}
