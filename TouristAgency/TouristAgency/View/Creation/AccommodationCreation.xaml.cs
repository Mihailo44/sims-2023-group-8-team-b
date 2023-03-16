﻿using System;
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
    public partial class AccommodationCreation : Window,IDataErrorInfo
    {
        private readonly AccommodationController _controller;
        private readonly LocationController _locationController;
        private readonly PhotoController _photoController;
        private string _photoLinks;

        public Accommodation NewAccommodation { get; set; }
        public Location NewLocation { get; set; }
        public string PhotoLinks 
        { 
            get => _photoLinks;
            set
            {
                if(_photoLinks != value)
                {
                    _photoLinks = value;
                }
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "PhotoLinks")
                {
                    if (string.IsNullOrEmpty(PhotoLinks))
                        return "Required field";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "PhotoLinks" };

        public bool IsValid
        {
            get
            {
                foreach(var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

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
        }

        private void PrepareAccommodationForCreation()
        {
            NewAccommodation.OwnerId = 0; // ovo treba menjati kad login bude funkcionalan
            NewAccommodation.Location = _locationController.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim());
            NewAccommodation.LocationId = _locationController.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim()).Id;
            NewAccommodation.Type = Enum.Parse<TYPE>(cbType.SelectedValue.ToString());
        }

        private void AddPhotos()
        {
            PhotoLinks = PhotoLinks.Replace("\r\n", "|");
            string[] photoLinks = PhotoLinks.Split("|");
            foreach (string photoLink in photoLinks)
            {
                Photo photo = new Photo(photoLink, 'A', NewAccommodation.Id);
                NewAccommodation.Photos.Add(photo);
                _photoController.Create(photo);
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrepareAccommodationForCreation();
                if (NewAccommodation.IsValid)
                {
                    _controller.Create(NewAccommodation);
                    AddPhotos();
                }
                MessageBox.Show("Accommodation created successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show((ex.Message));
            }
            finally
            {
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}