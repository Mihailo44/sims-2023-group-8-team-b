using System.Windows;
using TouristAgency.Users;
using TouristAgency.Reservations;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for ActiveTourDisplay.xaml
    /// </summary>
    public partial class ActiveTourDisplay : Window
    {

        public ActiveTourDisplay(Guide guide)
        {
            InitializeComponent();
            DataContext = new ActiveTourDisplayViewModel(guide);

        }
    }
}
