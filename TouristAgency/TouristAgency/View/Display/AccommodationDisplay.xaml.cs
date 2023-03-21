﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using TouristAgency.Controller;
using TouristAgency.Model;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for AccommodationDisplay.xaml
    /// </summary>
    public partial class AccommodationDisplay : Window, INotifyPropertyChanged
    {
        //private List<Accommodation> _accommodations;
        private ObservableCollection<Accommodation> _accommodations;
        private ObservableCollection<Reservation> _reservations;
        private ObservableCollection<string> _countires;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _types;
        private ObservableCollection<string> _names;
        private int _maxGuestNum;
        private int _minNumOfDays;
        private DateTime _start;
        private DateTime _end;
        private int _numOfDays;
        private int _numOfPeople;
        private Guest _loggedInGuest;
        private App _app;
        public event PropertyChangedEventHandler PropertyChanged;

        public AccommodationDisplay(Guest guest)
        {
            InitializeComponent();
            DataContext = this;
            
            _app = (App)Application.Current;

            Accommodations = new ObservableCollection<Accommodation>(_app.AccommodationController.GetAll());
            Reservations = new ObservableCollection<Reservation>();
            Countries = _app.AccommodationController.GetCountries();
            Cities = _app.AccommodationController.GetCities();
            Names = _app.AccommodationController.GetNames();
            Types = _app.AccommodationController.GetTypes();
            Start = DateTime.Today;
            End = DateTime.Today;

            _loggedInGuest = guest;

            CountryComboBox.SelectedIndex = 0;
            CityComboBox.SelectedIndex = 0;
            TypeComboBox.SelectedIndex = 0;
            NameComboBox.SelectedIndex = 0;
        }


        public ObservableCollection<Accommodation> Accommodations
        {
            get => _accommodations;
            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged("Accommodations");
                }
            }
        }

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                if (value != _reservations)
                {
                    _reservations = value;
                    OnPropertyChanged("Reservations");
                }
            }
        }

        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
                }
            }
        }

        public ObservableCollection<string> Countries
        {
            get => _countires;
            set
            {
                if (value != _countires)
                {
                    _countires = value;
                    OnPropertyChanged("Countries");
                }
            }
        }

        public ObservableCollection<string> Names
        {
            get => _names;
            set
            {
                if (value != _names)
                {
                    _names = value;
                    OnPropertyChanged("Names");
                }
            }
        }

        public ObservableCollection<string> Types
        {
            get => _types;
            set
            {
                if (value != _types)
                {
                    _types = value;
                    OnPropertyChanged("Types");
                }
            }
        }

        public int MaxGuestNum
        {
            get => _maxGuestNum;
            set
            {
                if (_maxGuestNum != value)
                {
                    _maxGuestNum = value;
                    OnPropertyChanged("MaxGuestNum");
                }
            }
        }

        public int MinNumOfDays
        {
            get => _minNumOfDays;
            set
            {
                if (_minNumOfDays != value)
                {
                    _minNumOfDays = value;
                    OnPropertyChanged("MinNumOfDays");
                }
            }
        }

        public int NumOfDays
        {
            get => _numOfDays;
            set
            {
                if (_numOfDays != value)
                {
                    _numOfDays = value;
                    OnPropertyChanged("NumOfDays");
                }
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                }
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                {
                    _end = value;
                    OnPropertyChanged("End");
                }
            }
        }

        public int NumOfPeople
        {
            get => _numOfPeople;
            set
            {
                if (_numOfPeople != value)
                {
                    _numOfPeople = value;
                    OnPropertyChanged("NumOfPeople");
                }
            }
        }
       

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            
                string country = CountryComboBox.SelectedItem.ToString();
                string city = CityComboBox.SelectedItem.ToString();
                string name = NameComboBox.SelectedItem.ToString();
                string type = TypeComboBox.SelectedItem.ToString();


                Accommodations = new ObservableCollection<Accommodation>(
                    _app.AccommodationController.Search(country, city, name, type, MaxGuestNum, MinNumOfDays));
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            Accommodations = new ObservableCollection<Accommodation>(_app.AccommodationController.GetAll());
        }

        private void SearchDate_Click(object sender, RoutedEventArgs e)
        {
            Reservations.Clear();

            int numOfReservations = ((End - Start).Days - NumOfDays + 2); // TODO: Smisli formulu
            Accommodation selectedAccommodation = (Accommodation)AccommodationsListView.SelectedItem;

            if (selectedAccommodation != null && selectedAccommodation.MinNumOfDays <= NumOfDays)
            {
                DateTime startInterval = Start;
                DateTime endInterval = Start.AddDays(NumOfDays - 1);

                for (int i = 0; i < numOfReservations; i++)
                {
                    if (_app.ReservationController.IsReserved(selectedAccommodation.Id, startInterval.AddDays(i), endInterval.AddDays(i)) ==
                        false)
                    {
                        Reservations.Add(new Reservation(_loggedInGuest, selectedAccommodation, startInterval.AddDays(i), endInterval.AddDays(i)));
                    }
                    
                }

                if (ReservationsListView.Items.Count == 0)
                {
                    MessageBoxResult result =
                        MessageBox.Show(
                            "There are no available dates in this date range. Would you like some alternatives?", "Question", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        DateTime startFirstInterval = Start.AddMonths(1);
                        DateTime startSecondInterval = Start.AddMonths(-1);
                        DateTime endFirstInterval = startFirstInterval.AddDays(NumOfDays - 1);
                        DateTime endSecondInterval = startSecondInterval.AddDays(NumOfDays - 1);

                        for (int i = 0; i < numOfReservations; i++)
                        {
                            if (_app.ReservationController.IsReserved(selectedAccommodation.Id, startSecondInterval.AddDays(i), endSecondInterval.AddDays(i)) ==
                                false)
                            {
                                Reservations.Add(new Reservation(_loggedInGuest, selectedAccommodation, startSecondInterval.AddDays(i), endSecondInterval.AddDays(i)));
                            }
                        }

                        for (int i = 0; i < numOfReservations; i++)
                        {
                            if (_app.ReservationController.IsReserved(selectedAccommodation.Id, startFirstInterval.AddDays(i), endFirstInterval.AddDays(i)) ==
                                false)
                            {
                                Reservations.Add(new Reservation(_loggedInGuest, selectedAccommodation, startFirstInterval.AddDays(i), endFirstInterval.AddDays(i)));
                            }

                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Please select a hotel or change the number of reservations");
            }

            
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            Reservation newReservation = (Reservation)ReservationsListView.SelectedItem;
            Accommodation selectedAccommodation = (Accommodation)AccommodationsListView.SelectedItem;


            if (selectedAccommodation.MaxGuestNum >= NumOfPeople && selectedAccommodation.MinNumOfDays <= NumOfDays && newReservation != null)
            {
                newReservation.Accommodation = selectedAccommodation;
                newReservation.AccommodationId = selectedAccommodation.Id;
                newReservation.Guest = _loggedInGuest;
                newReservation.GuestId = _loggedInGuest.ID;
                _app.ReservationController.Create(newReservation);
                Reservations.Remove(newReservation);
                MessageBox.Show("Successfully reserved");
            }
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            NumOfPeople = 0;
            Reservations.Clear();
        }
    }
}
