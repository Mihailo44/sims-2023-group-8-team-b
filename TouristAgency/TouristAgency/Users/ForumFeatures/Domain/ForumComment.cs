using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Users.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class ForumComment : ISerializable
    {
        private int _id;
        private Forum _forum;
        private User _user;
        private string _comment;
        private DateTime _created;
        private int _reportNum;
        
        public ForumComment()
        {
            _id = -1;
            _created = DateTime.Now;
            Forum = new();
            User = new();
        }

        public ForumComment(User user,Forum forum,string comment)
        {
            _id = -1;
            _created = DateTime.Now;
            _user = user;
            _forum = forum;
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
            Forum.Id = int.Parse(values[1]);
            User.ID = int.Parse(values[2]);
            Comment = values[3];
            Created = DateTime.Parse(values[4]);
            ReportNum = int.Parse(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Forum.Id.ToString(),
                User.ID.ToString(),
                Comment,
                Created.ToString(),
                ReportNum.ToString()
            };

            return csvValues;
        }
    }
}
