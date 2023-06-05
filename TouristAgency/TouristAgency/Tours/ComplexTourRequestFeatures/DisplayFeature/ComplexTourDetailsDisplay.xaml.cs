using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Users;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature
{
    /// <summary>
    /// Interaction logic for ComplexTourDetailsDisplay.xaml
    /// </summary>
    public partial class ComplexTourDetailsDisplay : Window
    {
        public ComplexTourDetailsDisplay(Tourist tourist, ComplexTourRequest request)
        {
            InitializeComponent();
            DataContext = new ComplexTourDetailsDisplayViewModel(tourist, this, request);
        }
    }
}
