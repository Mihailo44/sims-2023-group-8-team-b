using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Reservations.Domain;
using TouristAgency.Users;

namespace TouristAgency.Reservations
{
    public class AccommodationDisplayViewModel : ViewModelBase, ICloseable, ICreate
    {
        private App _app;
        private Guest _loggedInGuest;

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

        private ReservationService _reservationService;
        private AccommodationService _accommodationService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand SearchCmd { get; set; }
        public DelegateCommand ShowAllCmd { get; set; }
        public DelegateCommand SearchDateCmd { get; set; }
        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand CancelReservationCmd { get; set; }


        public AccommodationDisplayViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _accommodationService = new AccommodationService();
        }

        private void InstantiateCollections()
        {
            Start = DateTime.Today;
            End = DateTime.Today;
            SelectedCity = "";
            SelectedCountry = "";
            SelectedName = "";
            SelectedType = "";

            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.AccommodationRepository.GetAll());
            Reservations = new ObservableCollection<Reservation>();
            Countries = new ObservableCollection<string>(_accommodationService.GetCountries());
            Cities = new ObservableCollection<string>(_accommodationService.GetCities());
            Names = new ObservableCollection<string>(_accommodationService.GetNames());
            Types = new ObservableCollection<string>(_accommodationService.GetTypes());
        }

        private void InstantiateCommands()
        {
            SearchCmd = new DelegateCommand(param => SearchCmdExecute(), param => CanSearchCmdExecute());
            ShowAllCmd = new DelegateCommand(param => ShowAllCmdExecute(), param => CanShowAllCmdExecute());
            SearchDateCmd = new DelegateCommand(param => SearchDateCmdExecute(), param => CanSearchDateCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
            CancelReservationCmd = new DelegateCommand(param => CancelReservationCmdExecute(), param => CanCancelReservationCmdExecute());
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

        //TODO bind u xaml-u
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

        public string SelectedName
        {
            get;
            set;
        }

        public string SelectedType
        {
            get;
            set;
        }

        public Accommodation SelectedAccommodation
        {
            get;
            set;
        }

        public Reservation SelectedReservation
        {
            get;
            set;
        }

        public bool CanSearchCmdExecute()
        {
            return true;
        }
        private void SearchCmdExecute()
        {

            string country = SelectedCountry;
            string city = SelectedCity;
            string name = SelectedName;
            string type = SelectedType;


            Accommodations = new ObservableCollection<Accommodation>(
              _accommodationService.Search(country, city, name, type, MaxGuestNum, MinNumOfDays));
        }

        public bool CanShowAllCmdExecute()
        {
            return true;
        }
        private void ShowAllCmdExecute()
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.AccommodationRepository.GetAll());
        }

        public bool CanSearchDateCmdExecute()
        {
            return true;
        }
        private void SearchDateCmdExecute()
        {
            Reservations.Clear();

            int numOfReservations = (End - Start).Days - NumOfDays + 2;
            Accommodation selectedAccommodation = SelectedAccommodation;

            if (selectedAccommodation != null && selectedAccommodation.MinNumOfDays <= NumOfDays)
            {
                Reservations = _reservationService.GeneratePotentionalReservations(Start, NumOfDays, numOfReservations,
                    selectedAccommodation, _loggedInGuest);

                if (Reservations.Count == 0)
                {
                    MessageBoxResult result =
                        MessageBox.Show(
                            "There are no available dates in this date range. Would you like some alternatives?", "Question", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Reservations = _reservationService.GenerateAlternativeReservations(Start, NumOfDays,
                        numOfReservations,
                        selectedAccommodation, _loggedInGuest);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a hotel or change the number of reservations");
            }
        }

        public bool CanCreateCmdExecute()
        {
            return true;
        }
        private void CreateCmdExecute()
        {
            Reservation newReservation = SelectedReservation;
            Accommodation selectedAccommodation = SelectedAccommodation;

            if (selectedAccommodation.MaxGuestNum >= NumOfPeople && selectedAccommodation.MinNumOfDays <= NumOfDays && newReservation != null)
            {
                newReservation.Accommodation = selectedAccommodation;
                newReservation.AccommodationId = selectedAccommodation.Id;
                newReservation.Guest = _loggedInGuest;
                newReservation.GuestId = _loggedInGuest.ID;
                _reservationService.ReservationRepository.Create(newReservation);
                Reservations.Remove(newReservation);
                MessageBox.Show("Successfully reserved");
            }
        }

        public bool CanCancelReservationCmdExecute()
        {
            return true;
        }
        private void CancelReservationCmdExecute()
        {
            NumOfPeople = 0;
            Reservations.Clear();
        }
    }
}
