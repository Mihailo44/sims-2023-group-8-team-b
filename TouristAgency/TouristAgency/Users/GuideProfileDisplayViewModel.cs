using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;

namespace TouristAgency.Users
{
    public class GuideProfileDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<Tour> _availableTours;
        private ObservableCollection<string> _years;
        private Window _window;
        private TourService _tourService;
        public DelegateCommand GetBestTourCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public GuideProfileDisplayViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuide = guide;
            _window = window;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
        }

        private void InstantiateCollections()
        {
            Years = new ObservableCollection<string>(_tourService.GetYearsForStatistics());
        }

        private void InstantiateCommands()
        {
            GetBestTourCmd = new DelegateCommand(param => GetBestTourExecute(), param => CanGetBestTourExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            Tours = new ObservableCollection<Tour>();
        }

        public ObservableCollection<Tour> Tours
        {
            get { return _availableTours; }
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("Tours");
                }
            }
        }

        public ObservableCollection<string> Years
        {
            get { return _years; }
            set
            {
                if (value != _years)
                {
                    _years = value;
                }
            }
        }

        public string SelectedYear
        {
            get;
            set;
        }

        public bool CanGetBestTourExecute()
        {
            return true;
        }

        public void GetBestTourExecute()
        {
            if (SelectedYear != null)
            {
                Tours.Clear();
                Tours.Add(_tourService.GetBestTourByYear(SelectedYear));
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
