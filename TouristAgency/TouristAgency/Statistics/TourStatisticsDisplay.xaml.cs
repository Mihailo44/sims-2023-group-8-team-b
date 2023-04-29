using System.Windows;
using TouristAgency.Users;
using TouristAgency.Statistics;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourStatisticsDisplay.xaml
    /// </summary>
    public partial class TourStatisticsDisplay : Window
    {
        public TourStatisticsDisplay(Guide guide)
        {
            InitializeComponent();
            DataContext = new TourStatisticsDisplayViewModel(guide);
        }
    }
}
