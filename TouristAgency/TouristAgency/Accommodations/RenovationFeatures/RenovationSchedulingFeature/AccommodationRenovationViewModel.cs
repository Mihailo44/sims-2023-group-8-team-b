using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;


namespace TouristAgency.Accommodations.RenovationFeatures.RenovationSchedulingFeature
{
    public class AccommodationRenovationViewModel : ViewModelBase,ICreate
    {
        private DateTime _start;
        private DateTime _end;
       
        private int _estimatedDuration;
        private RenovationService _renovationService;
        private ReservationService _reservationService;

        public Accommodation SelectedAccommodation { get; }
        public Renovation SelectedRenovation { get; set; }
        public ObservableCollection<Renovation> PossibleRenovationDates { get; set; }
        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand SearchCmd { get; set; }

        public AccommodationRenovationViewModel(Accommodation accommodation)
        {
            SelectedAccommodation = accommodation;
            SelectedRenovation = new();
            InstantiateServices();
            InstantiateCommands();
            PossibleRenovationDates = new();
            SetDefaultElementValues();
            FillCollection();
        }

        private void InstantiateServices()
        {
            _renovationService = new();
            _reservationService = new();
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
            SearchCmd = new DelegateCommand(param => SearchCmdExecute(), param => CanSearchCmdExecute());
        }

        private void SetDefaultElementValues()
        {
            EstimatedDuration = 2;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(EstimatedDuration);
        }

        private void FillCollection()
        {
            PossibleRenovationDates.Clear();
            List<Renovation> renovationSuggestions = new List<Renovation>();
            renovationSuggestions = _renovationService.GeneratePotentialRenovations(StartDate,EndDate,EstimatedDuration, SelectedAccommodation, _reservationService);
            foreach(var renovation in renovationSuggestions)
            {
                PossibleRenovationDates.Add(renovation);
            }
        }

        public DateTime StartDate
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    EndDate = _start.AddDays(EstimatedDuration);
                }
            }
        }

        public DateTime EndDate
        {
            get => _end;
            set
            {
                if(_end != value)
                {
                    _end = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public int EstimatedDuration
        {
            get => _estimatedDuration;
            set
            {
                if(_estimatedDuration != value)
                {
                    _estimatedDuration = value;
                }
            }
        }

        public bool CanCreateCmdExecute()
        {
            return SelectedRenovation != null;
        }

        public void CreateCmdExecute()
        {
            RenovationDescriptionDialogue x = new(SelectedRenovation);
            x.Show();
        }

        public bool CanSearchCmdExecute()
        {
            return (EstimatedDuration > 0) && (StartDate < EndDate);
        }

        public void SearchCmdExecute()
        {
            FillCollection();
        }
    }
}
