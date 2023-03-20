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
        private ObservableCollection<Tour> _tours;
        private ObservableCollection<string> _countires;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _languages;
        private int _minDuration;
        private int _maxDuration;
        private int _maxCapacity;
        private int _numberOfReservation;
        private Tourist loggedInTourist;
        private App _app;
        public event PropertyChangedEventHandler PropertyChanged;

        public TourDisplay(Tourist tourist)
        {
            InitializeComponent();
            DataContext = this;
            _app = (App)Application.Current;

            Tours = new ObservableCollection<Tour>(_app.TourController.GetValidTours());;
            Countries = _app.TourController.GetAllCountires();
            Cities = _app.TourController.GetAllCitites();
            Languages = _app.TourController.GetAllLanguages();
            loggedInTourist = tourist;

            foreach(var ttc in _app.TourTouristCheckpointController.GetPendingInvitations(tourist.ID))
            {
                MessageBoxResult result = MessageBox.Show("Are you at " + _app.CheckpointController.FindByID(ttc.TourCheckpoint.CheckpointID).AttractionName + "?", "Question", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    _app.TourTouristCheckpointController.AcceptInvitation(tourist.ID, ttc.TourCheckpoint.CheckpointID);
                }
            }

            CountryComboBox.SelectedIndex = 0;
            CityComboBox.SelectedIndex = 0;
            LanguageComboBox.SelectedIndex = 0;
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
            
            Tours = new ObservableCollection<Tour>(_app.TourController.Search(country, city, language, MinDuration, MaxDuration, MaxCapacity));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Tours = new ObservableCollection<Tour>(_app.TourController.GetAll());
            MinDurationIntegerUpDown.Value = 0;
            MaxDurationIntegerUpDown.Value = 0;
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
                    Tours = new ObservableCollection<Tour>(_app.TourController.Search(selectedTour.ShortLocation.Country, selectedTour.ShortLocation.City, "", MinDuration, 999, MaxCapacity));
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
            _app.TourController.Update(selectedTour, selectedTour.ID);
            _app.TourTouristController.Create(new TourTourist(selectedTour.ID, loggedInTourist.ID)); 
            Tourist tourist = _app.TouristController.FindById(loggedInTourist.ID);
            tourist.AppliedTours.Add(selectedTour);

            MessageBox.Show("Successfully made a reservation.", "Success");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            numOfReservation.Value = 0;
        }
    }
}
