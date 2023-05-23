using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;

namespace TouristAgency.Tours.DetailsFeature
{
    public class TourDetailsDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Window _window;
        private Tourist _loggedInTourist;
        private Tour _tour;

        public DelegateCommand CloseCmd { get; set; }

        public TourDetailsDisplayViewModel(Tour tour, Window window)
        {
            _app = (App)Application.Current;
            _window = window;
            _tour = tour;

            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public Tour Tour
        {
            get => _tour;
            set
            {
                if (_tour != value) 
                {
                    _tour = value;
                    OnPropertyChanged("Tour");
                }
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
