using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Repository;

namespace TouristAgency.Service
{
    public class UserService
    {
        private readonly App app = (App)App.Current;
        public UserRepository UserRepository { get; }

        public UserService()
        {
            UserRepository = app.UserRepository;   
        }
    }
}
