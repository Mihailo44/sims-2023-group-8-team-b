using System.Windows;
using TouristAgency.Users;
using TouristAgency.Tours.DisplayFeature;

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
            DataContext = new TourDisplayViewModel(tourist, this);
        }
    }
}
