using System;
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
        private AccommodationController _accommodationController;
        private ReservationController _reservationController;
        private ObservableCollection<string> _countires;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _types;
        private ObservableCollection<string> _names;
        private int _maxGuestNum;
        private int _minNumOfDays;
        private DateTime _start;
        private DateTime _end;
        private int _numOfDays;
        public event PropertyChangedEventHandler PropertyChanged;


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
                if (_start != value)
                {
                    _end = value;
                    OnPropertyChanged("End");
                }
            }
        }
        public AccommodationDisplay(AccommodationController accommodationController, ReservationController reservationController)
        {
            _accommodationController = accommodationController;
            _reservationController = reservationController;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            Reservations = new ObservableCollection<Reservation>();
            InitializeComponent();
            DataContext = this;
            Countries = accommodationController.GetCountries();
            Cities = accommodationController.GetCities();
            Names = accommodationController.GetNames();
            Types = accommodationController.GetTypes();
            
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Accommodation> searchedAccommodations = new ObservableCollection<Accommodation>();
            foreach (Accommodation accommodation in _accommodationController.GetAll())
            {
                bool country = accommodation.Location.Country == CountryComboBox.SelectedItem.ToString();
                bool city = accommodation.Location.City == CityComboBox.SelectedItem.ToString();
                bool name = accommodation.Name == NameComboBox.SelectedItem.ToString();
                bool type = accommodation.Type.ToString() == TypeComboBox.SelectedItem.ToString();
                bool maxGuestNum = accommodation.MaxGuestNum >= MaxGuestNum;
                bool minNumOfDays = accommodation.MinNumOfDays <= MinNumOfDays;

                if (country && city && name && type && maxGuestNum && minNumOfDays)
                {
                    searchedAccommodations.Add(accommodation);
                }
            }

            Accommodations = searchedAccommodations;
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
        }

        private void SearchDate_Click(object sender, RoutedEventArgs e)
        {
            Reservations.Clear();

            int numOfReservations = ((End - Start).Days + 1) / NumOfDays; // TODO: Smisli formulu
            Guest tempGuest = new Guest(new User("nesto", "ftn", "miko", "mikic", new DateOnly(2001, 3, 15),
                "nesto@gmail.com", new Location("Spanija", "Madrid", "Salvadora Dalija", "5"), "065621489"));
            Accommodation selectedAccommodation = (Accommodation)AccommodationsListView.SelectedItem;

            if (selectedAccommodation != null && selectedAccommodation.MinNumOfDays <= NumOfDays)
            {
                DateTime startInterval = Start;
                DateTime endInterval = Start.AddDays(NumOfDays - 1);

                for (int i = 0; i < numOfReservations; i++)
                {
                    Reservations.Add(new Reservation(tempGuest, selectedAccommodation, startInterval.AddDays(i), endInterval.AddDays(i)));
                }
            }
            else
            {
                MessageBox.Show("Please select a hotel or change the number of reservations");
            }

            
        }
    }
}
