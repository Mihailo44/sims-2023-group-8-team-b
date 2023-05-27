using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
