using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Service;
using TouristAgency.Model;
using TouristAgency.Interfaces;

namespace TouristAgency.ViewModel
{
    public class PostponementRequestCommentDialogueViewModel : ViewModelBase,ICloseable
    {
        private PostponementRequestService _postponementRequestService;
        private PostponementRequest _postponementRequest;
        private readonly App app = (App)App.Current;
        private readonly Window _window;
     
        public string Comment { get; set; }

        public DelegateCommand SubmitCommentCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public PostponementRequestCommentDialogueViewModel(PostponementRequest postponementRequest,Window window)
        {
            _postponementRequestService = app.PostponementRequestService;
            _postponementRequest = postponementRequest;
            _window = window;
            SubmitCommentCmd = new DelegateCommand(param => SubmitCommentExecute(), param => CanSubmitCommentExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(),param => CanCloseWindowExecute());
        }

        public bool CanSubmitCommentExecute()
        {
            return true;
        }

        public void SubmitCommentExecute()
        {
            _postponementRequest.Comment = Comment;
            _postponementRequestService.Update(_postponementRequest, _postponementRequest.Id);
            MessageBox.Show("Comment successfully submited");
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
