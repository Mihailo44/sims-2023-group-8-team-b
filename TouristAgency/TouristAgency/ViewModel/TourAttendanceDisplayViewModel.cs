using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Model;
using TouristAgency.Interfaces;

namespace TouristAgency.ViewModel
{
    public class TourAttendanceDisplayViewModel : ViewModelBase, IObserver
    {
        private ObservableCollection<Tour> _tours;
        private Tourist _loggedInTourist;
        private string _activeCheckpoint;
        private App _app;

        public DelegateCommand ShowCheckpointInfoCmd { get; }
        public DelegateCommand JoinCmd { get; }

        public TourAttendanceDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            Tours = new ObservableCollection<Tour>(_app.TourService.GetActiveTours(tourist));
            ShowCheckpointInfoCmd = new DelegateCommand(param => ShowCheckpointInfoExecute(), param => CanShowCheckpointInfoExecute());
            JoinCmd = new DelegateCommand(param => JoinExecute(), param => CanJoinExecute());
        }

        public ObservableCollection<Tour> Tours
        {
            get => _tours;
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
            get;
            set;
        }

        public string ActiveCheckpoint
        {
            get => _activeCheckpoint;
            set
            {
                if (value != _activeCheckpoint) 
                {
                    _activeCheckpoint = value;
                    OnPropertyChanged("ActiveCheckpoint");
                }
            }
        }

        public bool CanShowCheckpointInfoExecute()
        {
            return true;
        }

        public void ShowCheckpointInfoExecute() 
        {
            if(SelectedTour != null) 
            {
                Checkpoint checkpoint = _app.TourCheckpointService.GetLatestCheckpoint(SelectedTour);
                string name = checkpoint.AttractionName;
                string city = checkpoint.Location.City;
                string country = checkpoint.Location.Country;
                ActiveCheckpoint = "Checkpoint: " + name + ", " + city + ", " + country;
            }
        }

        public bool CanJoinExecute()
        {
            return true;
        }

        public void JoinExecute() 
        {
            if (SelectedTour != null) 
            {
                TourTourist tourTourist = _app.TourTouristService.FindByTourAndTouristID(SelectedTour.ID, _loggedInTourist.ID);
                tourTourist.Arrived = true;
                _app.TourTouristService.Update(tourTourist);
                MessageBox.Show("Successfully joined the tour.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void Update()
        {
            var tours = _app.TourService.GetActiveTours(_loggedInTourist);
            Tours.Clear();
            foreach(Tour tour in tours)
            {
                Tours.Add(tour);
            }
            ShowCheckpointInfoExecute();
        }
    }
}
