using System.Windows;
using TouristAgency.Users;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for GuideHome.xaml
    /// </summary>
    public partial class GuideHome : Window
    {
        public GuideHome(Guide guide)
        {
            InitializeComponent();
            DataContext = new GuideHomeViewModel(guide, this);
        }
    }
}
