﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.PostponementFeatures;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.ReportFeature;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users.ForumFeatures.DisplayFeature;
using TouristAgency.Users.ReviewFeatures;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.Users.HomeDisplayFeature
{
    public class GuestHomeViewModel : HelpMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guest _loggedInGuest;
        private Window _window;
        private string _username;
        private string _welcomeUsername;
        private string _text;
        private int _progressValue;
        private string _isVisible;
        private bool _isEnabled;

        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand AnywhereAnytimeCreationCmd { get; set; }
        public DelegateCommand ForumDisplayCmd { get; set; }
        public DelegateCommand GuestReportDisplayCmd { get; set; }
        public DelegateCommand NextCmd { get; set; }
        public DelegateCommand SkipCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }

        public GuestHomeViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuestHome");
            IsEnabled = true;
            ProgressValue = 1;
            Text = "Click the button from the menu for the option you want.";

            InstantiateCommands();
            InstantiateHelpMenuCommands();
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
            GuestReviewDisplayCmd = new DelegateCommand(param => OpenGuestReviewDisplayCmdExecute(), param => CanOpenGuestReviewDisplayCmdExecute());
            AnywhereAnytimeCreationCmd = new DelegateCommand(param => OpenAnywhereAnytimeCreationCmdExecute(), param => CanOpenAnywhereAnytimeCreationCmdExecute());
            ForumDisplayCmd = new DelegateCommand(param => OpenForumDisplayCmdExecute(), param => CanOpenForumDisplayCmdExecute());
            GuestReportDisplayCmd = new DelegateCommand(param => OpenGuestReportDisplayCmdExecute(), param =>  CanOpenGuestReportDisplayCmdExecute());
            NextCmd = new DelegateCommand(param =>  NextCmdExecute(), param => CanNextCmdExecute());
            SkipCmd = new DelegateCommand(param => SkipCmdExecute(), param => CanSkipCmdExecute());
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

        public string Text
        {
            get => _text;
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                if (value != _progressValue)
                {
                    _progressValue = value;
                    OnPropertyChanged("ProgressValue");
                }
            }
        }

        public string IsVisible
        {
            get => _isVisible;
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    OnPropertyChanged("IsEnabled");
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

        public bool CanOpenGuestReviewDisplayCmdExecute()
        {
            return true;
        }

        public void OpenGuestReviewDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestReviewDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenAnywhereAnytimeCreationCmdExecute()
        {
            return true;
        }

        public void OpenAnywhereAnytimeCreationCmdExecute()
        {
            _app.CurrentVM = new AnywhereAnytimeCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenForumDisplayCmdExecute()
        {
            return true;
        }

        public void OpenForumDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestForumDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenGuestReportDisplayCmdExecute()
        {
            return true;
        }

        public void OpenGuestReportDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestReportDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenHomeCmdExecute()
        {
            return true;
        }

        public void OpenHomeCmdExecute()
        {
           _app.CurrentVM = new GuestHomeViewModel(_loggedInGuest, _window);
        }

        public bool CanNextCmdExecute()
        {
            return true;
        }

        public void NextCmdExecute()
        {
            if(ProgressValue == 1)
            {
                ProgressValue = 2;
                Text = "If you find it hard to do something, you can always click the help button which is located on the left in every section from the menu.";
            }
            else if(ProgressValue == 2)
            {
                ProgressValue = 3;
                Text = "You won't be able to see your ratings from the owner until you rate the owner and the accommodation in the section Review and recommendation.";
                IsEnabled = false;
            }
        }

        public bool CanSkipCmdExecute()
        {
            return true;
        }

        public void SkipCmdExecute()
        {
            IsVisible = "Hidden";
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
