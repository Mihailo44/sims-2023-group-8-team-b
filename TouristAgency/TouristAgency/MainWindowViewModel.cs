using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.View.Home;
using TouristAgency.Users;
using TouristAgency.Users.Domain;
using TouristAgency.Util;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Users.GuideNavigationWindow;
using TouristAgency.Users.OwnerNavigationWindow;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.GuestNavigationWindow;

namespace TouristAgency
{
    public class MainWindowViewModel : ViewModelBase, ICloseable
    {
        private OwnerService _ownerService;
        private GuestService _guestService;
        private TouristService _touristService;
        private GuideService _guideService;
        private UserService _userService;
        private ReservationService _reservationService;
        private readonly App app = (App)Application.Current;
        private Window _window;
        private string _username;
        private string _password;

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

        public dynamic User { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand LoginCmd { get; set; }

        public MainWindowViewModel()
        {
            _userService = new();
        }

        public MainWindowViewModel(Window window)
        {
            _window = window;
            InstantiateServices();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _ownerService = new();
            _touristService = new();
            _guideService = new();
            _guestService = new();
            _userService = new();
            _reservationService = new();
        }

        private void InstantiateCommands()
        {
            LoginCmd = new DelegateCommand(param => LoginExecute(), param => CanLoginExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(), param => CanCloseWindowExecute());
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
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter username and password", "Login Dialogue", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                User = _userService.UserRepository.CheckCredentials(Username, Password);

                if (User != null)
                {
                    switch (User.UserType)
                    {
                        case UserType.OWNER:
                            {
                                User = _ownerService.OwnerRepository.GetById(User.ID);
                                app.LoggedUser = User;
                                OwnerMain x = new OwnerMain();
                                x.Show();
                                ClearTxtBoxes();
                            }
                            break;
                        case UserType.GUEST:
                            {
                                User = _guestService.GuestRepository.GetById(User.ID);
                                app.LoggedUser = User;
                                User.Username = Username;
                                User.Password = Password;
                                GuestMain x = new GuestMain();
                                x.Show();
                                ClearTxtBoxes();
                            }
                            break;
                        case UserType.TOURIST:
                            {
                                User = _touristService.TouristRepository.GetById(User.ID);
                                app.LoggedUser = User;
                                User.Username = Username;
                                User.Password = Password;
                                TouristHome x = new TouristHome(User);
                                x.Show();
                                ClearTxtBoxes();
                            }
                            break;
                        case UserType.GUIDE:
                            {
                                User = _guideService.GuideRepository.GetById(User.ID);
                                app.LoggedUser = User;
                                User.Username = Username;
                                User.Password = Password;
                                GuideMain x = new GuideMain();
                                x.Show();
                                ClearTxtBoxes();
                            }
                            break;
                        default: MessageBox.Show("User type doesn't exist"); break;
                    }
                }
                else
                {
                    MessageBox.Show("User is not registered","Login Dialogue",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
        }

    }
}
