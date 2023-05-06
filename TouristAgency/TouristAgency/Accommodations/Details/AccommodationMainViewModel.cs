using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.AccommodationRenovation.SchedulingFeature;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Base;

namespace TouristAgency.Accommodations.Details
{
    public class AccommodationMainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private AccommodationRenovationViewModel accommodationRenovationViewModel;

        public ViewModelBase CurrentViewModel 
        { 
            get => _currentViewModel;
            set
            {
                if(_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel)); // mozda nece raditi
                }
            }
        }
        public DelegateCommand NavCmd { get; set; }

        public AccommodationMainViewModel(Accommodation accommodation)
        {
            accommodationRenovationViewModel = new(accommodation);
            CurrentViewModel = new AccommodationRenovationViewModel(accommodation);
            NavCmd = new DelegateCommand(NavCmdExecute,CanNavCmdExecute);
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
                case 1:
                    CurrentViewModel = accommodationRenovationViewModel;
                    break;
            }
        }
    }
}
