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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TouristAgency.Model;
using TouristAgency.Controller;
using System.Collections.ObjectModel;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for AccommodationCreation.xaml
    /// </summary>
    public partial class AccommodationCreation : Window
    {
        private readonly AccommodationController _controller;
        private readonly LocationController _locationController;
        private readonly PhotoController _photoController;

        public Accommodation NewAccommodation { get; set; }
        public Location NewLocation { get; set; }
        public Photo NewAccommodationPhotos { get; set; }

        public AccommodationCreation()
        {
            InitializeComponent();
            DataContext = this;

            cbType.Items.Add(TYPE.HOTEL.ToString());
            cbType.Items.Add(TYPE.APARTMENT.ToString());
            cbType.Items.Add(TYPE.HUT.ToString());

            _controller = new AccommodationController();
            _locationController = new LocationController();
            _photoController = new PhotoController();

            NewAccommodation = new();
            NewLocation = new();
            NewAccommodationPhotos = new();
        }

        private void PrepareAccommodationForCreation()
        {
            NewAccommodation.OwnerId = 0;
            NewAccommodation.Location = _locationController.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim());
            NewAccommodation.LocationId = _locationController.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim()).Id;
            NewAccommodation.Type = Enum.Parse<TYPE>(cbType.SelectedValue.ToString());
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {

            PrepareAccommodationForCreation();

            try
            {
                _controller.Create(NewAccommodation);
                NewAccommodationPhotos.ExternalID = NewAccommodation.Id;
                NewAccommodationPhotos.Type = 'A';
                NewAccommodation.Photos.Add(_photoController.Create(NewAccommodationPhotos));
            }
            catch(Exception ex)
            {
                MessageBox.Show((ex.Message));
            }
            finally
            {
                MessageBox.Show("Accommodation created successfully");
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
