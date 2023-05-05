using System.Windows;
using TouristAgency.Users;
using TouristAgency.Vouchers;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for NotificationDisplay.xaml
    /// </summary>
    public partial class NotificationDisplay : Window
    {
        public NotificationDisplay(Tourist tourist)
        {
            InitializeComponent();

            DataContext = new NotificationDisplayViewModel(tourist, this);
        }
    }
}
