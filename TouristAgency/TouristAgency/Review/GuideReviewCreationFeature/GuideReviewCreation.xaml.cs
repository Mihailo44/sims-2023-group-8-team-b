using System.Windows;
using TouristAgency.Review;
using TouristAgency.Users;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for TourGuideReviewCreation.xaml
    /// </summary>
    public partial class GuideReviewCreation : Window
    {
        public GuideReviewCreation(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new GuideReviewCreationViewModel(tourist, this);
        }
    }
}
