﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.ReportFeature;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users.Domain;
using TouristAgency.Users.ForumFeatures.Domain;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.ReviewFeatures;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.Users.SuperGuestFeature.Domain;
using TouristAgency.Util;

namespace TouristAgency.Users.ForumFeatures.DisplayFeature
{
    public class GuestForumDisplayViewModel : HelpMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guest _loggedInGuest;
        private Window _window;
        private string _username;
        private string _welcomeUsername;
        private string _city;
        private string _comment;
        private Forum _newForum;
        private ForumComment _newComment;
        private ObservableCollection<string> _cities;
        private ObservableCollection<Forum> _forums;
        private ObservableCollection<ForumComment> _comments;

        private LocationService _locationService;
        private ForumService _forumService;
        private ForumCommentService _forumCommentService;
        private ReservationService _reservationService;

        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand AnywhereAnytimeCreationCmd { get; set; }
        public DelegateCommand ForumDisplayCmd { get; set; }
        public DelegateCommand CreateForumCmd { get; set; }
        public DelegateCommand CreateCommentCmd { get; set; }
        public DelegateCommand OpenForumCmd { get; set; }
        public DelegateCommand CloseForumCmd { get; set; }
        public DelegateCommand GuestReportDisplayCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }

        public GuestForumDisplayViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            User = guest;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuestHome");
            NewComment = new ForumComment();
            

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateHelpMenuCommands();
            ShowUser();
            WelcomeUser();
            _forumCommentService.MarkAsSuper(_reservationService.GetAll());
        }

        private void InstantiateServices()
        {
            _locationService = new LocationService();
            _forumService = new ForumService();
            _forumCommentService = new ForumCommentService();
            _reservationService = new ReservationService();
        }
        private void InstantiateCollections()
        {
            Cities = new ObservableCollection<string>(_locationService.GetCities());
            Forums = new ObservableCollection<Forum>(_forumService.GetAll());
            _comments = new ObservableCollection<ForumComment>();
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
            CreateForumCmd = new DelegateCommand(param =>  CreateForumCmdExecute(), param => CanCreateForumCmdExecute());
            OpenForumCmd = new DelegateCommand(param => OpenForumCmdExecute(), param => CanOpenForumCmdExecute());
            CloseForumCmd = new DelegateCommand(param => CloseForumCmdExecute(), param => CanCloseForumCmdExecute());
            CreateCommentCmd = new DelegateCommand(param => CreateCommentCmdExecute(), param => CanCreateCommentCmdExecute());
            GuestReportDisplayCmd = new DelegateCommand(param => OpenGuestReportDisplayCmdExecute(), param => CanOpenGuestReportDisplayCmdExecute());
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

        public static Forum SelectedForum
        {
            get;
            set;
        }

        public static User User
        {
            get;
            set;
        }

        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public Forum NewForum
        {
            get => _newForum;
            set
            {
                if (value != _newForum)
                {
                    _newForum = value;
                    OnPropertyChanged("NewForum");
                }
            }
        }

        public ForumComment NewComment
        {
            get => _newComment;
            set
            {
                if (value != _newComment)
                {
                    _newComment = value;
                    OnPropertyChanged("NewComment");
                }
            }
        }


        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
                }
            }
        }

        public ObservableCollection<ForumComment> Comments
        {
            get => _comments;
            set
            {
                if (value != _comments)
                {
                    _comments = value;
                    OnPropertyChanged("Comments");
                }
            }
        }

        public ObservableCollection<Forum> Forums
        {
            get => _forums;
            set
            {
                if (value != _forums)
                {
                    _forums = value;
                    OnPropertyChanged("Forums");
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

        public bool CanOpenForumCmdExecute()
        {
            return true;
        }

        public void OpenForumCmdExecute()
        {
            if(SelectedForum != null)
            {
                Comments = new ObservableCollection<ForumComment>(_forumCommentService.ForumCommentRepository.GetAll().FindAll(c => c.Forum.Id == SelectedForum.Id));
            }
        }

        public bool CanCloseForumCmdExecute()
        {
            return true;
        }

        public void CloseForumCmdExecute()
        {
            Comments.Clear();
        }

        public bool CanCreateForumCmdExecute()
        {
            return true;
        }

        public void CreateForumCmdExecute()
        {
            NewForum = new Forum();
            NewComment = new ForumComment();
            NewComment.Comment = Comment;
            NewComment.ValidationClear();
            NewComment.ValidateSelf(Comment);

            if (_forumService.IsDuplicate(City) == false && string.IsNullOrEmpty(Comment) == false)
            {
                
                if(NewComment.IsValid == true)
                {
                    NewForum.Name = City + " forum";
                    NewForum.Location = _locationService.GetByCity(City);
                    _forumService.ForumRepository.Create(NewForum);
                    ForumComment forumComment = new ForumComment(_loggedInGuest, NewForum, Comment);
                    _forumCommentService.ForumCommentRepository.Create(forumComment);
                    _forumCommentService.MarkAsSuper(_reservationService.GetAll());
                    Forums.Add(NewForum);                   
                    Comment = "";
                    MessageBox.Show("You successfully created a new forum!");
                }
                
            }
            else if(_forumService.IsDuplicate(City) == true)
            {
                MessageBox.Show("This forum already exists.");
            }
            /*else
            {
                MessageBox.Show("You have to enter your first comment.");
            }*/
        }

        public bool CanCreateCommentCmdExecute()
        {
            return true;
        }

        public void CreateCommentCmdExecute()
        {
            /*if(SelectedForum != null)
            {
                String city = "";
                bool first = true;
                foreach(String part in  SelectedForum.Name.Split(" "))
                {
                    if(first == true)
                    {
                        city += part;
                        first = false;
                    }
                    else if(part != "Forum")
                    {
                        city += " " + part;
                    }
                    else
                    {
                        break;
                    }
                }
                Location location = _locationService.GetByCity(City);

            }*/ 

            ForumComment comment = new ForumComment(_loggedInGuest, SelectedForum, Comment);
            _forumCommentService.ForumCommentRepository.Create(comment);
            _forumCommentService.MarkAsSuper(_reservationService.GetAll());
            Comments = new ObservableCollection<ForumComment>(_forumCommentService.ForumCommentRepository.GetAll().FindAll(c => c.Forum.Id == SelectedForum.Id));
            Comment = "";
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
