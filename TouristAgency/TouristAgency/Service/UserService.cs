using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;

namespace TouristAgency.Service
{
    public class UserService
    {
        public object User { get; set; }

        private OwnerService _ownerService;
        private GuestService _guestService;
        private TouristService _TouristService;
        private GuideService _GuideService;

        public UserService()
        {
            _ownerService = new();
            _guestService = new();
            _TouristService = new();
            _GuideService = new();
        }

        public object GetUser(string Username,string Password)
        {
            User = _ownerService.GetAll().Find(o => o.Username == Username.Trim() && o.Password == Password.Trim());
            if (User != null)
            {
                return User as Owner;
            }
            User = _guestService.GetAll().Find(g => g.Username == Username.Trim() && g.Password == Password.Trim());
            if (User != null)
            {
                return User as Guest;
            }
            User = _TouristService.GetAll().Find(t => t.Username == Username.Trim() && t.Password == Password.Trim());
            if (User != null)
            {
                return User as Tourist;
            }
            User = _GuideService.GetAll().Find(g => g.Username == Username.Trim() && g.Password == Password.Trim());
            if (User != null)
            {
                return User as Guide;
            }

            return null;
        }
    }
}
