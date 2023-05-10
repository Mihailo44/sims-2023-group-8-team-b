using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Converter;
using TouristAgency.Requests.Domain;

namespace TouristAgency.Accommodations.StatisticsFeature
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private AccommodationService _accommodationService;
        private ReservationService _reservationService;
        private PostponementRequestService _postponementRequestService;

        private int _selectedYear;
        private string _month;
        private int _reservationsYearly;
        private int _cancelationsYearly;
        private int _postponationsYearly;
        private int _reservationsMonthly;

        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ReservationsYearly
        {
            get => _reservationsYearly;
            set
            {
                if (_reservationsYearly != value)
                {
                    _reservationsYearly = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CancelationsYearly
        {
            get => _cancelationsYearly;
            set
            {
                if (_cancelationsYearly != value)
                {
                    _cancelationsYearly = value;
                    OnPropertyChanged();
                }
            }
        }
        public int PostponationsYearly
        {
            get => _postponationsYearly;
            set
            {
                if (_postponationsYearly != value)
                {
                    _postponationsYearly = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationsMonthly
        {
            get => _reservationsMonthly;
            set
            {
                if(_reservationsMonthly != value)
                {
                    _reservationsMonthly = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BusiestMonth
        {
            get => _month;
            set
            {
                if (_month != value)
                {
                    _month = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedMonth { get; set; }

        public List<int> CbYearValues { get; set; }
        public List<string> CbMonthValues { get; set; }

        public Accommodation SelectedAccommodation { get; }
        public DelegateCommand ShowYearStatsCmd { get; }
        public DelegateCommand ShowMonthStatsCmd { get; }

        public AccommodationStatisticsViewModel(Accommodation accommodation)
        {
            SelectedAccommodation = accommodation;
            CbYearValues = new();
            CbMonthValues = new();
            InstantiateServices();
            FillCombos();
            SelectedYear = DateTime.Today.Year;
            GetStatsByYear();
            ShowYearStatsCmd = new DelegateCommand(param => ShowYearStatsCmdExecute(), param => CanShowYearStatsCmdExecute());
            ShowMonthStatsCmd = new DelegateCommand(param => ShowMonthStatsCmdExecute(), param => CanShowMonthStatsCmdExecute());
        }

        private void InstantiateServices()
        {
            _accommodationService = new();
            _reservationService = new();
            _postponementRequestService = new();
        }

        private void FillCombos()
        {
            CbYearValues.Clear();
            CbMonthValues.Clear();

            int currentYear = DateTime.Today.Year;

            for (int i = 2010; i <= currentYear; i++)
            {
                CbYearValues.Add(i);
            }

            CbYearValues.Reverse();

            CbMonthValues.Add("January");
            CbMonthValues.Add("February");
            CbMonthValues.Add("March");
            CbMonthValues.Add("April");
            CbMonthValues.Add("May");
            CbMonthValues.Add("June");
            CbMonthValues.Add("July");
            CbMonthValues.Add("August");
            CbMonthValues.Add("September");
            CbMonthValues.Add("October");
            CbMonthValues.Add("November");
            CbMonthValues.Add("December");
        }

        private void GetStatsByYear()
        {
            List<int> results = _accommodationService.GetAccommodationStatsByYear(_reservationService, _postponementRequestService, SelectedAccommodation,SelectedYear);
            ReservationsYearly = results[0];
            CancelationsYearly = results[1];
            PostponationsYearly = results[2];
            BusiestMonth = MonthConverter.GetMonthName(results[3]);
        }

        private void GetStatsByMonth()
        {
            int monthNumber = MonthConverter.GetMonthId(SelectedMonth);//proveri da li radi
            ReservationsMonthly = _reservationService.GetByAccommodationId(SelectedAccommodation.Id).Where(r => r.Start.Year == SelectedYear && r.Start.Month == monthNumber && r.IsCanceled == false).Count();
           // Cancelations = _reservationService.GetByAccommodationId(SelectedAccommodation.Id).Where(r => r.Start.Year == SelectedYear && r.Start.Month == monthNumber && r.IsCanceled == true).Count();
            //Postponations = _postponementRequestService.PostponementRequestRepository.GetAll().FindAll(p => p.Reservation.Start.Year == SelectedYear && p.Reservation.Start.Month == monthNumber && p.Reservation.AccommodationId == SelectedAccommodation.Id).Count();
        }

        public bool CanShowYearStatsCmdExecute()
        {
            return true;
        }

        public void ShowYearStatsCmdExecute()
        {
            GetStatsByYear();
        }

        public bool CanShowMonthStatsCmdExecute()
        {
            return true;
        }

        public void ShowMonthStatsCmdExecute()
        {
            GetStatsByMonth();
        }
    }
}
