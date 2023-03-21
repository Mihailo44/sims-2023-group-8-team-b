using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TouristAgency.Controller;
using TouristAgency.Model;
using TouristAgency.Test;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;
using TouristAgency.View.Home;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged,IDataErrorInfo
    {
        
        private TourCheckpointController _tourCheckpointController;
        private TourTouristCheckpointController _tourTouristCheckpointController;
        private TourTouristController _tourTouristController;
        private TouristController _touristController;
        private TourController _tourController;
        private CheckpointController _checkpointController;
        private OwnerController _ownerController;
        private GuestController _guestController;

        public App app;

        public object User { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if(_username != value)
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
                if(_password != value)
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
                if(columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Required field";
                }
                else if(columnName == "Password")
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

            this._ownerController = app.OwnerController;
            this._guestController = app.GuestController;
            this._tourCheckpointController = app.TourCheckpointController;
            this._tourTouristCheckpointController = app.TourTouristCheckpointController;
            this._tourCheckpointController = app.TourCheckpointController;
            this._tourTouristController = app.TourTouristController;
            this._tourController = app.TourController;
            this._touristController = app.TouristController;
            this._checkpointController = app.CheckpointController;
            

            LoadTourToTourist(_tourTouristController.GetAll());
            LoadCheckpointToTourist(_tourCheckpointController.GetAll());
        }

        public void LoadTourToTourist(List<TourTourist> tourTourists)
        {
            foreach (TourTourist tourTourist in tourTourists)
            {
                Tour tour = _tourController.FindById(tourTourist.TourID);
                Tourist tourist = _touristController.FindById(tourTourist.TouristID);
                if (tourist != null)
                {
                    tour.RegisteredTourists.Add(tourist);
                    tourist.AppliedTours.Add(tour);
                }
               
            }
        }

        public void LoadCheckpointToTourist(List<TourCheckpoint> tourCheckpoints)
        {
            foreach (TourCheckpoint tourCheckpoint in tourCheckpoints)
            {
                Tour tour = _tourController.FindById(tourCheckpoint.TourID);
                Checkpoint checkpoint = _checkpointController.FindByID(tourCheckpoint.CheckpointID);
                tour.Checkpoints.Add(checkpoint);
                _tourController.Update(tour, tour.ID);
            }
        }

        private void TourButton_Click(object sender, RoutedEventArgs e)
        {
            TourCreation creation = new TourCreation();
            creation.Show();
        }


        private void ActiveTourDisplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            ActiveTourDisplay y = new ActiveTourDisplay();
            y.Show();
        }

        private string GetUserType()
        {
            User = _ownerController.GetAll().Find(o => o.Username == Username && o.Password == Password);
            if (User != null)
            {
                return User.GetType().ToString();
            }
            User = _guestController.GetAll().Find(g => g.Username == Username && g.Password == Password);
            if (User != null)
            {
                return User.GetType().ToString();
            }
            User = _touristController.GetAll().Find(t => t.Username == Username && t.Password == Password);
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
                        }
                        break;
                    case "TouristAgency.Model.Guest":
                        {
                            AccommodationDisplay x = new AccommodationDisplay((Guest)User);
                            x.Show();
                        }
                        break;
                    case "TouristAgency.Model.Tourist":
                        {
                            TourDisplay x = new TourDisplay((Tourist)User);
                            x.Show();
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

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) 
            {
                ButtonLogin_Click(sender, e);
            }
        }
    }
}
