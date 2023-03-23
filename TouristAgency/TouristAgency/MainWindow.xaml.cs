using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TouristAgency.Model;
using TouristAgency.View.Display;
using TouristAgency.View.Home;
using TouristAgency.ViewModel;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private OwnerViewModel _OwnerViewModel;
        private GuestController _guestController;
        private TouristController _touristController;
        private GuideController _guideController;

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

            _OwnerViewModel = app.OwnerViewModel;
            _guestController = app.GuestController;
            _touristController = app.TouristController;
            _guideController = app.GuideController;
            Username = "(Username)";
            Password = "(Password)";
        }

        private string GetUserType()
        {
            User = _OwnerViewModel.GetAll().Find(o => o.Username == Username.Trim() && o.Password == Password.Trim());
            if (User != null)
            {
                return User.GetType().ToString();
            }
            User = _guestController.GetAll().Find(g => g.Username == Username.Trim() && g.Password == Password.Trim());
            if (User != null)
            {
                return User.GetType().ToString();
            }
            User = _touristController.GetAll().Find(t => t.Username == Username.Trim() && t.Password == Password.Trim());
            if (User != null)
            {
                return User.GetType().ToString();
            }
            User = _guideController.GetAll().Find(g => g.Username == Username && g.Password == Password);
            if (User != null)
            {
                return User.GetType().ToString();
            }

            return null;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string type = GetUserType();

            if (type != null)
            {
                switch (type)
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
