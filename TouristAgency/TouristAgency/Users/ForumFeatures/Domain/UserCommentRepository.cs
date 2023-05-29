using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class UserCommentRepository : ICrud<UserComment>
    {
        private readonly IStorage<UserComment> _storage;
        private readonly List<UserComment> _userComments;
        private readonly List<IObserver> _observers; 
        
        public UserCommentRepository(IStorage<UserComment> storage)
        {
            _storage = storage;
            _userComments = _storage.Load();
            _observers = new List<IObserver>();
        }

        private int GenerateId()
        {
            return _userComments.Count == 0 ? 0 : _userComments.Max(uc => uc.Id) + 1;
        }

        public UserComment GetById(int id)
        {
            return _userComments.Find(uc => uc.Id == id);
        }

        public UserComment Create(UserComment newLink)
        {
            newLink.Id = GenerateId();
            _userComments.Add(newLink);
            _storage.Save(_userComments);

            return newLink;
        }

        public List<UserComment> GetAll()
        {
            return _userComments;
        }

        public UserComment Update(UserComment obj, int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
