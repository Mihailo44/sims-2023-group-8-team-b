using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TouristAgency.Model;
using TouristAgency.View.Display;
using TouristAgency.View.Home;
using TouristAgency.ViewModel;
using TouristAgency.Service;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, IDataErrorInfo
    {
   
        private UserService _userService;
        
        public App app;
        public object User { get; set; }

        private string _username;
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

        private string _password;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Required field";
                }
                else if (columnName == "Password")
                {
                    if (string.IsNullOrEmpty(Password))
                        return "Required field";
                }

                return null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            _userService = new UserService();

            Username = "(Username)";
            Password = "(Password)";
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            User = _userService.GetUser(Username,Password);

            if (User != null)
            {
                switch (User.GetType().ToString())
                {
                    case "TouristAgency.Model.Owner":
                        {
                            OwnerHome x = new OwnerHome((Owner)User);
                            x.Show();
                            Username = "";
                            Password = "";
                        }
                        break;
                    case "TouristAgency.Model.Guest":
                        {
                            AccommodationDisplay x = new AccommodationDisplay((Guest)User);
                            x.Show();
                            Username = "";
                            Password = "";
                        }
                        break;
                    case "TouristAgency.Model.Tourist":
                        {
                            TourDisplay x = new TourDisplay((Tourist)User);
                            x.Show();
                            Username = "";
                            Password = "";
                        }
                        break;
                    case "TouristAgency.Model.Guide":
                        {
                            GuideHome x = new GuideHome((Guide)User);
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                ButtonLogin_Click(sender, e);
            }
        }
    }
}
