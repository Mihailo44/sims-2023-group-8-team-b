﻿using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class TourStatisticsDisplayViewModel : ViewModelBase
    {
        private ObservableCollection<Tour> _tours;
        private Tour _selectedTour;
        private Guide _guide;
        private App _app;
        private ChartValues<int> _young;
        private ChartValues<int> _adult;
        private ChartValues<int> _old;

        public DelegateCommand GetStatisticsCmd{ get; }

        public TourStatisticsDisplayViewModel(Guide guide, Window window)
        {
            _guide = guide;
            _tours = new ObservableCollection<Tour>();
            _app = (App)App.Current;
            Tours = new ObservableCollection<Tour>(_app.TourViewModel.GetAll());
            Young = new ChartValues<int>();
            Adult = new ChartValues<int>();
            Old = new ChartValues<int>();
            GetStatisticsCmd = new DelegateCommand(param => GetTourAgeStatistics(), param => CanGetStatisticsExecute());
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


        public bool CanGetStatisticsExecute()
        {
            return true;
        }

        public void GetStatisticsExecute()
        {
            if (SelectedTour != null)
            {
                GetTourAgeStatistics();
                //GetTourVoucherStatistics();
            }
        }

        public void GetTourAgeStatistics()
        {
            int[] results = _app.TourViewModel.GetTourAgeStatistics(SelectedTour);
            ClearAgeStatistics();
            Young.Add(results[0]);
            Adult.Add(results[1]);
            Old.Add(results[2]);
        }

        public void ClearAgeStatistics()
        {
            Young.Clear();
            Adult.Clear();
            Old.Clear();
        }

    }
}