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
    public class GuideReviewCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;
        private ObservableCollection<Tour> _finishedTours;

        public DelegateCommand CreateCmd { get; }

        public GuideReviewCreationViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;

            _loggedInTourist = tourist;
            FinishedTours =  new ObservableCollection<Tour>(_app.TourService.GetFinishedTours(tourist));
            NewGuideReview = new GuideReview();
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
        }

        public ObservableCollection<Tour> FinishedTours
        {
            get { return _finishedTours; }
            set 
            { 
                if(value != _finishedTours) 
                {
                    _finishedTours = value;
                    OnPropertyChanged("FinishedTours");
                }
            }
        }

        public Tour SelectedTour
        {
            get; 
            set;
        }

        public GuideReview NewGuideReview
        {
            get;
            set;
        }

        public bool CanCreateExecute()
        {
            return true;
        }

        public void CreateExecute()
        {
            _app.GuideReviewService.Create(NewGuideReview);
            MessageBox.Show("Successfully send a review.", "Success");
        }
    }
}
