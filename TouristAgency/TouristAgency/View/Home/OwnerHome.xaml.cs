using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using TouristAgency.Controller;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.View.Creation;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for OwnerHome.xaml
    /// </summary>
    public partial class OwnerHome : Window, IObserver
    {
        private ReservationController _reservationController;
        private AccommodationController _accommodationController;

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }

        public Owner LoggedUser { get; set; }

        public OwnerHome(Owner owner)
        {
            InitializeComponent();
            DataContext = this;

            var app = (App)Application.Current;

            _reservationController = app.ReservationController;
            _reservationController.Subcribe(this);

            _accommodationController = app.AccommodationController;
            _accommodationController.Subscribe(this);

            LoggedUser = owner;

            Accommodations = new ObservableCollection<Accommodation>();
            LoadAccommodations(LoggedUser.ID);

            Reservations = new ObservableCollection<Reservation>();
            LoadReservations(LoggedUser.ID);
            ReviewNotification();
        }

        private void LoadAccommodations(int ownerId = 0)
        {
            Accommodations.Clear();
            List<Accommodation> accommodations = _accommodationController.GetByOwnerId(ownerId);
            foreach(var accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }

        private void LoadReservations(int ownerId = 0)
        {
            Reservations.Clear();
            List<Reservation> reservations = _reservationController.GetByOwnerId(ownerId);
            foreach (var reservation in reservations)
            {
                Reservations.Add(reservation);
            }
        }

        private void ReviewNotification()
        {
            DateTime today = DateTime.UtcNow.Date;

            string notification = "Unreviewed guests:\n";
            double dateDif;
            int changes = 0;

            foreach (var reservation in _reservationController.GetUnreviewed(LoggedUser.ID))
            {
                dateDif = (today - reservation.End).TotalDays;

                if (dateDif < 5.0)
                {
                    notification += $"{reservation.Guest.FirstName} {reservation.Guest.LastName}{dateDif} days left\n";
                    changes++;
                }
                else
                {
                    reservation.Status = REVIEW_STATUS.EXPIRED;
                    _reservationController.Update(reservation,reservation.Id);
                }
            }

            if (changes > 0)
            {
                MessageBox.Show(notification);
            }
        }

        public void Update()
        {
            LoadAccommodations(LoggedUser.ID);
            LoadReservations(LoggedUser.ID);
        }

        private void MenuNewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation(LoggedUser);
            x.Show();
        }

        private void ToolBarCreate_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation(LoggedUser);
            x.Show();
        }

        private void DataGridReservations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            string guidance = "https://youtu.be/oOni4BMeMp0?t=9";
            ProcessStartInfo ps = new ProcessStartInfo
            {
                FileName = paris,
                UseShellExecute = true
            };
            Process.Start(ps);
        }
    }
}
