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

        public static Forum SelectedForum { get; set; }

        public DelegateCommand OpenNewCommentCmd { get; }
        public DelegateCommand ReportCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public ObservableCollection<ForumComment> ForumComments { get; set; }

        public ForumDisplayViewModel(Forum selectedForum)
        {
            _app = (App)App.Current;
            _forumCommentService = new();
            _locationService = new();

            _forumCommentService.ForumCommentRepository.Subscribe(this);
            SelectedForum = selectedForum;

            ForumComments = new ObservableCollection<ForumComment>();

            OpenNewCommentCmd = new DelegateCommand(param => OpenNewCommentCmdExecute(),param => CanOpenNewCommentCmdExecute());
            ReportCmd = new DelegateCommand(param => ReportCmdExecute(),param => CanReportCmdExecute());
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            LoadComments();
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

        public bool CanReportCmdExecute()
        {
            return true;
        }

        public void ReportCmdExecute()
        {
            
            MessageBox.Show("radim");
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
