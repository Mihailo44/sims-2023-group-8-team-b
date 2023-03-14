using System;
using System.Collections.Generic;
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

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    public partial class TourCreation : Window
    {
        private TourController _tourController;
        private CheckpointController _checkpointController;
        private List<Checkpoint> _suitableCheckpoints;
        private Tour _newTour;
        private Location _newLocation;
        public Tour NewTour
        {
            get => _newTour;
            set => _newTour = value;
        }

        public Location NewLocation
        {
            get => _newLocation;
            set => _newLocation = value;
        }



        public TourCreation(TourController _tourController, CheckpointController _checkpointController)
        {
            InitializeComponent();
            _newTour = new Tour();
            _newLocation = new Location();
            this._tourController = _tourController;
            this._checkpointController = _checkpointController;
            this.DataContext = this;
        }
    }
}
