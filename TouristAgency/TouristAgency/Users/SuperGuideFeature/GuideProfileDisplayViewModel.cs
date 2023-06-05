using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Review.Domain;
using TouristAgency.Tours;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Users.SuperGuideFeature
{
    public class GuideProfileDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<Tour> _availableTours;
        private ObservableCollection<string> _years;
        private string _name;
        private string _username;
        public int _totalTourCount;
        private int _yearlyTourCount;
        private string _mostUsedLanguage;
        private string _mostVisitedCity;
        private string _mostVisitedCountry;
        private string _score;
        private string _status;

        private TourService _tourService;
        private GuideReviewService _guideReviewService;
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
            _guideReviewService = new GuideReviewService();
        }

        private void InstantiateCollections()
        {
            int guideID = _loggedInGuide.ID;
            Name = _loggedInGuide.FirstName + " " + _loggedInGuide.LastName;
            Username = "@"+_loggedInGuide.Username;
            Years = new ObservableCollection<string>(_tourService.GetYearsForStatistics());
            MostUsedLanguage = _tourService.GetMostUsedLanguage(guideID);
            MostVisitedCity = _tourService.GetMostVisitedCity(guideID);
            MostVisitedCountry = _tourService.GetMostVisitedCountry(guideID);
            TotalTourCount = _tourService.GetTourCount(guideID);
            YearlyTourCount = _tourService.GetYearlyTourCount(DateTime.Now.Year, guideID);
            Score = Convert.ToString(Math.Round(_guideReviewService.GetGuideScore(_loggedInGuide.ID, DateTime.Now.Year),2));
            Status = "Status: " + _loggedInGuide.Super + " guide";
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

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public int TotalTourCount
        {
            get { return _totalTourCount; }
            set
            {
                if (value != _totalTourCount)
                {
                    _totalTourCount = value;
                    OnPropertyChanged("TotalTourCount");
                }
            }
        }

        public int YearlyTourCount
        {
            get { return _yearlyTourCount; }
            set
            {
                if (value != _yearlyTourCount)
                {
                    _yearlyTourCount = value;
                    OnPropertyChanged("YearlyTourCount");
                }
            }
        }

        public string MostUsedLanguage
        {
            get { return _mostUsedLanguage; }
            set
            {
                if (value != _mostUsedLanguage)
                {
                    _mostUsedLanguage = value;
                    OnPropertyChanged("MostUsedLanguage");
                }
            }
        }

        public string MostVisitedCity
        {
            get { return _mostVisitedCity; }
            set
            {
                if (value != _mostVisitedCity)
                {
                    _mostVisitedCity = value;
                    OnPropertyChanged("MostVisitedCity");
                }
            }
        }

        public string MostVisitedCountry
        {
            get { return _mostVisitedCountry; }
            set
            {
                if (value != _mostVisitedCountry)
                {
                    _mostVisitedCountry = value;
                    OnPropertyChanged("MostVisitedCountry");
                }
            }
        }

        public string Score
        {
            get { return _score; }
            set
            {
                if (value != _score)
                {
                    _score = value;
                    OnPropertyChanged("Score");
                }
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged("Status");
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
