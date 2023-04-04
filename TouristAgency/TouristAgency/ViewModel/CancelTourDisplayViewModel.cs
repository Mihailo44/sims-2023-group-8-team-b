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
    public class CancelTourDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Guide _guide;
        private ObservableCollection<Tour> _availableTours;
        private Tour _selectedTour;
        public CancelTourDisplayViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;
            _guide = guide;
            AvailableTours = new ObservableCollection<Tour>(_app.TourViewModel.GetCancellabeTours());
        }

        public ObservableCollection<Tour> AvailableTours
        {
            get => _availableTours;
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("AvailableTours");
                }
            }
        }
    }
}
