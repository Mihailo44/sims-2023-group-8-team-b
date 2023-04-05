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
using TouristAgency.Model;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for TouristHome.xaml
    /// </summary>
    public partial class TouristHome : Window
    {
        private Tourist _loggedInTourist; 

        public TouristHome(Tourist tourist)
        {
            InitializeComponent();
            DataContext = this;
            _loggedInTourist = tourist;
        }

        private void TourDisplay_Click(object sender, RoutedEventArgs e)
        {
            TourDisplay display = new TourDisplay(_loggedInTourist);
            display.Show();
        }

        private void TourAttendance_Click(object sender, RoutedEventArgs e)
        {
            TourAttendance attendance = new TourAttendance(_loggedInTourist);
            attendance.Show();
        }

        private void TourGuideReview_Click(object sender, RoutedEventArgs e)
        {
            TourGuideReviewCreation creation = new TourGuideReviewCreation(_loggedInTourist);
            creation.Show();
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            NotificationDisplay display = new NotificationDisplay(_loggedInTourist);
            display.Show();
        }
    }
}