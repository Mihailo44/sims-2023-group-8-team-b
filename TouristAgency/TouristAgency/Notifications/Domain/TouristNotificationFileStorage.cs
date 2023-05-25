using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Notifications.Domain
{
    public class TouristNotificationFileStorage : IStorage<TouristNotification>
    {
        private Serializer<TouristNotification> _serializer;
        private readonly string _file = "touristnotifications.txt";

        public TouristNotificationFileStorage()
        {
            _serializer = new Serializer<TouristNotification>();
        }

        public List<TouristNotification> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TouristNotification> notifications)
        {
            _serializer.ToCSV(_file, notifications);
        }
    }
}
