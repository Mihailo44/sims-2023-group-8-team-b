using System.Collections.Generic;
using System.Linq;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Util;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class ForumService
    {
        private readonly App _app;
        public ForumRepository ForumRepository { get; }

        public ForumService()
        {
            _app = (App)App.Current;
            ForumRepository = _app.ForumRepository;
        }

        public List<Forum> GetAll()
        {
            return ForumRepository.GetAll();
        }

        public void IsUseful(ForumCommentService forumCommentService,ReservationService reservationService)
        {
            foreach(Forum forum in ForumRepository.GetAll())
            {
               List<ForumComment> forumComments = forumCommentService.GetForumComments(forum);
               int ownerCommentsNum = CountOwnerComments(forumComments);
               int guestCommentsNum = CountGuestComments(forumComments, reservationService);

                if(ownerCommentsNum >= 2 && guestCommentsNum >= 2)
                {
                    forum.IsUseful = true;
                    ForumRepository.Update(forum, forum.Id);
                }
            }
        }

        public bool IsDuplicate(string city)
        {
            Forum forum = ForumRepository.GetAll().FirstOrDefault(f => f.Name.Contains(city));
            if(forum == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int CountOwnerComments(List<ForumComment> forumComments)
        {
            int ownerCommentsNum = 0;

            foreach(ForumComment comment in forumComments)
            {
                if(comment.User.UserType == UserType.OWNER)
                {
                    ownerCommentsNum++;
                }
            }

            return ownerCommentsNum;
        }

        private int CountGuestComments(List<ForumComment> forumComments,ReservationService reservationService)
        {
            int guestCommentsNum = 0;

            foreach (ForumComment comment in forumComments)
            {
                if (comment.User.UserType == UserType.GUEST)
                {
                    foreach (Reservation reservation in reservationService.GetByGuestId(comment.User.ID))
                    {
                        if (reservation.Accommodation.Location.Equals(comment.Forum.Location))
                        {
                            guestCommentsNum++;
                        }
                    }
                }
            }

            return guestCommentsNum;
        }
    }
}
