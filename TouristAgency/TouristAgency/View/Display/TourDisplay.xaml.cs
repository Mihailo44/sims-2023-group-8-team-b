using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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
using TouristAgency.Storage;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourDisplay.xaml
    /// </summary>
    public partial class TourDisplay : Window, INotifyPropertyChanged
    {
        TourController _tourController;
        TourTouristController _tourTouristController;
        TouristController _touristController;
        private ObservableCollection<Tour> _tours;
        private ObservableCollection<string> _countires;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _languages;
        private int _minDuration;
        private int _maxDuration;
        private int _maxCapacity;
        private int _numberOfReservation;
        public event PropertyChangedEventHandler PropertyChanged;

        public TourDisplay()
        {
            InitializeComponent();
            DataContext = this;
            var app = (App)Application.Current;

            _tourController = app.TourController;
            _tourTouristController = app.TourTouristController;
            _touristController = app.TouristController;
            Tours = new ObservableCollection<Tour>(_tourController.GetValidTours());
            Countries = _tourController.GetAllCountires();
            Cities = _tourController.GetAllCitites();
            Languages = _tourController.GetAllLanguages();
        }

        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                if(value != _tours) 
                {
                    _tours = value;
                    OnPropertyChanged("Tours");
                }
            }
        }

        public ObservableCollection<string> Countries
        {
            get => _countires;
            set
            {
                if(value != _countires) 
                {
                    _countires = value;
                    OnPropertyChanged("Countries");
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

        public ObservableCollection<string> Languages
        {
            get => _languages;
            set
            {
                if(value != _languages)
                {
                    _languages = value;
                    OnPropertyChanged("Languages");
                }
            }
        }

        public int MinDuration
        {
            get => _minDuration;
            set
            {
                if(_minDuration != value)
                {
                    _minDuration = value;
                    OnPropertyChanged("MinDuration");
                }
            }
        }

        public int MaxDuration
        {
            get => _maxDuration;
            set
            {
                if (_maxDuration != value)
                {
                    _maxDuration = value;
                    OnPropertyChanged("MaxDuration");
                }
            }
        }

        public int MaxCapacity
        {
            get => _maxCapacity;
            set
            {
                if(_maxCapacity != value)
                {
                    _maxCapacity = value;
                    OnPropertyChanged("MaxCapacity");
                }
            }
        }

        public int NumberOfReservation
        {
            get => _numberOfReservation;
            set
            {
                if( value != _numberOfReservation) 
                {
                    _numberOfReservation = value;
                    OnPropertyChanged("NumberOfReservation");
                }
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string country = CountryComboBox.SelectedItem.ToString();
            string city = CityComboBox.SelectedItem.ToString();
            string language = LanguageComboBox.SelectedItem.ToString();
            
            Tours = new ObservableCollection<Tour>(_tourController.Search(country, city, language, MinDuration, MaxDuration, MaxCapacity));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Tours = new ObservableCollection<Tour>(_tourController.GetAll());
        }

        private void MakeAReservation_Click(object sender, RoutedEventArgs e)
        {
            Tour selectedTour = (Tour)ToursListView.SelectedItem;
            if(selectedTour == null)
            {
                MessageBox.Show("You must select a tour from the list.");
                return;
            }

            int availableReservations = selectedTour.MaxAttendants - selectedTour.CurrentAttendants - NumberOfReservation;

            if(selectedTour.MaxAttendants == selectedTour.CurrentAttendants) 
            {
                MessageBoxResult result = MessageBox.Show("The tour does not have any places left. Would you like to see an alternative?", "Alert", MessageBoxButton.YesNo); 
                
                if(result == MessageBoxResult.Yes) 
                {
                    Tours = new ObservableCollection<Tour>(_tourController.Search(selectedTour.ShortLocation.Country, selectedTour.ShortLocation.City, "", MinDuration, 999, MaxCapacity));
                    List<Tour> emptyTours = new List<Tour>(Tours.Where(t => t.MaxAttendants == t.CurrentAttendants).ToList());
                    Tours.Remove(selectedTour);
                    foreach(Tour tour in emptyTours) 
                    {
                        Tours.Remove(tour);
                    }
                    return;
                }
            }

            if(availableReservations < 0) 
            {
                MessageBox.Show("The selected tour does not have enough capacity. Try to reduce number of reservation or pick another tour.", "Alert");
                return;
            }

            selectedTour.CurrentAttendants += NumberOfReservation;
            _tourController.Update(selectedTour, selectedTour.ID);
            _tourTouristController.Create(new TourTourist(selectedTour.ID, 6)); //TODO zakucan je na korisnika sa IDem 6, promeni kad se implementira logovanje
            Tourist tourist = _touristController.FindById(6);
            tourist.AppliedTours.Add(selectedTour);

            MessageBox.Show("Successfully made a reservation.", "Success");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            numOfReservation.Value = 0;
        }
    }
}
