using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Notifications.Domain
{
    public class ForumNotificationFileStorage : IStorage<ForumNotification>
    {
        private Serializer<ForumNotification> _serializer;
        private readonly string _file = "forumNotifications.txt";

        public ForumNotificationFileStorage()
        {
            _serializer = new Serializer<ForumNotification>();
        }

        public List<ForumNotification> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<ForumNotification> notifications)
        {
            _serializer.ToCSV(_file, notifications); 
        }
    }
}
