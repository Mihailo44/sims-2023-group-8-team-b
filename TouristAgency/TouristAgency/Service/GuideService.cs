using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Storage.FileStorage;

namespace TouristAgency.Service
{
    public class GuideService
    {
        //TODO Logic for guide later...
        private readonly App _app;
        private GuideRepository GuideRepository { get; set; }
        public GuideService()
        {
            _app = (App)App.Current;
            GuideRepository = _app.GuideRepository;
        }
    }
}
