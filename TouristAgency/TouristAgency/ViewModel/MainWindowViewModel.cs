using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Service;
using TouristAgency.Interfaces;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;
using TouristAgency.View.Home;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class MainWindowViewModel : ViewModelBase,ICloseable
    {
        private readonly UserService _userService;
        private Window _window;
        private string _username;
        private string _password;

        public dynamic User { get; set; }
        public DelegateCommand CloseCmd { get; }
        public DelegateCommand LoginCmd { get; }

        public MainWindowViewModel()
        {
            _userService = new();
        }

        public MainWindowViewModel(Window window)
        {
            _userService = new();
            _window = window;
            Username = "(Username)";
            Password = "(Password)";
            LoginCmd = new DelegateCommand(param => LoginExecute(),param => CanLoginExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(),param => CanCloseWindowExecute());
        }
        
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            _window.Close();
        }

        public bool CanLoginExecute()
        {
            return true;
        }

        public void LoginExecute()
        {
            User = _userService.GetUser(Username, Password);

            if (User != null)
            {
                switch (User.GetType().ToString())
                {
                    case "TouristAgency.Model.Owner":
                        {
                            OwnerHome x = new OwnerHome(User);
                            x.Show();
                            Username = "";
                            Password = "";
                        }
                        break;
                    case "TouristAgency.Model.Guest":
                        {
                            AccommodationDisplay x = new AccommodationDisplay(User);
                            x.Show();
                            Username = "";
                            Password = "";
                        }
                        break;
                    case "TouristAgency.Model.Tourist":
                        {
                            TourDisplay x = new TourDisplay(User);
                            x.Show();
                            Username = "";
                            Password = "";
                        }
                        break;
                    case "TouristAgency.Model.Guide":
                        {
                            GuideHome x = new GuideHome(User);
                            x.Show();
                            Username = "";
                            Password = "";
                        }
                        break;
                    default: MessageBox.Show("Failure"); break;
                }
            }
            else
            {
                MessageBox.Show("User is not registered");
            }
        }
    }
}
