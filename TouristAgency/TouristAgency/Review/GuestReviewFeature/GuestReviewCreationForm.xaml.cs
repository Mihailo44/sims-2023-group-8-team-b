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
using TouristAgency.Reservations.Domain;
using TouristAgency.Review.GuestReviewFeature;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for GuestReviewCreationForm.xaml
    /// </summary>
    public partial class GuestReviewCreationForm : Window
    {
        public GuestReviewCreationForm(Reservation SelectedReservation)
        {
            InitializeComponent();
            DataContext = new GuestReviewViewModel(SelectedReservation,this);
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            for (int i = 1; i <= 5; i++)
            {
                cbClean.Items.Add(i.ToString());
                cbRules.Items.Add(i.ToString());
                cbComm.Items.Add(i.ToString());
                cbOver.Items.Add(i.ToString());
                cbNoise.Items.Add(i.ToString());
            }
        }
    }
}