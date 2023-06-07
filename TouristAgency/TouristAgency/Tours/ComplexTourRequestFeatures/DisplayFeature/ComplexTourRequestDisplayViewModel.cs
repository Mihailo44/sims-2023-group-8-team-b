using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Tours.TourRequestFeatures.Domain;
using TouristAgency.Users;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature
{
    public class ComplexTourRequestDisplayViewModel : ViewModelBase, ICloseable
    {
        private Tourist _loggedInTourist;
        private App _app;
        private Window _window;

        private ComplexTourRequestService _complexTourRequestService;

        private ObservableCollection<ComplexTourRequest> _requests;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand DetailsCmd { get; set; }

        public ComplexTourRequestDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            InstantiateService();
            InstantiateCollection();
            InstantiateCommands();
        }

        private void InstantiateService()
        {
            _complexTourRequestService = new ComplexTourRequestService();
        }

        private void InstantiateCollection()
        {
            _requests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestService.GetByTouristID(_loggedInTourist.ID));
        }

        private void InstantiateCommands()
        {
            DetailsCmd = new DelegateCommand(DetailsExecute, CanDetailsExecute);
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public ObservableCollection<ComplexTourRequest> Requests
        {
            get => _requests;
            set
            {
                if (value != _requests)
                {
                    _requests = value;
                    OnPropertyChanged("Requests");
                }
            }
        }

        public ComplexTourRequest SelectedTourRequest
        {
            get;
            set;
        }

        public bool CanDetailsExecute(object param)
        {
            return true;
        }

        public void DetailsExecute(object param)
        {
            SelectedTourRequest = Requests.FirstOrDefault(c => c.ID == (int)param);
            if (SelectedTourRequest != null)
            {
                ComplexTourDetailsDisplay display = new ComplexTourDetailsDisplay(_loggedInTourist, SelectedTourRequest);
                display.Show();
            }
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }
    }
}
