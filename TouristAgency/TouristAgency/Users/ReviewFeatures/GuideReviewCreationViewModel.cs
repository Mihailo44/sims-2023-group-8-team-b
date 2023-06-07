using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Review.Domain;
using TouristAgency.Tours;
using TouristAgency.Tours.DetailsFeature;
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
        private PhotoService _photoService;
        private GuideReviewService _guideReviewService;
        private List<string> _paths;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand LoadPhotoLinksCmd { get; set; }

        public DelegateCommand SetTourQualityCmd { get; set; }
        public DelegateCommand SetTourOrganizationCmd { get; set; }
        public DelegateCommand SetTourAttractionsCmd { get; set; }
        public DelegateCommand SetTourKnowledgeCmd { get; set; }
        public DelegateCommand SetTourLanguageCmd { get; set; }
        public DelegateCommand SetTourSocialCmd { get; set; }
        public DelegateCommand DetailsCmd { get; set; }

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
            _photoService = new PhotoService();
        }

        private void InstantiateCollections()
        {
            FinishedTours = new ObservableCollection<Tour>(_tourService.GetFinishedToursByTourist(_loggedInTourist));
            NewGuideReview = new GuideReview();
            _paths = new List<string>();
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
            DetailsCmd = new DelegateCommand(DetailsExecute, CanDetailsExecute);
            LoadPhotoLinksCmd = new DelegateCommand(param => LoadPhotoLinksExecute(), param => CanPhotoLinksExecute());
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
            if(SelectedTour != null)
            {
                AddPhotos();
                NewGuideReview.TouristID = _loggedInTourist.ID;
                NewGuideReview.Tourist = _loggedInTourist;
                NewGuideReview.TourID = SelectedTour.ID;
                NewGuideReview.Tour = SelectedTour;
                _guideReviewService.GuideReviewRepository.Create(NewGuideReview);
                MessageBox.Show("Successfully send a review.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a tour for reviewing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        public bool CanDetailsExecute(object param)
        {
            return true;
        }

        public void DetailsExecute(object param)
        {
            SelectedTour = FinishedTours.FirstOrDefault(t => t.ID == (int)param);
            if (SelectedTour != null)
            {
                TourDetailsDisplay display = new TourDetailsDisplay(SelectedTour);
                display.Show();
            }
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

        public bool CanPhotoLinksExecute()
        {
            return true;
        }

        public void LoadPhotoLinksExecute()
        {
            int guideReviewID = _guideReviewService.GuideReviewRepository.GenerateId() - 1;
            if (guideReviewID == -1)
                guideReviewID = 0;
            List<String> selectedPaths = _photoService.SelectPhotoPaths();
            _paths = _photoService.CopyToResourceDirectory(selectedPaths);
            foreach (String path in selectedPaths)
            {
                Photo photo = new Photo(path, 'G', guideReviewID);
                NewGuideReview.Photos.Add(photo);
                _app.PhotoRepository.Create(photo);
            }
            //AddPhotos(selectedPaths);
        }
    }
}
