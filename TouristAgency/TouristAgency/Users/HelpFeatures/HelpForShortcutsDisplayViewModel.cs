using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.HelpFeatures
{
    public class HelpForShortcutsDisplayViewModel : ICloseable
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Window _window;

        public HelpForShortcutsDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current; ;
            _loggedInTourist = tourist;
            _window = window;
            InstantiateCommands();
        }

        public DelegateCommand CloseCmd { get; set; }

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
