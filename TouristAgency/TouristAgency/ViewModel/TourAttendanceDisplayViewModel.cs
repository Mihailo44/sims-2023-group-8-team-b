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
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class TourAttendanceDisplayViewModel : ViewModelBase, IObserver
    {
        private ObservableCollection<Tour> _tours;
        private Tourist _loggedInTourist;
        private TourService _tourService;
        private TouristService _touristService;
        private TourCheckpointService _tourCheckpointService;
        private TourTouristService _tourTouristService;
        private string _activeCheckpoint;
        private App _app;

        public DelegateCommand ShowCheckpointInfoCmd { get; }
        public DelegateCommand JoinCmd { get; }

        public TourAttendanceDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _tourService = new TourService();
            _touristService = new TouristService();
            _tourTouristService = new TourTouristService();
            _tourCheckpointService = new TourCheckpointService();
            Tours = new ObservableCollection<Tour>(_tourService.GetActiveTours(tourist));
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
                Checkpoint checkpoint = _tourCheckpointService.GetLatestCheckpoint(SelectedTour);
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
                TourTourist tourTourist = _tourTouristService.TourTouristRepository.GetByTourAndTouristID(SelectedTour.ID, _loggedInTourist.ID);
                tourTourist.Arrived = true;
                _app.TourTouristRepository.Update(tourTourist);
                MessageBox.Show("Successfully joined the tour.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
        }

        public void Update()
        {
            List<Tour> tours = _tourService.GetActiveTours(_loggedInTourist);
            Tours.Clear();
            foreach(Tour tour in tours)
            {
                Tours.Add(tour);
            }
            ShowCheckpointInfoExecute();
        }
    }
}
