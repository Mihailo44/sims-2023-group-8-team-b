using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Storage;

namespace TouristAgency.Service
{
    public class GuestService 
    {
        private readonly App _app;
        public GuestRepository GuestRepository { get; }
        public GuestService()
        {
            _app = (App)App.Current;
            GuestRepository = _app.GuestRepository;
        }

    }
}
