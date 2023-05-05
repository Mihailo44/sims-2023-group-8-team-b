using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
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

        private TourService _tourService;
        private GuideReviewService _guideReviewService;

        public DelegateCommand CreateCmd { get; set; }

        public GuideReviewCreationViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
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
