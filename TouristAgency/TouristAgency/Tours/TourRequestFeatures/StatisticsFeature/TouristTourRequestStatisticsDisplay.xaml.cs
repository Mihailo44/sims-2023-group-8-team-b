using System.Windows;

namespace TouristAgency.Statistics
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsDisplay.xaml
    /// </summary>
    public partial class TourRequestStatisticsDisplay : Window
    {
        public TourRequestStatisticsDisplay()
        {
            InitializeComponent();
            DataContext = new TourRequestStatisticsDisplayViewModel();
        }
    }
}
