using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;

namespace TouristAgency.Users
{
    public class GuideProfileDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<Tour> _availableTours;
        private ObservableCollection<string> _years;
        private TourService _tourService;
        public DelegateCommand GetBestTourCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public GuideProfileDisplayViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            //_window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuideStart"); ;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
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
            _app.CurrentVM = new GuideHomeViewModel();
        }
    }
}
