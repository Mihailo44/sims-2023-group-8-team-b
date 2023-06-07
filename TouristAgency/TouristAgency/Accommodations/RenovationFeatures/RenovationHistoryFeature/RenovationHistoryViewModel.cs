using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.RenovationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.RenovationFeatures.RenovationHistoryFeature
{
    public class RenovationHistoryViewModel : ViewModelBase, IObserver
    {
        private RenovationService _renovationService;
        private readonly App _app;
        public Renovation SelectedRenovation { get; set; }
        public Accommodation SelectedAccommodation { get; } = new();
        public ObservableCollection<Renovation> Renovations { get; set; }

        public DelegateCommand OpenCancelRenovationCmd { get; set; }

        public RenovationHistoryViewModel(Accommodation accommodation)
        {
            _app = (App)App.Current;
            _renovationService = new();
            _renovationService.RenovationRepository.Subscribe(this);
            SelectedRenovation = new();
            SelectedAccommodation = accommodation;
            Renovations = new();
            LoadRenovations();

            OpenCancelRenovationCmd = new DelegateCommand(param => OpenCancelRenovationCmdExecute(), param => CanOpenCancelRenovationCmdExecute());
        }

        private void LoadRenovations()
        {
            Renovations.Clear();
            foreach (var renovation in _renovationService.GetRenovationsByAccommodationId(SelectedAccommodation.Id))
            {
                Renovations.Add(renovation);
            }
        }

        public void Update()
        {
            LoadRenovations();
        }

        public bool CanOpenCancelRenovationCmdExecute()
        {
            return SelectedRenovation != null;
        }

        public void OpenCancelRenovationCmdExecute()
        {
            if (SelectedRenovation.IsCanceled)
            {
                MessageBox.Show("Selected renovation has been canceled", "Renovation History Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                RenovationCancelationDialogue x = new RenovationCancelationDialogue(SelectedRenovation);
                x.ShowDialog();
            }

        }
    }
}
