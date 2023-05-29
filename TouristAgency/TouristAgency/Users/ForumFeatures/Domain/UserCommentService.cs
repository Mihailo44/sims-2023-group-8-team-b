using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class UserCommentService
    {
        private readonly App _app;
        public UserCommentRepository UserCommentRepository { get; }

        public UserCommentService()
        {
            _app = (App)App.Current;
            UserCommentRepository = _app.UserCommentRepository;
        }

        public List<UserComment> GetByForumAndUser(int forumId)
        {
            return UserCommentRepository.GetAll().FindAll(uc => uc.ForumId == forumId && uc.UserId == _app.LoggedUser.ID);
        }
    }
}
