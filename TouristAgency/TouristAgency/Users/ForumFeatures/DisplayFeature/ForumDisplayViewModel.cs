using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Users.ForumFeatures.Domain;

namespace TouristAgency.Users.ForumFeatures.DisplayFeature
{
    public class ForumDisplayViewModel : ViewModelBase,ICloseable,IObserver
    {
        private ForumCommentService _forumCommentService;

        public Forum SelectedForum { get; }

        public DelegateCommand CloseCmd { get; }

        public ObservableCollection<ForumComment> ForumComments { get; set; }

        public ForumDisplayViewModel(Forum selectedForum)
        {
            _forumCommentService = new();
            _forumCommentService.ForumCommentRepository.Subscribe(this);
            SelectedForum = selectedForum;
            ForumComments = new ObservableCollection<ForumComment>();
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            LoadComments();
        }

        private void LoadComments()
        {
            ForumComments.AddRange(_forumCommentService.GetForumComments(SelectedForum));
        }

        public void Update()
        {
            LoadComments();
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
