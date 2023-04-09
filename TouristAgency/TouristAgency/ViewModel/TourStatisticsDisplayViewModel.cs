using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Model;
using TouristAgency.View.Display;

namespace TouristAgency.ViewModel
{
    public class TourStatisticsDisplayViewModel : ViewModelBase
    {
        private ObservableCollection<Tour> _tours;
        private Tour _selectedTour;
        private Guide _loggedInGuide;
        private App _app;
        private ChartValues<int> _young;
        private ChartValues<int> _adult;
        private ChartValues<int> _old;
        private ChartValues<int> _withVoucher;
        private ChartValues<int> _withoutVoucher;

        public DelegateCommand GetStatisticsCmd { get; }
        public DelegateCommand GetReviewsCmd { get; }

        public TourStatisticsDisplayViewModel(Guide guide, Window window)
        {
            _loggedInGuide = guide;
            _tours = new ObservableCollection<Tour>();
            _app = (App)App.Current;
            Tours = new ObservableCollection<Tour>(_app.TourService.GetFinishedToursByGuide(guide));
            Young = new ChartValues<int>();
            Adult = new ChartValues<int>();
            Old = new ChartValues<int>();
            WithoutVoucher = new ChartValues<int>();
            WithVoucher = new ChartValues<int>();
            GetStatisticsCmd = new DelegateCommand(param => GetStatisticsExecute(), param => CanGetStatisticsExecute());
            GetReviewsCmd = new DelegateCommand(param => GetReviewsExecute(), param => CanGetReviewsExecute());
        }

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                if(value != _tours)
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
                if(value != _selectedTour)
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
                if(value != _young)
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
                if(value != _adult)
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
                if(value != _old)
                {
                    _old = value;
                    OnPropertyChanged("Old");
                }
            }
        }

        public ChartValues<int> WithVoucher
        {
            get { return _withVoucher; }
            set
            {
                if(value != _withVoucher)
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
                if(value != _withoutVoucher)
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
            int[] results = _app.TourService.GetTourAgeStatistics(SelectedTour);
            ClearAgeStatistics();
            Young.Add(results[0]);
            Adult.Add(results[1]);
            Old.Add(results[2]);
        }

        public void GetTourVoucherStatistics()
        {
            WithVoucher.Clear();
            WithoutVoucher.Clear();
            WithVoucher.Add(_app.VoucherService.GetVouchersFromTours(SelectedTour.ID));
            WithoutVoucher.Add(SelectedTour.RegisteredTourists.Count - _app.VoucherService.GetVouchersFromTours(SelectedTour.ID));
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
            if(SelectedTour != null && _loggedInGuide != null)
            {
                TourReviewDisplay reviewDisplay = new TourReviewDisplay(_loggedInGuide, SelectedTour);
                reviewDisplay.ShowDialog();
            }
        }
    }
}
