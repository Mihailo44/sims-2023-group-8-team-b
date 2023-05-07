using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.AccommodationRenovation.Dialogue;
using TouristAgency.AccommodationRenovation.Domain;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Reservations.Domain;

namespace TouristAgency.AccommodationRenovation.SchedulingFeature
{
    public class AccommodationRenovationViewModel : ViewModelBase,ICreate
    {
        private DateTime _start;
        private DateTime _end;
       
        private string _estimatedDuration;
        private RenovationService _renovationService;
        private ReservationService _reservationService;

        public Accommodation SelectedAccommodation { get; }
        public Renovation SelectedRenovation { get; set; }
        public ObservableCollection<Renovation> PossibleRenovationDates { get; set; }
        public DelegateCommand CreateCmd { get; }
        public DelegateCommand SearchCmd { get; }

        public AccommodationRenovationViewModel(Accommodation accommodation)
        {
            SelectedAccommodation = accommodation;
            SelectedRenovation = new();
            _renovationService = new();
            _reservationService = new();
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            EstimatedDuration = "";
            PossibleRenovationDates = new();
          
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
            SearchCmd = new DelegateCommand(param => SearchCmdExecute(), param => CanSearchCmdExecute());
        }

        public void FillCollection()
        {
            PossibleRenovationDates.Clear();
            List<Renovation> renovationSuggestions = new List<Renovation>();
            renovationSuggestions = _renovationService.GeneratePotentionalRenovations(StartDate,EndDate,int.Parse(EstimatedDuration), SelectedAccommodation, _reservationService);
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
                if(_start != value)
                {
                    _start = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        public string EstimatedDuration
        {
            get => _estimatedDuration;
            set
            {
                if(_estimatedDuration != value)
                {
                    _estimatedDuration = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CanCreateCmdExecute()
        {
            if (SelectedRenovation != null)
                return true;
            else
            {
                MessageBox.Show("Selektujte opseg datuma");
                return false;
            }
        }

        public void CreateCmdExecute()
        {
            RenovationDescriptionDialogue x = new(SelectedRenovation);
            x.Show();
        }

        public bool CanSearchCmdExecute()
        {
            return true;
        }

        public void SearchCmdExecute()
        {
            FillCollection();
        }
    }
}
