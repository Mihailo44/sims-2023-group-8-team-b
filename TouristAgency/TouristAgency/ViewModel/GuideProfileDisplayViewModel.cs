using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class GuideProfileDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<Tour> _availableTours;
        private ObservableCollection<String> _years;
        private Window _window;

        public DelegateCommand GetBestTourCmd { get; }
        public DelegateCommand CloseCmd { get; }
        public GuideProfileDisplayViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuide = guide;
            _window = window;
            Years = new ObservableCollection<string>(_app.TourService.GetYearsForStatistics());
            GetBestTourCmd = new DelegateCommand(param => GetBestTourExecute(), param => CanGetBestTourExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            Tours = new ObservableCollection<Tour>();
        }

        public ObservableCollection<Tour> Tours
        {
            get { return _availableTours; }
            set
            {
                if(value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("Tours");
                }
            }
        }

        public ObservableCollection<String> Years
        {
            get { return _years; }
            set
            {
                if(value != _years)
                {
                    _years = value;
                }
            }
        }

        public String SelectedYear
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
            if(SelectedYear != null)
            { 
                Tours.Clear();
                Tours.Add(_app.TourService.GetBestTourByYear(SelectedYear));
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
