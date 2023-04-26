    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.ViewModel
{
    public class TouristHomeViewModel : ViewModelBase, ICloseable
    {
        private Tourist _loggedInTourist;
        private TourTouristCheckpointService _ttcService;
        private CheckpointService _checkpointService;
        private string _username;
        private App _app;
        private Window _window;

        public DelegateCommand TourDisplayCmd { get; }
        public DelegateCommand TourGuideReviewCmd { get; }
        public DelegateCommand TourAttendanceCmd { get; }
        public DelegateCommand NotificationCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public TouristHomeViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            Username = "Welcome, " + _loggedInTourist.Username + "...";
            _ttcService = new TourTouristCheckpointService();
            _checkpointService = new CheckpointService();

            foreach (var ttc in _ttcService.GetPendingInvitations(tourist.ID))
            {
                MessageBoxResult result = MessageBox.Show("The guide has added you as present at the tour. Are you at: " + _checkpointService.CheckpointRepository.GetById(ttc.TourCheckpoint.CheckpointID).AttractionName + "?", "Question", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _ttcService.AcceptInvitation(tourist.ID, ttc.TourCheckpoint.CheckpointID);
                }
            }

            TourDisplayCmd = new DelegateCommand(param => TourDisplayExecute(), param => CanTourDisplayExecute());
            TourGuideReviewCmd = new DelegateCommand(param => TourGuideReviewExecute(), param => CanTourGuideReviewExecute());
            TourAttendanceCmd = new DelegateCommand(param => TourAttendanceExecute(), param => CanTourAttendanceExecute());
            NotificationCmd = new DelegateCommand(param => NotificationExecute(), param => CanNotificationExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
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
