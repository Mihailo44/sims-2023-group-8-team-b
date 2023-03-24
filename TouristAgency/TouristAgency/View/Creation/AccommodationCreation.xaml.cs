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
    public partial class AccommodationCreation : Window
    {
        /*public string Error => null;
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
        } */

        public AccommodationCreation(Owner owner)
        {
            InitializeComponent();
            DataContext = new AccommodationViewModel(owner);
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            cbType.Items.Add(TYPE.HOTEL.ToString());
            cbType.Items.Add(TYPE.APARTMENT.ToString());
            cbType.Items.Add(TYPE.HUT.ToString());
        }

       /* private void ButtonRegister_Click(object sender, RoutedEventArgs e)
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
        }*/

    }
}
