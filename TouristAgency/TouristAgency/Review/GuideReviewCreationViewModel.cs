using System;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Review.Domain;
using TouristAgency.Tours;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Review
{
    public class GuideReviewCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;

        private ObservableCollection<Tour> _finishedTours;
        private string _tourQuality;

        private TourService _tourService;
        private GuideReviewService _guideReviewService;

        public DelegateCommand CreateCmd { get; set; }

        public DelegateCommand SetTourQualityCmd { get; set; }
        public DelegateCommand SetTourOrganizationCmd { get; set; }
        public DelegateCommand SetTourAttractionsCmd { get; set; }
        public DelegateCommand SetTourKnowledgeCmd { get; set; }
        public DelegateCommand SetTourLanguageCmd { get; set; }
        public DelegateCommand SetTourSocialCmd { get; set; }

        public GuideReviewCreationViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _guideReviewService = new GuideReviewService();
        }

        private void InstantiateCollections()
        {
            FinishedTours = new ObservableCollection<Tour>(_tourService.GetFinishedToursByTourist(_loggedInTourist));
            NewGuideReview = new GuideReview();
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
            SetTourQualityCmd = new DelegateCommand(SetTourQualityExecute, CanSetTourQualityExecute);
            SetTourOrganizationCmd = new DelegateCommand(SetTourOrganizationExecute, CanSetTourOrganizationExecute);
            SetTourAttractionsCmd = new DelegateCommand(SetTourAttractionsExecute, CanSetTourAttractionsExecute);
            SetTourKnowledgeCmd = new DelegateCommand(SetTourKnowledgeExecute, CanSetTourKnowledgeExecute);
            SetTourLanguageCmd = new DelegateCommand(SetTourLanguageExecute, CanSetTourLanguageExecute);
            SetTourSocialCmd = new DelegateCommand(SetTourSocialExecute, CanSetTourSocialExecute);
        }

        public ObservableCollection<Tour> FinishedTours
        {
            get { return _finishedTours; }
            set
            {
                if (value != _finishedTours)
                {
                    _finishedTours = value;
                    OnPropertyChanged("FinishedTours");
                }
            }
        }

        public string TourQuality
        {
            get => _tourQuality;
            set
            {
                if(value != _tourQuality)
                {
                    _tourQuality = value;
                    OnPropertyChanged("TourQuality");
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

        public string PhotoLinks
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
            AddPhotos();
            NewGuideReview.TouristID = _loggedInTourist.ID;
            NewGuideReview.Tourist = _loggedInTourist;
            NewGuideReview.TourID = SelectedTour.ID;
            NewGuideReview.Tour = SelectedTour;
            _guideReviewService.GuideReviewRepository.Create(NewGuideReview);
            MessageBox.Show("Successfully send a review.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool CanSetTourQualityExecute(object parameter)
        {
            return true;
        }

        public void SetTourQualityExecute(object parameter)
        {
            NewGuideReview.Quality = Convert.ToInt32(parameter);
        }

        public bool CanSetTourOrganizationExecute(object parameter)
        {
            return true;
        }

        public void SetTourOrganizationExecute(object parameter)
        {
            NewGuideReview.TourOrganization = Convert.ToInt32(parameter);
        }

        public bool CanSetTourAttractionsExecute(object parameter)
        {
            return true;
        }

        public void SetTourAttractionsExecute(object parameter)
        {
            NewGuideReview.Attractions = Convert.ToInt32(parameter);
        }

        public bool CanSetTourKnowledgeExecute(object parameter)
        {
            return true;
        }

        public void SetTourKnowledgeExecute(object parameter)
        {
            NewGuideReview.Knowledge = Convert.ToInt32(parameter);
        }

        public bool CanSetTourLanguageExecute(object parameter)
        {
            return true;
        }

        public void SetTourLanguageExecute(object parameter)
        {
            NewGuideReview.Language = Convert.ToInt32(parameter);
        }

        public bool CanSetTourSocialExecute(object parameter)
        {
            return true;
        }

        public void SetTourSocialExecute(object parameter)
        {
            NewGuideReview.SocialInteraction = Convert.ToInt32(parameter);
        }
        public void AddPhotos()
        {
            int guideReviewID = _guideReviewService.GuideReviewRepository.GenerateId() - 1;
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'G', guideReviewID);
                    NewGuideReview.Photos.Add(photo);
                    _app.PhotoRepository.Create(photo);
                }
            }
        }
    }
}
