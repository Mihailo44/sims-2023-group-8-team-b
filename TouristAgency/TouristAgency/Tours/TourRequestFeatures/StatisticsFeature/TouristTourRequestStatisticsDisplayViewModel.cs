using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.TourRequests;
using TouristAgency.Users;

namespace TouristAgency.Statistics
{
    public class TourRequestStatisticsDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Tourist _loggedInTourist;

        private SeriesCollection _acceptedSeries;
        private SeriesCollection _deniedSeries;
        private string _acceptedPercentage;
        private string _deniedPercentage;
        private string _avgNumOfPeople;
        private ObservableCollection<string> _years;
        private string _selectedYear;

        private TourRequestService _tourRequestService;

        public DelegateCommand GetTourRequestStatisticsCmd { get; set; }

        public TourRequestStatisticsDisplayViewModel()
        {            
            _app = (App)Application.Current;
            _loggedInTourist = _app.LoggedUser;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            GetAcceptedTourRequestData();
            GetDeniedTourRequestData();
        }

        private void InstantiateServices()
        {
            _tourRequestService = new TourRequestService();
        }

        private void InstantiateCollections()
        {
            Years = new ObservableCollection<string>(_tourRequestService.GetYearsForStatistics());
        }

        private void InstantiateCommands()
        {
            GetTourRequestStatisticsCmd = new DelegateCommand(param => GetTourRequestStatisticsExecute(), param => CanGetTourRequestStatisticsExecute());
        }

        public SeriesCollection AcceptedSeries
        {
            get => _acceptedSeries;
            set
            {
                if (value != _acceptedSeries)
                {
                    _acceptedSeries = value;
                    OnPropertyChanged("AcceptedSeries");
                }
            }
        }

        public SeriesCollection DeniedSeries
        {
            get => _deniedSeries;
            set
            {
                if (value != _deniedSeries)
                {
                    _deniedSeries = value;
                    OnPropertyChanged("DeniedSeries");
                }
            }
        }

        public string AcceptedPercentage
        {
            get => _acceptedPercentage;
            set
            {
                if (value != _acceptedPercentage) 
                {
                    _acceptedPercentage = value;
                    OnPropertyChanged("AcceptedPercentage");
                }
            }
        }

        public string DeniedPercentage
        {
            get => _deniedPercentage;
            set
            {
                if (value != _deniedPercentage)
                {
                    _deniedPercentage = value;
                    OnPropertyChanged("DeniedPercentage");
                }
            }
        }

        public string AvgNumOfPeople
        {
            get => _avgNumOfPeople;
            set
            {
                if (value != _avgNumOfPeople)
                {
                    _avgNumOfPeople = value;
                    OnPropertyChanged("AvgNumOfPeople");
                }
            }
        }

        public ObservableCollection<string> Years
        {
            get => _years;
            set
            {
                if (value != _years)
                {
                    _years = value;
                    OnPropertyChanged("Years");
                }
            }
        }

        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (value != _selectedYear)
                {
                    _selectedYear = value;
                    OnPropertyChanged("SelectedYear");
                }
            }
        }

        public bool CanGetTourRequestStatisticsExecute()
        {
            return true;
        }

        public void GetTourRequestStatisticsExecute()
        {
            List<string> results = _tourRequestService.GetTourRequestStatisticsByYear(_loggedInTourist.ID, SelectedYear);
            AcceptedPercentage = results[0];
            DeniedPercentage = results[1];
            AvgNumOfPeople = results[2];
        }

        public void GetAcceptedTourRequestData()
        {
            AcceptedSeries = new SeriesCollection();

            foreach(TourRequestStatisticsData data in _tourRequestService.GetAcceptedGraphData(_loggedInTourist.ID))
            {
                AcceptedSeries.Add(
                    new RowSeries
                    {
                        Values = new ChartValues<int> { data.Value },
                        Title = data.Title,
                        DataLabels = true
                    }
                    ); ;
            }
        }

        public void GetDeniedTourRequestData()
        {
            DeniedSeries = new SeriesCollection();

            foreach (TourRequestStatisticsData data in _tourRequestService.GetDeniedGraphData(_loggedInTourist.ID))
            {
                DeniedSeries.Add(
                    new RowSeries
                    {
                        Values = new ChartValues<int> { data.Value },
                        Title = data.Title,
                        DataLabels = true
                    }
                    ); ;
            }
        }
    }
}

