using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.RenovationFeatures.RenovationHistoryFeature;
using TouristAgency.Accommodations.RenovationFeatures.RenovationSchedulingFeature;
using TouristAgency.Accommodations.StatisticsFeature;
using TouristAgency.Base;

namespace TouristAgency.Accommodations.NavigationWindow
{
    public class AccommodationMainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private AccommodationRenovationViewModel _accommodationRenovationViewModel;
        private RenovationHistoryViewModel _renovationHistoryViewModel;
        private AccommodationStatisticsViewModel _accommodationStatisticsViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }
        public DelegateCommand NavCmd { get; set; }

        public AccommodationMainViewModel(Accommodation accommodation)
        {
            _accommodationRenovationViewModel = new(accommodation);
            _renovationHistoryViewModel = new(accommodation);
            _accommodationStatisticsViewModel = new(accommodation);
            CurrentViewModel = new AccommodationStatisticsViewModel(accommodation);
            NavCmd = new DelegateCommand(NavCmdExecute, CanNavCmdExecute);
        }

        public bool CanNavCmdExecute(object parameter)
        {
            if (parameter == null || !int.TryParse(parameter.ToString(), out int index))
            {
                return false;
            }

            return index >= 0 && index <= 2;
        }

        public void NavCmdExecute(object parameter)
        {
            int index = int.Parse(parameter.ToString());
            switch (index)
            {
                case 0:
                    CurrentViewModel = _accommodationStatisticsViewModel;
                    break;
                case 1:
                    CurrentViewModel = _accommodationRenovationViewModel;
                    break;
                case 2:
                    CurrentViewModel = _renovationHistoryViewModel;
                    break;
            }
        }
    }
}
