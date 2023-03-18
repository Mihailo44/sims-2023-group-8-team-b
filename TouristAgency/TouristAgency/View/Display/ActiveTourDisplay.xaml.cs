using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using TouristAgency.Controller;
using TouristAgency.Model;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for ActiveTourDisplay.xaml
    /// </summary>
    public partial class ActiveTourDisplay : Window, INotifyPropertyChanged
    {

        private TourController _tourController;
        private TourCheckpointController _tourCheckpointController;
        private CheckpointController _checkpointController;
        private TouristController _touristController;
        private ObservableCollection<Tour> _availableTours;
        private ObservableCollection<Checkpoint> _availableCheckpoints;
        private ObservableCollection<Tourist> _registeredTourists;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Tour> AvailableTours
        {
            get => _availableTours;
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("ActiveTours");
                }
            }
        }

        public ObservableCollection<Checkpoint> AvailableCheckpoints
        {
            get => _availableCheckpoints;
            set
            {
                if (value != _availableCheckpoints)
                {
                    _availableCheckpoints = value;
                    OnPropertyChanged("AvailableCheckpoints");
                }
            }
        }

        public ObservableCollection<Tourist> RegisteredTourists
        {
            get => _registeredTourists;
            set
            {
                if (value != _registeredTourists)
                {
                    _registeredTourists = value;
                    OnPropertyChanged("RegisteredTourists");
                }
            }
        }

        public ActiveTourDisplay(TourController tourController, TourCheckpointController tourCheckpointController, CheckpointController checkpointController, TouristController touristController)
        {
            InitializeComponent();
            _tourController = tourController;
            _tourCheckpointController = tourCheckpointController;
            _checkpointController = checkpointController;
            _touristController = touristController;
            AvailableTours = tourController.GetTodayTours();
            this.DataContext = this;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BeginTourButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO
            //Do ovog momenta bi trebalo da su ucitane sve ture, pa logika moze da ide u DAO
            //Pogledaj ostale metode i primeni slican princip
            Tour selectedTour = (Tour)AvailableToursListView.SelectedItem;
            RegisteredTourists = _tourController.GetTouristsFromTour(selectedTour.ID);
            AvailableCheckpoints = new ObservableCollection<Checkpoint>(selectedTour.Checkpoints);
        }
    }
}
