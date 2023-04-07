using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.View.Display;

namespace TouristAgency.ViewModel
{
    public class TourDisplayViewModel : ViewModelBase, ICloseable, ICreate
    {
        private ObservableCollection<Tour> _tours;
        private ObservableCollection<string> _countires;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _languages;
        private int _minDuration;
        private int _maxDuration;
        private int _numberOfPeople;
        private int _numberOfReservation;
        private Tourist _loggedInTourist;
        private App _app;

        public DelegateCommand CloseCmd { get; }
        public DelegateCommand FilterCmd { get; }
        public DelegateCommand CreateCmd { get; }
        public DelegateCommand ClearCmd { get; }
        public DelegateCommand CancelCmd { get; }

        public TourDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;

            Tours = new ObservableCollection<Tour>(_app.TourService.GetValidTours());
            Countries = new ObservableCollection<string>(_app.TourService.GetAllCountries());
            Cities = new ObservableCollection<string>(_app.TourService.GetAllCities());
            Languages = new ObservableCollection<string>(_app.TourService.GetAllLanguages());
            _loggedInTourist = tourist;

            //TODO
            //CountryComboBox.SelectedIndex = 0;
            //CityComboBox.SelectedIndex = 0;
            //LanguageComboBox.SelectedIndex = 0;

            FilterCmd = new DelegateCommand(param => FilterCmdExecute(), param => CanFilterCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
            ClearCmd = new DelegateCommand(param => ClearCmdExecute(), param => CanClearCmdExecute());
            CancelCmd = new DelegateCommand(param => CancelCmdExecute(), param => CanCancelCmdExecute());
        }

        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                if (value != _tours)
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
                if (value != _countires)
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
                if (value != _languages)
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
                if (_minDuration != value)
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

        public int NumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                if (_numberOfPeople != value)
                {
                    _numberOfPeople = value;
                    OnPropertyChanged("NumberOfPeople");
                }
            }
        }

        public int NumberOfReservation
        {
            get => _numberOfReservation;
            set
            {
                if (value != _numberOfReservation)
                {
                    _numberOfReservation = value;
                    OnPropertyChanged("NumberOfReservation");
                }
            }
        }

        public string SelectedCountry
        {
            get;
            set;
        }

        public string SelectedCity
        {
            get;
            set;
        }

        public string SelectedLanguage
        {
            get;
            set;
        }

        public Tour SelectedTour
        {
            get;
            set;
        }

        public bool CanFilterCmdExecute()
        {
            return true;
        }

        private void FilterCmdExecute()
        {
            string country = SelectedCountry;
            string city = SelectedCity;
            string language = SelectedLanguage;

            Tours = new ObservableCollection<Tour>(_app.TourService.Search(country, city, language, MinDuration, MaxDuration, NumberOfPeople));

            if (MinDuration == 0 && MaxDuration == 0)
            {
                MessageBox.Show("You must change the value for min or max duration of tour.", "Alert");
            }
        }

        public bool CanClearCmdExecute()
        {
            return true;
        }

        private void ClearCmdExecute()
        {
            Tours = new ObservableCollection<Tour>(_app.TourService.GetValidTours());
            MinDuration = 0;
            MaxDuration = 0;
        }

        public bool CanCreateCmdExecute()
        {
            return true;
        }

        private void CreateCmdExecute()
        {
            Tour selectedTour = SelectedTour;
            if (selectedTour == null)
            {
                MessageBox.Show("You must select a tour from the list.");
                return;
            }

            int availableReservations = selectedTour.MaxAttendants - selectedTour.CurrentAttendants - NumberOfReservation;

            if (selectedTour.MaxAttendants == selectedTour.CurrentAttendants)
            {
                MessageBoxResult result = MessageBox.Show("The tour does not have any places left. Would you like to see an alternative?", "Alert", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Tours = new ObservableCollection<Tour>(_app.TourService.Search(selectedTour.ShortLocation.Country, selectedTour.ShortLocation.City, "", MinDuration, 999, NumberOfPeople));
                    List<Tour> emptyTours = new List<Tour>(Tours.Where(t => t.MaxAttendants == t.CurrentAttendants).ToList());
                    Tours.Remove(selectedTour);
                    foreach (Tour tour in emptyTours)
                    {
                        Tours.Remove(tour);
                    }
                    return;
                }
            }

            if (availableReservations < 0)
            {
                MessageBox.Show("The selected tour does not have enough capacity. Try to reduce number of reservation or pick another tour.", "Alert");
                return;
            }

            if (NumberOfReservation != 0)
            {
                if (_app.TouristService.HasActiveVoucher(_loggedInTourist))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to use one voucher for this reservation?", "Question", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        ChooseVoucherDisplay display = new ChooseVoucherDisplay(_loggedInTourist, selectedTour.ID);
                        display.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Successfully made a reservation.", "Success");
                }

                _app.TourService.RegisterTourist(selectedTour.ID, _loggedInTourist, NumberOfReservation);
                _app.TourTouristService.Create(new TourTourist(selectedTour.ID, _loggedInTourist.ID));
                _loggedInTourist.AppliedTours.Add(selectedTour);
            }
            else
            {
                MessageBox.Show("Number of reservation can not be a null!", "Alert");
            }
        }

        public bool CanCancelCmdExecute()
        {
            return true;
        }

        private void CancelCmdExecute()
        {
            NumberOfReservation = 0;
        }
    }
}
