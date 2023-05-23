using System.Windows;
using TouristAgency.Users;
using TouristAgency.Tours.DisplayFeature;
using TouristAgency.Tours;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourDisplay.xaml
    /// </summary>
    public partial class TourDisplay : Window
    {
        public TourDisplay(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new TourDisplayViewModel(tourist);
        }

        public TourDisplay(Tourist tourist, Tour tour)
        {
            InitializeComponent();
            DataContext = new TourDisplayViewModel(tourist, tour);
        }
    }
}
