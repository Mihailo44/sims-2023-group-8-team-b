using System.Windows;
using TouristAgency.Users;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for TouristHome.xaml
    /// </summary>
    public partial class TouristHome : Window
    {
        public TouristHome(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new TouristHomeViewModel(tourist, this);
        }
    }
}
