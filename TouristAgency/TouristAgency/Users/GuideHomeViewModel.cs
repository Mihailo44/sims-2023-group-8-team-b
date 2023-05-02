using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.Users
{
    public class GuideHomeViewModel :BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Window _window;
        private Guide _loggedInGuide;
        public DelegateCommand CloseCmd { get; set; }

        public GuideHomeViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuideStart");
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
            _window.Close();
        }
    }
}
