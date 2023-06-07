using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Notifications.Domain;
using TouristAgency.Tours.DetailsFeature;
using TouristAgency.Users;
using TouristAgency.View.Display;
using TouristAgency.Vouchers;

namespace TouristAgency.Tours.DisplayFeature
{
    public class TourDisplayViewModel : ViewModelBase, ICloseable, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;

        private ObservableCollection<Tour> _tours;
        private ObservableCollection<string> _countries;
        private ObservableCollection<string> _cities;
        private ObservableCollection<string> _languages;

        private int _minDuration;
        private int _maxDuration;
        private int _numberOfPeople;
        private int _numberOfReservation;

        private TouristService _touristService;
        private TourService _tourService;
        private TourTouristService _tourTouristService;
        private VoucherService _voucherService;
        private TouristNotificationService _touristNotificationService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand FilterCmd { get; set; }
        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand ClearCmd { get; set; }
        public DelegateCommand CancelCmd { get; set; }
        public DelegateCommand DetailsCmd { get; set; }

        public TourDisplayViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        public TourDisplayViewModel(Tourist tourist, Tour tour)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            Tours.Clear();
            Tours.Add(tour);
        }

        private void InstantiateServices()
        {
            _touristService = new TouristService();
            _tourTouristService = new TourTouristService();
            _tourService = new TourService();
            _voucherService = new VoucherService();
            _touristNotificationService = new TouristNotificationService();
        }

        private void InstantiateCollections()
        {
            Tours = new ObservableCollection<Tour>(_tourService.GetValidTours());
            Countries = new ObservableCollection<string>(_tourService.GetAllCountries());
            Cities = new ObservableCollection<string>(_tourService.GetAllCities());
            Languages = new ObservableCollection<string>(_tourService.GetAllLanguages());
            SelectedCountry = "";
            SelectedCity = "";
            SelectedLanguage = "";
        }

        private void InstantiateCommands()
        {
            FilterCmd = new DelegateCommand(param => FilterExecute(), param => CanFilterExecute());
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
            ClearCmd = new DelegateCommand(param => ClearExecute(), param => CanClearExecute());
            CancelCmd = new DelegateCommand(param => CancelExecute(), param => CanCancelExecute());
            DetailsCmd = new DelegateCommand(DetailsExecute, CanDetailsExecute);
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
            get => _countries;
            set
            {
                if (value != _countries)
                {
                    _countries = value;
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

        public bool CanFilterExecute()
        {
            return true;
        }

        private void FilterExecute()
        {
            string country = SelectedCountry;
            string city = SelectedCity;
            string language = SelectedLanguage;

            
            Tours = new ObservableCollection<Tour>(_tourService.Search(country, city, language, MinDuration, MaxDuration, NumberOfPeople));

            if (MinDuration == 0 && MaxDuration == 0)
            {
                MessageBox.Show("You must change the value for min or max duration of tour.", "Alert");
            }
            
        }

        public bool CanClearExecute()
        {
            return true;
        }

        private void ClearExecute()
        {
            Tours = new ObservableCollection<Tour>(_tourService.GetValidTours());
            MinDuration = 0;
            MaxDuration = 0;
        }

        public bool CanCreateExecute()
        {
            return true;
        }

        private void CreateExecute()
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
                MessageBoxResult result = MessageBox.Show("The tour does not have any places left. Would you like to see an alternative?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    List<Tour> alternatives = _tourService.Search(selectedTour.ShortLocation.Country, selectedTour.ShortLocation.City, "", MinDuration, 999, NumberOfPeople);
                    alternatives = alternatives.FindAll(t => t.StartDateTime >= DateTime.Now);
                    Tours = new ObservableCollection<Tour>(alternatives);
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
                MessageBox.Show("The selected tour does not have enough capacity. Try to reduce number of reservation or pick another tour.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NumberOfReservation != 0)
            {
                if (_touristService.HasActiveVoucher(_loggedInTourist))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to use one voucher for this reservation?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        ChooseVoucherDisplay display = new ChooseVoucherDisplay(_loggedInTourist, selectedTour.ID);
                        display.Show();
                    }
                    else
                    {
                        MessageBox.Show("Successfully made a reservation.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Successfully made a reservation.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                _loggedInTourist.NumOfReservations++;
                Voucher potentionalVoucher = _voucherService.WinVoucher(_loggedInTourist.ID, selectedTour.ID, _loggedInTourist.NumOfReservations);
                if(potentionalVoucher != null) 
                {
                    _loggedInTourist.WonVouchers.Add(potentionalVoucher);
                    _touristNotificationService.NotifyAboutWonVoucher(_loggedInTourist.ID);
                }
                _touristService.Update(_loggedInTourist, _loggedInTourist.ID);
                _tourService.RegisterTourist(selectedTour.ID, _loggedInTourist, NumberOfReservation);
                _tourTouristService.TourTouristRepository.Create(new TourTourist(selectedTour.ID, _loggedInTourist.ID));
                _loggedInTourist.AppliedTours.Add(selectedTour);
                NumberOfReservation = 0;
            }
            else
            {
                MessageBox.Show("Number of reservation can not be a zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }

        private void CancelExecute()
        {
            MinDuration = 0;
            MaxDuration = 0;
            NumberOfPeople = 0;
            NumberOfReservation = 0;
        }

        public bool CanDetailsExecute(object param)
        {
            return true;
        }

        public void DetailsExecute(object param)
        {
            SelectedTour = Tours.FirstOrDefault(t => t.ID == (int)param);
            if(SelectedTour != null) 
            {
                TourDetailsDisplay display = new TourDetailsDisplay(SelectedTour);
                display.Show();
            }
        }
    }
}
