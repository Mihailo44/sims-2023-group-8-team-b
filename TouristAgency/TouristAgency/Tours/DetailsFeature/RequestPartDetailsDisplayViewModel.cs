using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;

namespace TouristAgency.Tours.DetailsFeature
{
    public class RequestPartDetailsDisplayViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Window _window;
        private TourRequest _tourRequest;

        private string _name;

        public DelegateCommand CloseCmd { get; set; }

        public RequestPartDetailsDisplayViewModel(string name, TourRequest tourRequest, Window window)
        {
            _app = (App)Application.Current;
            _window = window;
            TourRequest = tourRequest;
            _name = name;
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public TourRequest TourRequest
        {
            get => _tourRequest;
            set
            {
                if (_tourRequest != value)
                {
                    _tourRequest = value;
                    OnPropertyChanged("TourRequest");
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
