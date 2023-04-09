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
using TouristAgency.Model.Enums;


namespace TouristAgency.ViewModel
{
    public class MainWindowViewModel : ViewModelBase,ICloseable
    {
        private OwnerService _ownerService;
        private GuestService _guestService;
        private TouristService _touristService;
        private GuideService _guideService;
        private UserService _userService;
        private readonly App app = (App)App.Current;
        private Window _window;
        private string _username;
        private string _password;

        public dynamic User { get; set; }
        public DelegateCommand CloseCmd { get; }
        public DelegateCommand LoginCmd { get; }

        public MainWindowViewModel()
        {
            _userService = app.UserService;
        }

        public MainWindowViewModel(Window window)
        {
            _ownerService = app.OwnerService;
            _touristService = app.TouristService;
            _guideService = app.GuideService;
            _guestService = new();
            _userService = app.UserService;

            _window = window;
            Username = "User";
            Password = "Pass";
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

        public void ClearTxtBoxes()
        {
            Username = "";
            Password = "";
        }

        public void LoginExecute()
        {
            User = _userService.CheckCredentials(Username, Password);

            if (User != null)
            {
                switch (User.UserType)
                {
                    case UserType.OWNER:
                        {
                            User = _ownerService.FindById(User.ID);
                            OwnerHome x = new OwnerHome(User);
                            x.Show();
                            ClearTxtBoxes();
                        }
                        break;
                    case UserType.GUEST:
                        {
                            User = _guestService.FindById(User.ID);
                            GuestHome x = new GuestHome(User);
                            x.Show();
                            ClearTxtBoxes();
                        }
                        break;
                    case UserType.TOURIST:
                        {
                            User = _touristService.FindById(User.ID);
                            User.Username = Username;
                            User.Password = Password;
                            TouristHome x = new TouristHome(User);
                            x.Show();
                            ClearTxtBoxes();
                        }
                        break;
                    case UserType.GUIDE:
                        {
                            User = _guideService.FindById(User.ID);
                            GuideHome x = new GuideHome(User);
                            x.Show();
                            ClearTxtBoxes();
                        }
                        break;
                    default: MessageBox.Show("User type doesn't exist"); break;
                }
            }
            else
            {
                MessageBox.Show("User is not registered");
            }
        }

    }
}
