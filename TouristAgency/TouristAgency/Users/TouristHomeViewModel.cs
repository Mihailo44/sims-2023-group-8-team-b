using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Statistics;
using TouristAgency.TourRequests;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;
using TouristAgency.Vouchers;

namespace TouristAgency.Users
{
    public class TouristHomeViewModel : ViewModelBase, ICloseable
    {
        private Tourist _loggedInTourist;
        private App _app;

        private string _username;
        private Window _window;

        private TourTouristCheckpointService _ttcService;
        private CheckpointService _checkpointService;

        public DelegateCommand TourDisplayCmd { get; set; }
        public DelegateCommand TourGuideReviewCmd { get; set; }
        public DelegateCommand TourAttendanceCmd { get; set; }
        public DelegateCommand NotificationCmd { get; set; }
        public DelegateCommand TourRequestCmd { get; set; }
        public DelegateCommand TourRequestStatisticsCmd { get; set; }
        public DelegateCommand HelpForVoucherCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }

        public TouristHomeViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            InstantiateServices();
            InstantiateCommands();
            NotifyUser();
        }

        private void InstantiateServices()
        {
            _ttcService = new TourTouristCheckpointService();
            _checkpointService = new CheckpointService();
        }

        private void InstantiateCommands()
        {
            TourDisplayCmd = new DelegateCommand(param => TourDisplayExecute(), param => CanTourDisplayExecute());
            TourGuideReviewCmd = new DelegateCommand(param => TourGuideReviewExecute(), param => CanTourGuideReviewExecute());
            TourAttendanceCmd = new DelegateCommand(param => TourAttendanceExecute(), param => CanTourAttendanceExecute());
            NotificationCmd = new DelegateCommand(param => NotificationExecute(), param => CanNotificationExecute());
            TourRequestCmd = new DelegateCommand(param => TourRequestExecute(), param => CanTourRequestExecute());
            TourRequestStatisticsCmd = new DelegateCommand(param => TourRequestStatisticsExecute(), param => CanTourRequestStatisticsExecute());
            HelpForVoucherCmd = new DelegateCommand(param => HelpForVoucherExecute(), param => CanHelpForVoucherExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        private void NotifyUser()
        {
            Username = "Welcome, " + _loggedInTourist.Username + "...";

            foreach (var ttc in _ttcService.GetPendingInvitations(_loggedInTourist.ID))
            {
                MessageBoxResult result = MessageBox.Show("The guide has added you as present at the tour. Are you at: " + _checkpointService.CheckpointRepository.GetById(ttc.TourCheckpoint.CheckpointID).AttractionName + "?", "Question", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _ttcService.AcceptInvitation(_loggedInTourist.ID, ttc.TourCheckpoint.CheckpointID);
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public bool CanTourDisplayExecute()
        {
            return true;
        }

        public void TourDisplayExecute()
        {
            TourDisplay display = new TourDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanTourGuideReviewExecute()
        {
            return true;
        }

        public void TourGuideReviewExecute()
        {
            GuideReviewCreation creation = new GuideReviewCreation(_loggedInTourist);
            creation.Show();
        }

        public bool CanTourAttendanceExecute()
        {
            return true;
        }

        public void TourAttendanceExecute()
        {
            TourAttendanceDisplay attendance = new TourAttendanceDisplay(_loggedInTourist);
            attendance.Show();
        }

        public bool CanNotificationExecute()
        {
            return true;
        }

        public void NotificationExecute()
        {
            NotificationDisplay display = new NotificationDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanTourRequestExecute()
        {
            return true;
        }

        public void TourRequestExecute()
        {
            TourRequestCreation creation = new TourRequestCreation(_loggedInTourist);
            creation.Show();
        }

        public bool CanTourRequestStatisticsExecute()
        {
            return true;
        }

        public void TourRequestStatisticsExecute()
        {
            TourRequestStatisticsDisplay display = new TourRequestStatisticsDisplay();
            display.Show();
        }

        public bool CanHelpForVoucherExecute()
        {
            return true;
        }

        public void HelpForVoucherExecute()
        {
            HelpForVouchersDisplay display = new HelpForVouchersDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }
    }
}
