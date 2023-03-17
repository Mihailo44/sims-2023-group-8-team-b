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
        private AccommodationController _accommodationController;
        private ObservableCollection<string> _countires;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _types;
        private ObservableCollection<string> _names;
        private int _maxGuestNum;
        private int _minNumOfDays;
        public event PropertyChangedEventHandler PropertyChanged;

        /*public List<Accommodation> Accommodations
        {
            get => _accommodations;
            set => _accommodations = value;
        }*/

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
        public AccommodationDisplay(AccommodationController accommodationController)
        {
            _accommodationController = accommodationController;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            InitializeComponent();
            DataContext = this;
            Countries = accommodationController.GetCountries();
            Cities = accommodationController.GetCities();
            Names = accommodationController.GetNames();
            Types = accommodationController.GetTypes();

            /* Owner owner1 = new Owner("njutro", "njutro123", "Nikola", "Todic", new DateOnly(1990, 5, 6),
                 "njutro123@gmail.com", new Location("Pariz", "Francuska"), "851455");
             Accommodation accommodation1 = new Accommodation("PartyHouse", owner1, new Location("Pariz", "Francuska"), TYPE.APARTMENT, 5, 2, 5);
             Accommodations.Add(accommodation1); */

            //Accommodations = _accommodationController.GetAll();

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

        
    }
}
