using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.PostponementFeatures;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users.ReviewFeatures;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.Users.HomeDisplayFeature
{
    public class GuestHomeViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Guest _loggedInGuest;
        private Window _window;
        private string _username;
        private string _welcomeUsername;

        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }

        public GuestHomeViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = window;

            InstantiateCommands();
            ShowUser();
            WelcomeUser();
        }

        private void InstantiateCommands()
        {
            AccommodationDisplayCmd = new DelegateCommand(param => OpenAccommodationDisplayCmdExecute(),
                param => CanOpenAccommodationDisplayCmdExecute());
            PostponementRequestDisplayCmd = new DelegateCommand(param => OpenPostponementRequestDisplayCmdExecute(),
                param => CanOpenPostponementRequestDisplayCmdExecute());
            OwnerReviewCreationCmd = new DelegateCommand(param => OpenOwnerReviewCreationCmdExecute(),
                param => CanOpenOwnerReviewCreationCmdExecute());
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            SuperGuestDisplayCmd = new DelegateCommand(param => OpenSuperGuestDisplayCmdExecute(), param => CanOpenSuperGuestDisplayCmdExecute());
            HomeCmd = new DelegateCommand(param => OpenHomeCmdExecute(), param => CanOpenHomeCmdExecute());
        }

        private void ShowUser()
        {
            Username = "Username: " + _loggedInGuest.Username;
        }

        private void WelcomeUser()
        {
            WelcomeUsername = "Welcome " + _loggedInGuest.Username + "!!!";
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

        public string WelcomeUsername
        {
            get => _welcomeUsername;
            set
            {
                if (value != _welcomeUsername)
                {
                    _welcomeUsername = value;
                    OnPropertyChanged("WelcomeUsername");
                }
            }
        }
        public bool CanOpenAccommodationDisplayCmdExecute()
        {
            return true;
        }

        public void OpenAccommodationDisplayCmdExecute()
        {
            _app.CurrentVM = new ReservationCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenPostponementRequestDisplayCmdExecute()
        {
            return true;
        }

        public void OpenPostponementRequestDisplayCmdExecute()
        {
           _app.CurrentVM = new PostponementRequestCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenOwnerReviewCreationCmdExecute()
        {
            return true;
        }

        public void OpenOwnerReviewCreationCmdExecute()
        {
            _app.CurrentVM = new OwnerReviewCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenSuperGuestDisplayCmdExecute()
        {
            return true;
        }

        public void OpenSuperGuestDisplayCmdExecute()
        {
            _app.CurrentVM = new SuperGuestDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenHomeCmdExecute()
        {
            return true;
        }

        public void OpenHomeCmdExecute()
        {
           _app.CurrentVM = new GuestHomeViewModel(_loggedInGuest, _window);
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
