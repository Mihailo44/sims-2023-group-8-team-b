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
        private string _username;
        private App _app;
        private Window _window;

        public DelegateCommand TourDisplayCmd { get; }
        public DelegateCommand TourGuideReviewCmd { get; }
        public DelegateCommand TourAttendanceCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public TouristHomeViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            Username = "Welcome, " + _loggedInTourist.Username + "...";

            foreach (var ttc in _app.TourTouristCheckpointService.GetPendingInvitations(tourist.ID))
            {
                MessageBoxResult result = MessageBox.Show("The guide has added you as present at the tour. Are you at: " + _app.CheckpointService.FindById(ttc.TourCheckpoint.CheckpointID).AttractionName + "?", "Question", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _app.TourTouristCheckpointService.AcceptInvitation(tourist.ID, ttc.TourCheckpoint.CheckpointID);
                }
            }

            TourDisplayCmd = new DelegateCommand(param => OpenTourDisplayCmdExecute(), param => CanOpenTourDisplayCmdExecute());
            TourGuideReviewCmd = new DelegateCommand(param => OpenTourGuideReviewCmdExecute(), param => CanOpenTourGuideReviewCmdExecute());
            TourAttendanceCmd = new DelegateCommand(param => OpenTourAttendanceCmdExecute(), param => CanOpenTourAttendanceCmdExecute());
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
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

        public bool CanOpenTourDisplayCmdExecute()
        {
            return true;
        }

        public void OpenTourDisplayCmdExecute()
        {
            TourDisplay display = new TourDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanOpenTourGuideReviewCmdExecute()
        {
            return true;
        }

        public void OpenTourGuideReviewCmdExecute()
        {
            GuideReviewCreation creation = new GuideReviewCreation(_loggedInTourist);
            creation.Show();
        }

        public bool CanOpenTourAttendanceCmdExecute()
        {
            return true;
        }

        public void OpenTourAttendanceCmdExecute()
        {
            TourAttendanceDisplay attendance = new TourAttendanceDisplay(_loggedInTourist);
            attendance.Show();
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }
    }
}
