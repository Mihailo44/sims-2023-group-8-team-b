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
    public class TourReviewDisplayViewModel : ViewModelBase, ICloseable
    {

        private App _app;
        private Guide _loggedInGuide;
        private Tour _selectedTour;
        private ObservableCollection<GuideReview> _reviews;
        private Window _window;
        public DelegateCommand CloseCmd { get; }
        
        public TourReviewDisplayViewModel(Guide guide, Tour tour, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuide = guide;
            _selectedTour = tour;
            _window = window;
            GuideReviews = new ObservableCollection<GuideReview>(_app.GuideReviewService.GetReviewsForGuideTourID(guide.ID, tour.ID));
            StartEndTime = tour.StartDateTime.Hour + tour.StartDateTime.ToString("tt") + " - " + (tour.StartDateTime.Hour + tour.Duration).ToString() + tour.StartDateTime.ToString("tt");
            Capacity = tour.CurrentAttendants + "/" + tour.MaxAttendants;
        }

        public ObservableCollection<GuideReview> GuideReviews
        {
            get { return _reviews; }
            set
            {
                if(value != _reviews)
                {
                    _reviews = value;
                    OnPropertyChanged("Reviews");
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

        public string StartEndTime
        {
            get;
            set;
        }

        public string Capacity
        {
            get;
            set;
        }

    }
}
