using System.Windows;
using TouristAgency.Users;
using TouristAgency.Vouchers;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for ChooseVoucherDisplay.xaml
    /// </summary>
    public partial class ChooseVoucherDisplay : Window
    {
        public ChooseVoucherDisplay(Tourist tourist, int tourID)
        {
            InitializeComponent();
            DataContext = new ChooseVoucherDisplayViewModel(tourist, tourID, this);
        }
    }
}
