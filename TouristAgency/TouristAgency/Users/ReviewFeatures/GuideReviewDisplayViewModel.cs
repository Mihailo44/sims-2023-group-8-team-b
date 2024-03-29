﻿using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.Tours;
using TouristAgency.Review.Domain;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.ReviewFeatures;

namespace TouristAgency.Review.GuideReviewDisplayFeature
{
    public class GuideReviewDisplayViewModel : BurgerMenuViewModelBase, ICloseable, IObserver
    {

        private App _app;
        private Guide _loggedInGuide;

        private ObservableCollection<GuideReview> _reviews;
        private Tour _selectedTour;
        private GuideReview _review;

        private GuideReviewService _guideReviewService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand MarkAsInvalidCmd { get; set; }
        public DelegateCommand DetailsCmd { get; set; }
        public GuideReviewDisplayViewModel(Tour tour)
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            _selectedTour = tour;
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
            //_app.GuideReviewRepository.Subscribe(this);
        }

        private void InstantiateServices()
        {
            _guideReviewService = new GuideReviewService();
        }

        private void InstantiateCollections()
        {
            GuideReviews = new ObservableCollection<GuideReview>(_guideReviewService.GetReviewsForGuideTourID(_loggedInGuide.ID, SelectedTour.ID));
            StartEndTime = SelectedTour.StartDateTime.Hour + SelectedTour.StartDateTime.ToString("tt") + " - " + SelectedTour.StartDateTime.AddHours(SelectedTour.Duration).Hour.ToString() + SelectedTour.StartDateTime.ToString("tt");
            Capacity = SelectedTour.CurrentAttendants + "/" + SelectedTour.MaxAttendants;
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            MarkAsInvalidCmd = new DelegateCommand(param => MarkAsInvalidExecute(), param => CanMarkAsInvalidExecute());
            DetailsCmd = new DelegateCommand(param =>  DetailsExecute(), param => CanDetailsExecute());
        }

        public ObservableCollection<GuideReview> GuideReviews
        {
            get { return _reviews; }
            set
            {
                if (value != _reviews)
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
                if (value != _review)
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
                if (value != _selectedTour)
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
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public bool CanMarkAsInvalidExecute()
        {
            return true;
        }

        public void MarkAsInvalidExecute()
        {
            if (SelectedReview != null)
            {
                SelectedReview.IsInvalid = true;
                _guideReviewService.GuideReviewRepository.Update(SelectedReview, SelectedReview.ID);
            }
        }

        public bool CanDetailsExecute()
        {
            return true;
        }

        public void DetailsExecute()
        {
            _app.CurrentVM = new GudeReviewDetailsDisplayViewModel(SelectedReview);
        }

        public void Update()
        {
            GuideReviews.Clear();
            ObservableCollection<GuideReview> temp = new ObservableCollection<GuideReview>(_guideReviewService.GetReviewsForGuideTourID(_loggedInGuide.ID, _selectedTour.ID));
            foreach (var GuideReview in temp)
            {
                GuideReviews.Add(GuideReview);
            }
        }
    }
}
