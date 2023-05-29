using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Users.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class UserComment : ISerializable
    {
        private int _id;
        private int _forumId;
        private int _userId;
        private int _commentId;

        public UserComment() 
        {
            
        }

        public UserComment(int forumId,int userId,int commentId)
        {
            _id = -1;
            _forumId = forumId;
            _userId = userId;
            _commentId = commentId;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int ForumId
        {
            get => _forumId;
            set => _forumId = value;
        }

        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public int CommentId
        {
            get => _commentId;
            set => _commentId = value;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ForumId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
            CommentId = int.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ForumId.ToString(),
                UserId.ToString(),
                CommentId.ToString()
            };

            return csvValues;
        }
    }
}
