using System.Windows;
using TouristAgency.Users;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for GuideProfileDisplay.xaml
    /// </summary>
    public partial class GuideProfileDisplay : Window
    {
        public GuideProfileDisplay(Guide guide)
        {
            InitializeComponent();
            DataContext = new GuideProfileDisplayViewModel(guide, this);
        }
    }
}
