using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Review.Domain;
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
        private GuideService _guideService;
        private GuideReviewService _guideReviewService;

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
            CheckForSupers();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _guideService = new GuideService();
            _guideReviewService = new GuideReviewService();
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
                if (value != _selectedTour)
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
            if (CheckStartedTourExistance() && (int)parameter != SelectedTour.ID)
            {
                MessageBox.Show("Tour: " + SelectedTour.Name + " has been already started. Please finish it before starting other tours.");
            }
            else if (SelectedTour != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to start this tour?", "Alert", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
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

        public void CheckForSupers()
        {
            foreach(Guide guide in _guideService.GuideRepository.GetAll())
            {
                //TODO20
                if(_tourService.GetTourCount(guide.ID) >= 4 && _guideReviewService.GetGuideScore(guide.ID, DateTime.Now.Year) >= 4)
                {
                    guide.Super = "super";
                    _guideService.Update(guide, guide.ID);
                }
                else
                {
                    guide.Super = "regular";
                    _guideService.Update(guide, guide.ID);
                }
            }
        }

    }
}
