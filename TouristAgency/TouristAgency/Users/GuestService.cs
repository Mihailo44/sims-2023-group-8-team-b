using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage;

namespace TouristAgency.Users
{
    public class GuestService
    {
        private readonly App _app;
        public GuestRepository GuestRepository { get; }
        public GuestService()
        {
            _app = (App)System.Windows.Application.Current;
            GuestRepository = _app.GuestRepository;
        }

    }
}
