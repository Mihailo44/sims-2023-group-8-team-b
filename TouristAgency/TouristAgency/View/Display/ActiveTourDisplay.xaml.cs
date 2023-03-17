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
        private ObservableCollection<Tour> _activeTours;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Tour> ActiveTours
        {
            get => _activeTours;
            set
            {
                if (value != _activeTours)
                {
                    _activeTours = value;
                    OnPropertyChanged("ActiveTours");
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
            ActiveTours = tourController.GetTodayTours();
            this.DataContext = this;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
