using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Notifications.Domain
{
    public class Notification
    {
        protected int _id;
        protected string _message;
        protected DateTime _created;

        public Notification()
        {
            _id = -1;
            _created = DateTime.Today;
        }

        public Notification(string message)
        {
            _id = -1;
            _message = message;
            _created = DateTime.Today;
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
            }
        }

        public DateTime Created
        {
            get => _created;
            set
            {
                _created = value;
            }
        }
    }
}
