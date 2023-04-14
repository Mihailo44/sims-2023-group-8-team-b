using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.View.Creation;
using TouristAgency.ViewModel;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for OwnerHome.xaml
    /// </summary>
    public partial class OwnerHome : Window
    {
        public OwnerHome(Owner owner)
        {
            InitializeComponent();
            DataContext = new OwnerHomeViewModel(this,owner);
        }
    }
}
