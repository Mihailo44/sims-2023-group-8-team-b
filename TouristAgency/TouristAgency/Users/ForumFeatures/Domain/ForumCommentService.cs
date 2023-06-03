using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class ForumCommentService
    {
        private readonly App _app;
        public ForumCommentRepository ForumCommentRepository { get; }

        public ForumCommentService()
        {
            _app = (App)App.Current;
            ForumCommentRepository = _app.ForumCommentRepository;
        }

        public List<ForumComment> GetForumComments(Forum forum)
        {
            return ForumCommentRepository.GetAll().FindAll(c => c.Forum.Id == forum.Id);
        }
    }
}
