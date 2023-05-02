using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.View.Display;
using TouristAgency.Users;
using TouristAgency.Tours;
using TouristAgency.Vouchers;
using TouristAgency.Interfaces;

namespace TouristAgency.Statistics
{
    public class TourStatisticsDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<Tour> _tours;
        private ChartValues<int> _young;
        private ChartValues<int> _adult;
        private ChartValues<int> _old;
        //private ChartValues<int> _withVoucher;
        private PieSeries _withVoucher;
        private ChartValues<int> _withoutVoucher;
        private Tour _selectedTour;

        private TourService _tourService;
        private VoucherService _voucherService;

        public DelegateCommand GetStatisticsCmd { get; set; }
        public DelegateCommand GetReviewsCmd { get; set; }

        public DelegateCommand CloseCmd { get; set; }

        public TourStatisticsDisplayViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _voucherService = new VoucherService();
        }

        private void InstantiateCollections()
        {
            Tours = new ObservableCollection<Tour>(_tourService.GetFinishedToursByGuide(_loggedInGuide));
            Young = new ChartValues<int>();
            Adult = new ChartValues<int>();
            Old = new ChartValues<int>();
            WithoutVoucher = new ChartValues<int>();
            WithVoucher = new PieSeries
            {
                Values = new ChartValues<int>(),
                Name = "Withvoucher"
            };
        }

        private void InstantiateCommands()
        {
            GetStatisticsCmd = new DelegateCommand(param => GetStatisticsExecute(), param => CanGetStatisticsExecute());
            GetReviewsCmd = new DelegateCommand(param => GetReviewsExecute(), param => CanGetReviewsExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                if (value != _tours)
                {
                    _tours = value;
                    OnPropertyChanged("Tours");
                }
            }
        }

        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged("SelectedTour");
                }
            }
        }

        public ChartValues<int> Young
        {
            get { return _young; }
            set
            {
                if (value != _young)
                {
                    _young = value;
                    OnPropertyChanged("Young");
                }
            }
        }

        public ChartValues<int> Adult
        {
            get { return _adult; }
            set
            {
                if (value != _adult)
                {
                    _adult = value;
                    OnPropertyChanged("Adult");
                }
            }
        }

        public ChartValues<int> Old
        {
            get { return _old; }
            set
            {
                if (value != _old)
                {
                    _old = value;
                    OnPropertyChanged("Old");
                }
            }
        }

        public PieSeries WithVoucher
        {
            get { return _withVoucher; }
            set
            {
                if (value != _withVoucher)
                {
                    _withVoucher = value;
                }
            }
        }

        public ChartValues<int> WithoutVoucher
        {
            get { return _withoutVoucher; }
            set
            {
                if (value != _withoutVoucher)
                {
                    _withoutVoucher = value;
                }
            }
        }

        public bool CanGetStatisticsExecute()
        {
            return true;
        }

        public void GetStatisticsExecute()
        {
            if (SelectedTour != null)
            {
                GetTourAgeStatistics();
                GetTourVoucherStatistics();
            }
        }

        public void GetTourAgeStatistics()
        {
            int[] results = _tourService.GetTourAgeStatistics(SelectedTour);
            ClearAgeStatistics();
            Young.Add(results[0]);
            Adult.Add(results[1]);
            Old.Add(results[2]);
        }

        public void GetTourVoucherStatistics()
        {
            WithVoucher.Values.Clear();
            WithoutVoucher.Clear();
            WithVoucher.Values.Add(_voucherService.GetVouchersFromTours(SelectedTour.ID));
            WithoutVoucher.Add(SelectedTour.RegisteredTourists.Count - _voucherService.GetVouchersFromTours(SelectedTour.ID));
        }

        public void ClearAgeStatistics()
        {
            Young.Clear();
            Adult.Clear();
            Old.Clear();
        }

        public bool CanGetReviewsExecute()
        {
            return true;
        }

        public void GetReviewsExecute()
        {
            if (SelectedTour != null && _loggedInGuide != null)
            {
                GuideReviewDisplay reviewDisplay = new GuideReviewDisplay(_loggedInGuide, SelectedTour);
                reviewDisplay.ShowDialog();
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
