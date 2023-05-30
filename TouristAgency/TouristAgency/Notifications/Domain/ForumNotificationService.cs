using System;
using System.Collections.Generic;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Users.ForumFeatures.Domain;

namespace TouristAgency.Notifications.Domain
{
    public class ForumNotificationService
    {
        private App _app;
        public ForumNotificationRepository ForumNotificationRepository { get; }

        public ForumNotificationService()
        {
            _app = (App)App.Current;
            ForumNotificationRepository = _app.ForumNotificationRepository;
        }

        public void ManageNotifications(int ownerId, ForumService forumService)
        {
            DateTime today = DateTime.UtcNow.Date;

            string message;

            foreach (var forum in forumService.GetAll())
            {
                if (forum.Created == today)
                {
                    message = $"Check out new forum {forum.Name}";
                    ForumNotification notification = new ForumNotification(forum, message);
                    ForumNotificationRepository.Create(notification);
                }
            }
        }

        public List<ForumNotification> GetOwnersNotifications()
        {
            List<ForumNotification> notifications = new();

            foreach (Accommodation a in _app.LoggedUser.Accommodations)
            {
                notifications.AddRange(ForumNotificationRepository.GetAll().FindAll(n => n.Forum.LocationId == a.LocationId));
            }

            return notifications;
        }
    }
}
