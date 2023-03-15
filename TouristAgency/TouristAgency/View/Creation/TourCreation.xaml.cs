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
        private PhotoController _photoController;
        private List<Checkpoint> _suitableCheckpoints;
        private List<Checkpoint> _selectedCheckpoints;
        private Tour _newTour;
        private Location _newLocation;
        private string _photoLinks;
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

        public List<Checkpoint> SuitableCheckpoints
        {
            get => _selectedCheckpoints;
            set
            {
                _suitableCheckpoints = value;
            }

        }

        public string PhotoLinks
        {
            get => _photoLinks;
            set => _photoLinks = value;
        }

        public TourCreation(TourController _tourController, CheckpointController _checkpointController, PhotoController _photoController)
        {
            InitializeComponent();
            NewTour = new Tour();
            NewLocation = new Location();
            this._tourController = _tourController;
            this._checkpointController = _checkpointController;
            this._photoController = _photoController;
            _suitableCheckpoints = new List<Checkpoint>();
            _selectedCheckpoints = new List<Checkpoint>();
            this.DataContext = this;
        }

        private void CountryTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewLocation.Country != "" && NewLocation.City != "")
            {
                LoadCheckpoints();
            }
        }

        private void CityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewLocation.Country != "" && NewLocation.City != "")
            {
                LoadCheckpoints();
            }
        }

        private void LoadCheckpoints()
        {
            SuitableCheckpoints.AddRange(_checkpointController.FindSuitableByLocation(NewLocation));
            //TODO Pitaj zasto ne radi, kad proradi podesiti odgovarajucim checkpointovima tourID
        }

        private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewLocation.Country != "" && NewLocation.City != "")
            {
                LoadCheckpoints();
            }
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO Implementirati proveru da li postoji vec slika u PhotoDAO!
            _tourController.Create(_newTour);
            PhotoLinks = PhotoLinks.Replace("\r\n", "|");
            string[] photoLinks = PhotoLinks.Split("|");
            foreach (string photoLink in photoLinks)
            {
                Photo photo = new Photo(photoLink, 'T', _newTour.ID);
                _newTour.Photos.Add(photo);
                _photoController.Create(photo);
            }
        }
    }
}
