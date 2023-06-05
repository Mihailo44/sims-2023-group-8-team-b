using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Users.Domain;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class ForumCommentRepository : ICrud<ForumComment>,ISubject
    {
        private readonly IStorage<ForumComment> _storage;
        private List<ForumComment> _forumComments;
        private readonly List<IObserver> _observers;

        public ForumCommentRepository(IStorage<ForumComment> storage)
        {
            _storage = storage;
            _forumComments = _storage.Load();
            _observers = new List<IObserver>();
        }

        private int GenerateId()
        {
            return _forumComments.Count() == 0 ? 0: _forumComments.Max(x => x.Id) + 1;
        }

        public ForumComment GetById(int id)
        {
            return _forumComments.Find(c => c.Id == id);
        }

        public ForumComment Create(ForumComment newComment)
        {
            newComment.Id = GenerateId();
            _forumComments.Add(newComment);
            _storage.Save(_forumComments);
            NotifyObservers();

            return newComment;
        }

        public ForumComment Update(ForumComment updatedComment, int id)
        {
            ForumComment currentComment = GetById(id);
            currentComment.ReportNum = updatedComment.ReportNum;
            currentComment.Comment = updatedComment.Comment;
            currentComment.SuperComment = updatedComment.SuperComment;
            
            _storage.Save(_forumComments);  
            NotifyObservers();

            return currentComment;
        }

        public void Delete(int id)
        {
            ForumComment forumComment = GetById(id);
            _forumComments.Remove(forumComment);
            _storage.Save(_forumComments);
            NotifyObservers();
        }

        public List<ForumComment> GetAll()
        {
            return _forumComments;
        }

        public void LoadForumsToComments(List<Forum> forums)
        {
            foreach(var comment in _forumComments)
            {
                comment.Forum = forums.Find(f => f.Id == comment.Forum.Id);
            }
        }

        public void LoadUsersToComments(List<User> users)
        {
            foreach (var comment in _forumComments)
            {
                comment.User = users.Find(u => u.ID == comment.User.ID);
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers) 
            {
                observer.Update();
            }
        }
    }
}
