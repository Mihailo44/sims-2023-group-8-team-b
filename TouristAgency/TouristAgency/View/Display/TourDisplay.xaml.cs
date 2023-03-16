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
using TouristAgency.Storage;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourDisplay.xaml
    /// </summary>
    public partial class TourDisplay : Window, INotifyPropertyChanged
    {
        TourController _tourController;
        private ObservableCollection<Tour> _tours;
        private ObservableCollection<string> _countires;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _languages;
        private int _duration;
        private int _maxCapacity;
        public event PropertyChangedEventHandler PropertyChanged;

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

        public int Duration
        {
            get => _duration;
            set
            {
                if(_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
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

        public TourDisplay(TourController tourController)
        {
            InitializeComponent();
            DataContext = this;
            _tourController = tourController;
            Tours = new ObservableCollection<Tour>(tourController.GetAll());
            Countries = tourController.GetAllCountires();
            Cities = tourController.GetAllCitites();
            Languages = tourController.GetAllLanguages();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Tour> filteredTours = new ObservableCollection<Tour>();
            foreach(Tour tour in _tourController.GetAll())
            {
                bool country = tour.ShortLocation.Country == CountryComboBox.SelectedItem.ToString();
                bool city = tour.ShortLocation.City == CityComboBox.SelectedItem.ToString();
                bool language = tour.Language == LanguageComboBox.SelectedItem.ToString();
                bool duration = tour.Duration == Duration;
                bool maxCapacity = tour.MaxAttendants >= MaxCapacity;

                if(country && city && language && duration && maxCapacity)
                {
                    filteredTours.Add(tour);
                }
            }

            Tours = filteredTours;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                        
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
    }
}
