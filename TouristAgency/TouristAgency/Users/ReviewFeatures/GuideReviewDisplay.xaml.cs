using System.Windows;
using TouristAgency.Review;
using TouristAgency.Users;
using TouristAgency.Tours;
using TouristAgency.Review.GuideReviewDisplayFeature;

namespace TouristAgency.Review.GuideReviewDisplayFeature
{
    /// <summary>
    /// Interaction logic for TourReviewDisplay.xaml
    /// </summary>
    public partial class GuideReviewDisplay : Window
    {
        public GuideReviewDisplay(Guide guide, Tour tour)
        {
            InitializeComponent();
            DataContext = new GuideReviewDisplayViewModel(guide, tour, this);
        }
    }
}
