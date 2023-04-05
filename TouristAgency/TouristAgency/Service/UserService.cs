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
        private TouristService _touristService;
        private GuideService _guideService;
        private readonly App app = (App)App.Current;

        public UserService()
        {
            _ownerService = app.OwnerService;
            _touristService = app.TouristService;
            _guideService = app.GuideService;
            _guestService = new();
            //guest servis fali
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
            User = _touristService.GetAll().Find(t => t.Username == Username.Trim() && t.Password == Password.Trim());
            if (User != null)
            {
                return User as Tourist;
            }
            User = _guideService.GetAll().Find(g => g.Username == Username.Trim() && g.Password == Password.Trim());
            if (User != null)
            {
                return User as Guide;
            }

            return null;
        }
    }
}
