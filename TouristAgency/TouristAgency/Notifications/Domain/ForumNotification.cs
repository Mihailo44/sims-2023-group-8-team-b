using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Users.ForumFeatures.Domain;

namespace TouristAgency.Notifications.Domain
{
    public class ForumNotification : Notification, ISerializable
    {
        private Forum _forum;

        public ForumNotification() : base()
        {

        }

        public ForumNotification(Forum forum,string message) : base(message)
        {
            _forum = forum;
        }

        public Forum Forum
        {
            get => _forum;
            set
            {
                if(_forum != value)
                {
                    _forum = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Forum = new();

            Id = int.Parse(values[0]);
            Forum.Id = int.Parse(values[1]);
            Message = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Forum.Id.ToString(),
                Message
            };

            return csvValues;
        }
    }
}
