using System;
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
using System.Collections.ObjectModel;
using TouristAgency.View.Creation;
using TouristAgency.Controller;
using TouristAgency.Model;
using TouristAgency.Interfaces;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for OwnerHome.xaml
    /// </summary>
    public partial class OwnerHome : Window,IObserver
    {
        private ReservationController _reservationController;
        private AccommodationController _accommodationController;
        private GuestController _guestController;

        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }

        public OwnerHome()
        {
            InitializeComponent();
            DataContext = this;

            _reservationController = new ReservationController();
            _reservationController.Subcribe(this);

            _accommodationController = new AccommodationController();
            _accommodationController.Subscribe(this);

            _guestController = new GuestController();

            //ovo bi trebalo izmeniti da bude samo lista akomacija sa id ulogovanog vlasnika
            _reservationController.LoadAccommodationsToReservations(_accommodationController.GetAll());
            _reservationController.LoadGuestsToReservations(_guestController.GetAll());

            Reservations = new ObservableCollection<Reservation>();
            LoadReservations();
        }

        private void LoadReservations() //ovde ce ici parametar owner id, ili sta vec
        {
            Reservations.Clear();
            List<Reservation> reservations = _reservationController.GetAll().Where(r => r.Accommodation.OwnerId == 0).ToList();//magican broj
            foreach(var reservation in reservations)
            {
                Reservations.Add(reservation);
            }
        }

        public void Update()
        {
            LoadReservations();
        }

        private void MenuNewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation();
            x.Show();
        }

        private void ToolBarCreate_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation();
            x.Show();
        }

        private void DataGridReservations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DateTime now = DateTime.UtcNow.Date;
            double dateDif = (now - SelectedReservation.End).TotalDays;

            if (dateDif > 5.0)
            {
                MessageBox.Show("Guest review time window expired");// bolja poruka
            }
            else
            {
                GuestReviewCreation x = new GuestReviewCreation(SelectedReservation);
                x.Show();
            }
        }
    }
}
