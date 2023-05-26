using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature;
using TouristAgency.Util;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.Users.HomeDisplayFeature
{
    public class GuideHomeViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Window _window;
        private Guide _loggedInGuide;

        private ObservableCollection<Tour> _availableTours;
        private Tour _selectedTour;

        private TourService _tourService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand StartTourCmd { get; set; }
        public GuideHomeViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuideStart");
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCommands();
            InstantiateCollections();
            InstantiateMenuCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
        }

        private void InstantiateCollections()
        {
            AvailableTours = new ObservableCollection<Tour>(_tourService.GetTodayTours(_loggedInGuide.ID));
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            StartTourCmd = new DelegateCommand(StartTourExecute, CanStartTourExecute);
        }
        public ObservableCollection<Tour> AvailableTours
        {
            get => _availableTours;
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("ActiveTours");
                }
            }
        }

        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if(value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged("SelectedTour");
                }
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

        public bool CanStartTourExecute(object parameter)
        {
            return true;
        }

        public void StartTourExecute(object parameter)
        {
            if(CheckStartedTourExistance() && (int)parameter != SelectedTour.ID)
            {
                MessageBox.Show("Tour: " + SelectedTour.Name + " has been already started. Please finish it before starting other tours.");
            }
            else if(SelectedTour != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to start this tour?", "Alert", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                    _app.CurrentVM = new ActiveTourDisplayViewModel(_loggedInGuide, SelectedTour);
            }
        }

        private bool CheckStartedTourExistance()
        {
            foreach (Tour tour in AvailableTours)
            {
                if (tour.Status == TourStatus.IN_PROGRESS)
                {
                    SelectedTour = tour;
                    return true;
                }
            }
            return false;
        }

    }
}
