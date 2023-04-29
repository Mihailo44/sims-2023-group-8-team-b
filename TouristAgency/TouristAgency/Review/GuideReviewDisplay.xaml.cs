using System.Windows;
using TouristAgency.Review;
using TouristAgency.Users;
using TouristAgency.Tours;

namespace TouristAgency.View.Display
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
