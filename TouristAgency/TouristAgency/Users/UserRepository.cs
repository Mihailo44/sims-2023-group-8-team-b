using System.Collections.Generic;
using TouristAgency.Interfaces;

namespace TouristAgency.Users
{
    public class UserRepository
    {
        private readonly IStorage<User> _userStorage;
        private readonly List<User> _users;

        public User User { get; set; }

        public UserRepository(IStorage<User> storage)
        {
            _userStorage = storage;
            _users = _userStorage.Load();
        }

        public User CheckCredentials(string username, string password)
        {
            User user = _users.Find(u => u.Username == username.Trim() && u.Password == password.Trim());
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
