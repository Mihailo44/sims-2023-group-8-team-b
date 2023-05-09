using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;

namespace TouristAgency.Users.SuperGuestFeature
{
    public class SuperGuestDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Guest _loggedInGuest;
        private readonly Window _window;

        private string _username;

        public SuperGuestDisplayViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = window;

            DisplayUser();
        }

        private void DisplayUser()
        {
            Username = "Username: " + _loggedInGuest.Username;

        }

        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
    }
}
