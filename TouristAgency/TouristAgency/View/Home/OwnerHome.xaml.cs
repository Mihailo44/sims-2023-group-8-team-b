using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        private OwnerController _ownerController;
        private LocationController _locationController;
        private PhotoController _photoController;

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }

        public OwnerHome(ReservationController reservationController,AccommodationController accommodationController,OwnerController ownerController,LocationController locationController,PhotoController photoController)
        {
            InitializeComponent();
            DataContext = this;

            _reservationController = reservationController;
            _reservationController.Subcribe(this);

            _accommodationController = accommodationController;
            _accommodationController.Subscribe(this);

            _ownerController = ownerController;
            _ownerController.LoadAccommodationsToOwners(_accommodationController.GetAll());

            _locationController = locationController;
            _photoController = photoController;

            //ovo bi trebalo izmeniti da bude samo lista akomacija sa id ulogovanog vlasnika
           // _reservationController.LoadAccommodationsToReservations(_accommodationController.GetAll());
           // _reservationController.LoadGuestsToReservations(_guestController.GetAll());
           // _accommodationController.LoadLocationsToAccommodations(_locationController.GetAll());

            Accommodations = new ObservableCollection<Accommodation>();
            LoadAccommodations();

            Reservations = new ObservableCollection<Reservation>();
            LoadReservations();
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
            double dateDif = 0;
            int changes = 0;

            foreach (var reservation in _reservationController.GetUnreviewed())
            {
                dateDif = (today - reservation.End).TotalDays;

                if (dateDif < 5.0)
                {
                    notification += $"{reservation.Accommodation.Name} {reservation.End}\n";
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
            LoadAccommodations();
            LoadReservations();
        }

        private void MenuNewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation(_accommodationController,_locationController,_photoController);
            x.Show();
        }

        private void ToolBarCreate_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation(_accommodationController, _locationController,_photoController);
            x.Show();
        }

        private void DataGridReservations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DateTime today = DateTime.UtcNow.Date;
            double dateDif = (today - SelectedReservation.End).TotalDays;

            if (dateDif > 5.0)
            {
                MessageBox.Show("Guest review time window expired");// bolja poruka
            }
            else
            {
                GuestReviewCreation x = new GuestReviewCreation(SelectedReservation,_reservationController);
                x.Show();
            }
        }
    }
}
