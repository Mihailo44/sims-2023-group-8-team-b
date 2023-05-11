using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.PostponementFeatures
{
    public class PostponementRequestCommentDialogueViewModel : ViewModelBase, ICloseable
    {
        private PostponementRequestService _postponementRequestService;
        private PostponementRequest _postponementRequest;
        private readonly App app = (App)Application.Current;
        private readonly Window _window;

        public string Comment { get; set; }

        public DelegateCommand SubmitCommentCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public PostponementRequestCommentDialogueViewModel(PostponementRequest postponementRequest, Window window)
        {
            _postponementRequestService = new();
            _postponementRequest = postponementRequest;
            _window = window;
            Comment = "";
            SubmitCommentCmd = new DelegateCommand(param => SubmitCommentExecute(), param => CanSubmitCommentExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(), param => CanCloseWindowExecute());
        }

        public bool CanSubmitCommentExecute()
        {
            return true;
        }

        public void SubmitCommentExecute()
        {
            _postponementRequest.Comment = Comment.Trim();
            _postponementRequestService.PostponementRequestRepository.Update(_postponementRequest, _postponementRequest.Id);
            MessageBox.Show("Comment successfully submited","Postponement Comment Dialogue",MessageBoxButton.OK,MessageBoxImage.Information);
            _window.Close();
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            _window.Close();
        }
    }
}
