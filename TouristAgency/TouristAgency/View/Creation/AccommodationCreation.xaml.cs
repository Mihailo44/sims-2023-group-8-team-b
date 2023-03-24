﻿using System;
using System.ComponentModel;
using System.Windows;
using TouristAgency.Model;
using TouristAgency.ViewModel;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for AccommodationCreation.xaml
    /// </summary>
    public partial class AccommodationCreation : Window, IDataErrorInfo
    {
        private readonly AccommodationViewModel _accommodationViewModel;
        private readonly LocationController _locationController;
        private readonly PhotoViewModel _PhotoViewModel;
        private string _photoLinks;

        public Accommodation NewAccommodation { get; set; }
        public Owner LoggedUser { get; set; }
        public Location NewLocation { get; set; }

        public string PhotoLinks
        {
            get => _photoLinks;
            set
            {
                if (_photoLinks != value)
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
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        public AccommodationCreation(Owner owner)
        {
            InitializeComponent();
            DataContext = this;
            var app = (App)Application.Current;
            FillComboBoxes();

            _accommodationViewModel = app.AccommodationViewModel;
            _locationController = app.LocationController;
            _PhotoViewModel = app.PhotoViewModel;
            LoggedUser = owner;

            NewAccommodation = new();
            NewLocation = new();
        }

        private void FillComboBoxes()
        {
            cbType.Items.Add(TYPE.HOTEL.ToString());
            cbType.Items.Add(TYPE.APARTMENT.ToString());
            cbType.Items.Add(TYPE.HUT.ToString());
        }

        private void PrepareAccommodationForCreation()
        {
            NewAccommodation.OwnerId = LoggedUser.ID;
            NewAccommodation.Owner = LoggedUser;
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
                _PhotoViewModel.Create(photo);
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrepareAccommodationForCreation();
                if (NewAccommodation.IsValid && IsValid)
                {
                    _accommodationViewModel.Create(NewAccommodation);
                    AddPhotos();
                    MessageBox.Show("Accommodation created successfully");
                }
                else
                {
                    MessageBox.Show("Ne budi nepismen");
                }
            }
            catch (Exception ex)
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
