using System.Windows;
using TouristAgency.Users;
using TouristAgency.Tours;
using TouristAgency.Tours.DisplayFeature;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourAttendance.xaml
    /// </summary>
    public partial class TourAttendanceDisplay : Window
    {
        public TourAttendanceDisplay(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new TourAttendanceDisplayViewModel(tourist, this);
        }
    }
}
