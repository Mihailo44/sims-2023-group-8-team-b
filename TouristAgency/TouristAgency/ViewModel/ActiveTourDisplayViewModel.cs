using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class ActiveTourDisplayViewModel : ViewModelBase, ICloseable, ICreate
    {
        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<Tour> _availableTours;
        private Tour _selectedTour;
        private ObservableCollection<TourCheckpoint> _availableCheckpoints;
        private ObservableCollection<Tourist> _registeredTourists;
        private ObservableCollection<Tourist> _arrivedTourists;

        public ActiveTourDisplayViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;

            _loggedInGuide = guide;
            AvailableTours = _app.TourViewModel.GetTodayTours(_loggedInGuide.ID);
            _arrivedTourists = new ObservableCollection<Tourist>();
            _registeredTourists = new ObservableCollection<Tourist>();
            //CheckAndSelectStartedTour();

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

        public ObservableCollection<TourCheckpoint> AvailableCheckpoints
        {
            get => _availableCheckpoints;
            set
            {
                if (value != _availableCheckpoints)
                {
                    _availableCheckpoints = value;
                    OnPropertyChanged("AvailableCheckpoints");
                }
            }
        }

        public ObservableCollection<Tourist> RegisteredTourists
        {
            get => _registeredTourists;
            set
            {
                if (value != _registeredTourists)
                {
                    _registeredTourists = value;
                    OnPropertyChanged("RegisteredTourists");
                }
            }
        }

        public ObservableCollection<Tourist> ArrivedTourists
        {
            get => _arrivedTourists;
            set
            {
                if (value != _arrivedTourists)
                {
                    _arrivedTourists = value;
                    OnPropertyChanged("ArrivedTourists");
                }
            }
        }


        public DelegateCommand CloseCmd { get; }
        public DelegateCommand CreateCmd { get; }
    }
}
