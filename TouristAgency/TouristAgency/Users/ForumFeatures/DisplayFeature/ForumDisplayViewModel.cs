using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users.ForumFeatures.CommentFeature;
using TouristAgency.Users.ForumFeatures.Domain;
using TouristAgency.Util;

namespace TouristAgency.Users.ForumFeatures.DisplayFeature
{
    public class ForumDisplayViewModel : ViewModelBase,ICloseable,IObserver
    {
        private readonly App _app;
        private ForumCommentService _forumCommentService;
        private LocationService _locationService;
        private UserCommentService _userCommentService;
        private GuestService _guestService;

        public static Forum SelectedForum { get; set; }

        public DelegateCommand OpenNewCommentCmd { get; set; }
        public DelegateCommand ReportCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }

        public ObservableCollection<ForumComment> ForumComments { get; set; }

        public ForumDisplayViewModel(Forum selectedForum)
        {
            _app = (App)App.Current;

            InstantiateServices();

            _forumCommentService.ForumCommentRepository.Subscribe(this);
            SelectedForum = selectedForum;

            ForumComments = new ObservableCollection<ForumComment>();

            InstantiateCommands();
            LoadComments();
        }

        private void InstantiateServices()
        {
            _forumCommentService = new();
            _locationService = new();
            _userCommentService = new();
            _guestService = new();
        }

        private void InstantiateCommands()
        {
            OpenNewCommentCmd = new DelegateCommand(param => OpenNewCommentCmdExecute(), param => CanOpenNewCommentCmdExecute());
            ReportCmd = new DelegateCommand(ReportCmdExecute, CanReportCmdExecute);
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
        }

        private void LoadComments()
        {
            ForumComments.Clear();
            ForumComments.AddRange(_forumCommentService.GetForumComments(SelectedForum));
        }

        public void Update()
        {
            LoadComments();
        }

        public bool CanOpenNewCommentCmdExecute()
        {
            return true;
        }

        public void OpenNewCommentCmdExecute()
        {
            if (_locationService.HasAccommodationOnLocation(_app.LoggedUser,SelectedForum.Location))
            {
                CommentCreationForm x = new CommentCreationForm(SelectedForum);
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("You don't have a permission to leave comments","New Comment Dialogue",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        public bool CanReportCmdExecute(object param)
        {
            return true;
        }

        public void ReportCmdExecute(object param)
        {
            ForumComment comment = param as ForumComment;
            List<UserComment> reportList = _userCommentService.GetByForumAndUser(SelectedForum.Id);
            Guest guest = _guestService.GuestRepository.GetById(comment.User.ID);

            if (_locationService.HasAccommodationOnLocation(_app.LoggedUser, SelectedForum.Location))
            {
                if (!_locationService.BeenOnLocation(guest, SelectedForum.Location))
                {
                    if (reportList.Find(r => r.CommentId == comment.Id) == null)
                    {
                        comment.ReportNum++;
                        _forumCommentService.ForumCommentRepository.Update(comment, comment.Id);

                        UserComment report = new UserComment(SelectedForum.Id, _app.LoggedUser.ID, comment.Id);
                        _userCommentService.UserCommentRepository.Create(report);
                    }
                    else
                    {
                        MessageBox.Show("You have already reported this comment", "Report Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("You don't have the permission to report comments","Report Dialogue",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            Messenger.Default.Send(new SwitchViewModelMessage(null));
            Messenger.Reset();
        }
    }
}
