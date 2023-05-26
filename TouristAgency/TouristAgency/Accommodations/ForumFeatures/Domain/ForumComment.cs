using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Users.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.ForumFeatures.Domain
{
    public class ForumComment : ISerializable
    {
        private int _id;
        private Forum _forum;
        private int _forumId;
        private User _user;
        private int _userId;
        private string _comment;
        private DateTime _created;
        private int _reportNum;
        
        public ForumComment()
        {
            _id = -1;
            _created = DateTime.Now;
        }

        public ForumComment(User user,Forum forum,string comment)
        {
            _id = -1;
            _created = DateTime.Now;
            _user = user;
            _userId = user.ID;
            _forum = forum;
            _forumId = forum.Id;
            _comment = comment;
        }

        public int Id
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;
                }
            }
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

        public User User
        {
            get => _user;
            set
            {
                if(_user != value)
                {
                    _user = value;
                }
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                if(_userId != value)
                {
                    _userId = value;
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if(_comment != value)
                {
                    _comment = value;
                }
            }
        }

        public DateTime Created
        {
            get => _created;
            set
            {
                if(_created != value)
                {
                    _created = value;
                }
            }
        }

        public int ReportNum
        {
            get => _reportNum;
            set
            {
                if (_reportNum != value)
                {
                    _reportNum = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ForumId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
            Comment = values[3];
            Created = DateTime.Parse(values[4]);
            ReportNum = int.Parse(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ForumId.ToString(),
                UserId.ToString(),
                Comment,
                Created.ToString(),
                ReportNum.ToString()
            };

            return csvValues;
        }
    }
}
