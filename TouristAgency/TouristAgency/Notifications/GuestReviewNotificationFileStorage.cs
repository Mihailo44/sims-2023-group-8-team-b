using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;
using TouristAgency.Users.ReviewFeatures.Domain;

namespace TouristAgency.Notifications
{
    public class GuestReviewNotificationFileStorage : IStorage<GuestReviewNotification>
    {
        private Serializer<GuestReviewNotification> _serializer;
        private readonly string _file = "guestreviewnotification.txt";

        public GuestReviewNotificationFileStorage()
        {
            _serializer = new Serializer<GuestReviewNotification>();
        }

        public List<GuestReviewNotification> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<GuestReviewNotification> notifications)
        {
            _serializer.ToCSV(_file, notifications);
        }
    }
}
