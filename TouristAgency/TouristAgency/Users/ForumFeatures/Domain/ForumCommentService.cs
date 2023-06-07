using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;

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

        public void MarkAsSuper(List<Reservation> reservations)
        {
            foreach(ForumComment comment in ForumCommentRepository.GetAll())
            {
                Reservation reservation = reservations.FirstOrDefault(r => r.Guest.ID == comment.User.ID && comment.Forum.Location.Equals(r.Accommodation.Location));
               if(reservation != null)
                {
                    comment.SuperComment = true;
                }
                else
                {
                    comment.SuperComment = false;
                }

                ForumCommentRepository.Update(comment, comment.Id);
            }
        }
    }
}
