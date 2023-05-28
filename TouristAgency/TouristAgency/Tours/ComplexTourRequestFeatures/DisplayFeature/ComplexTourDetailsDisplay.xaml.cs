using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TouristAgency.Users;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature
{
    /// <summary>
    /// Interaction logic for ComplexTourDetailsDisplay.xaml
    /// </summary>
    public partial class ComplexTourDetailsDisplay : Window
    {
        public ComplexTourDetailsDisplay(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new ComplexTourDetailsDisplayViewModel(tourist);
        }
    }
}
