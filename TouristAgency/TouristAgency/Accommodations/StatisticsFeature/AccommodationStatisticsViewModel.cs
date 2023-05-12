using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Converter;


namespace TouristAgency.Accommodations.StatisticsFeature
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private AccommodationService _accommodationService;
        private ReservationService _reservationService;
        private PostponementRequestService _postponementRequestService;
        private RenovationRecommendationService _renovationRecommendationService;

        private int _selectedYear;
        private string _busiestMonth;
        private int _reservationsYearly;
        private int _cancelationsYearly;
        private int _postponationsYearly;
        private int _reccommendationsYearly;
        private int _reservationsMonthly;
        private int _cancelationsMonthly;
        private int _posponationsMonthly;
        private int _reccommendationsMonthly;

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

        public int ReccommendationsYearly
        {
            get => _reccommendationsYearly;
            set
            {
                if(_reccommendationsYearly != value)
                {
                    _reccommendationsYearly = value;
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

        public int CancelationsMonthly
        {
            get => _cancelationsMonthly;
            set
            {
                if(_cancelationsMonthly != value)
                {
                    _cancelationsMonthly = value;
                    OnPropertyChanged();
                }
            }
        }

        public int PostponationsMonthly
        {
            get => _posponationsMonthly;
            set
            {
                if(_posponationsMonthly != value)
                {
                    _posponationsMonthly = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ReccommendationsMonthly
        {
            get => _reccommendationsMonthly;
            set
            {
                if(_reccommendationsMonthly != value)
                {
                    _reccommendationsMonthly = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BusiestMonth
        {
            get => _busiestMonth;
            set
            {
                if (_busiestMonth != value)
                {
                    _busiestMonth = value;
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
            _renovationRecommendationService = new();
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
            List<int> stats = _accommodationService.GetAccommodationStatsByYear(_reservationService, _postponementRequestService,_renovationRecommendationService,SelectedAccommodation,SelectedYear);
            ReservationsYearly = stats[0];
            CancelationsYearly = stats[1];
            PostponationsYearly = stats[2];
            ReccommendationsYearly = stats[3];
            BusiestMonth = MonthConverter.GetMonthName(stats[4]);
        }

        private void GetStatsByMonth()
        {
            int monthNumber = MonthConverter.GetMonthId(SelectedMonth);
            List<int> stats = _accommodationService.GetAccommodationStatsByMonth(_reservationService,_postponementRequestService,_renovationRecommendationService,SelectedAccommodation,SelectedYear,monthNumber);
            ReservationsMonthly = stats[0];
            CancelationsMonthly = stats[1];
            PostponationsMonthly = stats[2];
            ReccommendationsMonthly = stats[3];
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
