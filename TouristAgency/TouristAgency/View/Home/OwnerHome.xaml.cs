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

        /*private void DataGridReservations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedReservation != null)
            {
                DateTime today = DateTime.UtcNow.Date;
                double dateDif = (today - SelectedReservation.End).TotalDays;

                if (dateDif > 5.0)
                {
                    MessageBox.Show("Guest review time window expired");
                }
                else
                {
                    GuestReviewCreation x = new GuestReviewCreation(SelectedReservation);
                    x.Show();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Q))
            {
                MenuBitanItem_Click(sender, e);
            }
        }

        private void MenuBitanItem_Click(object sender, RoutedEventArgs e)
        {
            string paris = "https://youtu.be/gG_dA32oH44?t=22";
            string blood = "https://youtu.be/0-Tm65i96TY?t=15";
            ProcessStartInfo ps = new ProcessStartInfo
            {
                FileName = blood,
                UseShellExecute = true
            };
            Process.Start(ps);
        } */
    }
}
