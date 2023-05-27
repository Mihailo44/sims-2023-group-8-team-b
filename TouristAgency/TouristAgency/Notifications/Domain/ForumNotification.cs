using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ForumFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Notifications.Domain
{
    public class ForumNotification : Notification, ISerializable
    {
        private Forum _forum;
        private int _forumId;

        public ForumNotification() : base()
        {

        }

        public ForumNotification(Forum forum,string message) : base(message)
        {
            _forum = forum;
            _forumId = forum.Id;
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

        public int ForumId
        {
            get => _forumId;
            set
            {
                if(_forumId != value)
                {
                    _forumId = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ForumId = int.Parse(values[1]);
            Message = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ForumId.ToString(),
                Message
            };

            return csvValues;
        }
    }
}
