using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users.ForumFeatures.Domain;

namespace TouristAgency.Users.ForumFeatures.CommentFeature
{
    public class CommentCreationViewModel : ViewModelBase, ICloseable,ICreate
    {
        private readonly App _app;
        private readonly Window _window;
        private ForumCommentService _forumCommentService;
        public ForumComment NewForumComment { get; set; }
        public string Comment { get; set; }
        public Forum Forum { get; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand CreateCmd { get; }

        public CommentCreationViewModel(Forum forum,Window window)
        {
            _app = (App)App.Current;
            Forum = forum;
            _window = window;
            _forumCommentService = new();
            NewForumComment = new();
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(),param => CanCloseCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(),param => CanCreateCmdExecute());
        }

        public bool CanCreateCmdExecute()
        {
            return true;
        }

        public void CreateCmdExecute()
        {
            NewForumComment.ValidateSelf(Comment);
            if (NewForumComment.IsValid)
            {
                //NewForumComment = new(_app.LoggedUser,Forum,Comment);
                NewForumComment.User = _app.LoggedUser;
                NewForumComment.Forum = Forum;
                NewForumComment.Comment = Comment;
                _forumCommentService.ForumCommentRepository.Create(NewForumComment);
                _window.Close();
            }
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
