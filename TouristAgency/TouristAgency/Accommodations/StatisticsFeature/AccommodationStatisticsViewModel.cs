using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.Domain.DTO;
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
        private string _selectedMonth;
        private string _busiestMonth;

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

        public string SelectedMonth 
        { 
            get => _selectedMonth;
            set
            {
                if (_selectedMonth != value) 
                {
                    _selectedMonth = value;
                    OnPropertyChanged();
                }
            } 
        }

        public List<int> CbYearValues { get; set; }
        public List<string> CbMonthValues { get; set; }

        public AccommodationStatisticsDTO YearlyStats { get; set; }
        public AccommodationStatisticsDTO MonthlyStats { get; set; }
        public ObservableCollection<AccommodationStatisticsDTO> StatsList { get; set; }
        public Accommodation SelectedAccommodation { get; }
        public DelegateCommand ShowYearStatsCmd { get; }
        public DelegateCommand ShowMonthStatsCmd { get; }

        public AccommodationStatisticsViewModel(Accommodation accommodation)
        {
            SelectedAccommodation = accommodation;
            CbYearValues = new();
            CbMonthValues = new();
            YearlyStats = new();
            MonthlyStats = new();
            StatsList = new();
            InstantiateServices();
            FillCombos();
            SelectedYear = DateTime.Today.Year;
            GetStatsByYear();
            GetStatsByMonth();

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
            YearlyStats.Reservations = stats[0];
            YearlyStats.Cancelations = stats[1];
            YearlyStats.Postponations = stats[2];
            YearlyStats.Reccommendations = stats[3];
            BusiestMonth = MonthConverter.GetMonthName(stats[4]);
            SelectedMonth = BusiestMonth;
            GetStatsByMonth();
        }

        private void GetStatsByMonth()
        {
            int monthNumber = MonthConverter.GetMonthNumber(SelectedMonth);
            MonthlyStats = _accommodationService.GetAccommodationStatsByMonth(_reservationService,_postponementRequestService,_renovationRecommendationService,SelectedAccommodation,SelectedYear,monthNumber);
            StatsList.Clear();
            StatsList.Add(MonthlyStats);
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
