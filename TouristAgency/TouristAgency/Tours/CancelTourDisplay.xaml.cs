using System.Windows;
using TouristAgency.Users;
using TouristAgency.Tours;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for CancelTourDisplay.xaml
    /// </summary>
    public partial class CancelTourDisplay : Window
    {
        public CancelTourDisplay(Guide guide)
        {
            InitializeComponent();
            DataContext = new CancelTourDisplayViewModel(guide);
        }
    }
}
