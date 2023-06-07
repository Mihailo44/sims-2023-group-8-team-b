using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Review.Domain;
using TouristAgency.Tours;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Users.ReviewFeatures
{
    public class GudeReviewDetailsDisplayViewModel : BurgerMenuViewModelBase
    {
        private App _app;
        private Guide _loggedInGuide;

        private string _image1;
        private string _image2;
        private string _image3;
        private ObservableCollection<GuideReview> _reviews;
        private Tour _selectedTour;
        private GuideReview _review;

        private GuideReviewService _guideReviewService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand MarkAsInvalidCmd { get; set; }

        public GudeReviewDetailsDisplayViewModel(GuideReview review)
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            SelectedReview = review;
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
            if (SelectedReview.Photos.Count == 0)
            {
                Image1 = "..\\..\\..\\Resources\\Image\\none.jpeg";
                Image2 = "..\\..\\..\\Resources\\Image\\none.jpeg";
                Image3 = "..\\..\\..\\Resources\\Image\\none.jpeg";
            }
            if (SelectedReview.Photos.Count == 1)
            {
                Image1 = SelectedReview.Photos[0].Link;
                Image2 = "..\\..\\..\\Resources\\Image\\none.jpeg";
                Image3 = "..\\..\\..\\Resources\\Image\\none.jpeg";
            }
            else if (SelectedReview.Photos.Count == 2)
            {
                Image1 = SelectedReview.Photos[0].Link;
                Image2 = SelectedReview.Photos[1].Link;
                Image3 = "..\\..\\..\\Resources\\Image\\none.jpeg";
            }
            else if (SelectedReview.Photos.Count >= 3)
            {
                Image1 = SelectedReview.Photos[0].Link;
                Image2 = SelectedReview.Photos[1].Link;
                Image2 = SelectedReview.Photos[2].Link;
            }
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            MarkAsInvalidCmd = new DelegateCommand(param => MarkAsInvalidExecute(), param => CanMarkAsInvalidExecute());
        }

        public string Image1
        {
            get => _image1;
            set
            {
                if (value != _image1)
                {
                    _image1 = value;
                    OnPropertyChanged("Image1");
                }
            }
        }

        public string Image2
        {
            get => _image2;
            set
            {
                if (value != _image2)
                {
                    _image2 = value;
                    OnPropertyChanged("Image2");
                }
            }
        }

        public string Image3
        {
            get => _image3;
            set
            {
                if (value != _image3)
                {
                    _image3 = value;
                    OnPropertyChanged("Image3");
                }
            }
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
