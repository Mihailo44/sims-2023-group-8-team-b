using System.Windows;
using TouristAgency.AccommodationRenovation.Domain;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.AccommodationRenovation.Dialogue
{
    public class RenovationDescriptionDialogueViewModel : ViewModelBase, ICloseable, ICreate
    {
        private string _description;
        private readonly Window _window;
        private Renovation _renovation;
        private RenovationService _renovationService;
        private AccommodationService _accommodationService;

        public DelegateCommand CreateCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public RenovationDescriptionDialogueViewModel(Renovation renovation, Window window)
        {
            _renovationService = new();
            _accommodationService = new();
            _window = window;
            Renovation = _renovation;
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(),param => CanCreateCmdExecute());
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                }
            }
        }

        public Renovation Renovation
        {
            get => _renovation;
            set
            {
                if (_renovation != value)
                {
                    _renovation = value;
                }
            }
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }

        public bool CanCreateCmdExecute()
        {
            if (Description != null)
                return true;
            else
                return false;
        }

        public void CreateCmdExecute()
        {
            Renovation.Description = Description;
            _renovationService.RenovationRepository.Update(Renovation, Renovation.Id);
            Renovation.Accommodation.CurrentlyRenovating = true;
            _accommodationService.AccommodationRepository.Update(Renovation.Accommodation, Renovation.AccommodationId);
        }
    }
}
