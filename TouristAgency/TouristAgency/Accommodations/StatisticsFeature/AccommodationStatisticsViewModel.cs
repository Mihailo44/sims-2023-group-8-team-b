using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Requests.Domain;

namespace TouristAgency.Accommodations.StatisticsFeature
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private ReservationService _reservationService;
        private PostponementRequestService _postponementRequestService;

        private int _year;
        private string _month;
        private int _reservations;
        private int _cancelations;
        private int _postponations;

        public int Year
        {
            get => _year;
            set
            {
                if(_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Reservations 
        { 
            get => _reservations;
            set
            {
                if(_reservations != value)
                {
                    _reservations = value;
                    OnPropertyChanged();
                }
            } 
        }
        
        public int Cancelations 
        { 
            get => _cancelations;
            set
            {
                if(_cancelations != value)
                {
                    _cancelations = value;
                    OnPropertyChanged();
                }
            } 
        }
        public int Postponations 
        { 
            get => _postponations;
            set
            {
                if(_postponations != value)
                {
                    _postponations = value;
                    OnPropertyChanged();
                }
            } 
        }
        public string Month 
        { 
            get => _month;
            set
            {
                if(_month != value)
                {
                    _month = value;
                    OnPropertyChanged();
                }
            } 
        }

        public List<int> CbYearValues { get; set; }
        public List<string> CbMonthValues { get; set; }

        public Accommodation SelectedAccommodation { get; }
        public DelegateCommand ShowStatsCmd { get; }

        public AccommodationStatisticsViewModel(Accommodation accommodation)
        {
            SelectedAccommodation = accommodation;
            CbYearValues = new();
            CbMonthValues = new();
            InstantiateServices();
            FillCombos();
            Year = DateTime.Today.Year;
            Month = string.Empty;
            GetStatsByYear();
            ShowStatsCmd = new DelegateCommand(param => ShowStatsCmdExecute(),param => CanShowStatsCmdExecute());
        }

        private void InstantiateServices()
        {
            _reservationService = new();
            _postponementRequestService = new();
        }

        private void FillCombos()
        {
            CbYearValues.Clear();
            CbMonthValues.Clear();

            int currentYear = DateTime.Today.Year;

            for(int i = 2010; i <= currentYear; i++)
            {
                CbYearValues.Add(i);
            }

            CbYearValues.Reverse();

            CbMonthValues.Add(string.Empty);
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
            Reservations = _reservationService.GetByAccommodationId(SelectedAccommodation.Id).Where(r =>r.Start.Year == Year && r.IsCanceled == false).Count();
            Cancelations = _reservationService.GetByAccommodationId(SelectedAccommodation.Id).Where(r =>r.Start.Year == Year && r.IsCanceled == true).Count();
            Postponations = _postponementRequestService.PostponementRequestRepository.GetAll().FindAll(p => p.Reservation.Start.Year == Year && p.Reservation.AccommodationId == SelectedAccommodation.Id).Count();
            var x = _reservationService.GetByAccommodationId(SelectedAccommodation.Id).FindAll(r =>r.Start.Year == Year && r.IsCanceled == false).GroupBy(r => r.Start.Month);
        }

        private void GetStatsByMonth()
        {
            Reservations = _reservationService.GetByAccommodationId(SelectedAccommodation.Id).Where(r => r.Start.Year == Year && r.IsCanceled == false).Count();
            Cancelations = _reservationService.GetByAccommodationId(SelectedAccommodation.Id).Where(r => r.Start.Year == Year && r.IsCanceled == true).Count();
            Postponations = _postponementRequestService.PostponementRequestRepository.GetAll().FindAll(p => p.Reservation.Start.Year == Year && p.Reservation.AccommodationId == SelectedAccommodation.Id).Count();
        }

        public bool CanShowStatsCmdExecute()
        {
            return true;
        }

        public void ShowStatsCmdExecute()
        {
            GetStatsByYear();
        }
    }
}
