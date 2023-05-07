using System.Windows;
using TouristAgency.Users;

namespace TouristAgency.Vouchers
{
    public class HelpForVoucherDisplayViewModel
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Window _window;

        public HelpForVoucherDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
        }
    }
}
