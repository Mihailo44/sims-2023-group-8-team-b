using System.Windows;
using TouristAgency.AccommodationRenovation.Domain;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.AccommodationRenovation.Dialogue
{
    public class RenovationDescriptionDialogueViewModel : ViewModelBase, ICloseable, ICreate
    {
        private readonly Window _window;
        private Renovation _renovation;
        private RenovationService _renovationService;
        private AccommodationService _accommodationService;
        public string Description { get; set; }

        public DelegateCommand CreateCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public RenovationDescriptionDialogueViewModel(Renovation renovation, Window window)
        {
            _renovationService = new();
            _accommodationService = new();
            _window = window;
            Renovation = _renovation;
            Description = "";
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(),param => CanCreateCmdExecute());
        }

        public Renovation Renovation
        {
            get => _renovation;
            set
            {
                if (_renovation != value)
                {
                    _renovation = value;
                    OnPropertyChanged();
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
            //if (string.IsNullOrEmpty(Description))
             //   return false;
           // else
                return true;
        }

        public void CreateCmdExecute()
        {
           // if (string.IsNullOrEmpty(Description))
                //Description = "";

            Renovation.Description = Description.Trim();
            _renovationService.RenovationRepository.Create(Renovation);
            Renovation.Accommodation.CurrentlyRenovating = true;
            _accommodationService.AccommodationRepository.Update(Renovation.Accommodation, Renovation.AccommodationId);
            MessageBox.Show("Renovation has been scheduled");
            _window.Close();
        }
    }
}
