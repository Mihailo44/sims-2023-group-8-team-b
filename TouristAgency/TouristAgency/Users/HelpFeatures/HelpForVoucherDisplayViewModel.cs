using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users;
using TouristAgency.View.Display;

namespace TouristAgency.Vouchers
{
    public class HelpForVoucherDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Tourist _loggedInTourist;
        private Window _window;

        public DelegateCommand VoucherNotificationCmd { get; set; }
        public DelegateCommand UseVoucherCmd { get; set; }

        public HelpForVoucherDisplayViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            VoucherNotificationCmd = new DelegateCommand(param => VoucherNotificationExecute(), param => CanVoucherNotificationExecute());
            UseVoucherCmd = new DelegateCommand(param => UseVoucherExecute(), param => CanUseVoucherExecute());
        }

        public bool CanVoucherNotificationExecute()
        {
            return true;
        }

        public void VoucherNotificationExecute()
        {
            NotificationDisplay display = new NotificationDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanUseVoucherExecute()
        {
            return true;
        }

        public void UseVoucherExecute() 
        {
            TourDisplay display = new TourDisplay(_loggedInTourist);
            display.Show();
        }
    }
}
