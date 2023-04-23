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
    public class GuideReviewDisplayViewModel : ViewModelBase, ICloseable, IObserver
    {

        private App _app;
        private Guide _loggedInGuide;
        private Tour _selectedTour;
        private ObservableCollection<GuideReview> _reviews;
        private GuideReview _review;
        private Window _window;
        public DelegateCommand CloseCmd { get; }
        public DelegateCommand MarkAsInvalidCmd { get; }
        public GuideReviewDisplayViewModel(Guide guide, Tour tour, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuide = guide;
            _selectedTour = tour;
            _window = window;
            //TODO REPOSITORY
            //GuideReviews = new ObservableCollection<GuideReview>(_app.GuideReviewService.GetReviewsForGuideTourID(guide.ID, tour.ID));
            StartEndTime = tour.StartDateTime.Hour + tour.StartDateTime.ToString("tt") + " - " + (tour.StartDateTime.AddHours(tour.Duration)).Hour.ToString() + tour.StartDateTime.ToString("tt");
            Capacity = tour.CurrentAttendants + "/" + tour.MaxAttendants;
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            MarkAsInvalidCmd = new DelegateCommand(param => MarkAsInvalidExecute(), param => CanMarkAsInvalidExecute());
            //_app.GuideReviewService.Subscribe(this);
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

        public GuideReview SelectedReview
        {
            get { return _review; }
            set
            {
                if(value != _review)
                {
                    _review = value;
                    OnPropertyChanged("SelectedReview");
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

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }

        public bool CanMarkAsInvalidExecute()
        {
            return true;
        }

        public void MarkAsInvalidExecute()
        {
            if(SelectedReview != null)
            {
                SelectedReview.IsInvalid = true;
                //TODO REPOSITORY
                //_app.GuideReviewService.Update(SelectedReview, SelectedReview.ID);
            }
        }

        public void Update()
        {
            GuideReviews.Clear();
            //TODO REPOSITORY
            /*ObservableCollection<GuideReview> temp = new ObservableCollection<GuideReview>(_app.GuideReviewService.GetReviewsForGuideTourID(_loggedInGuide.ID, _selectedTour.ID));
            foreach(var GuideReview in temp)
            {
                GuideReviews.Add(GuideReview);
            }*/
        }
    }
}
