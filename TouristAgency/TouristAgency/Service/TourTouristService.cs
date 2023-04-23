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
    public class TourTouristService
    {

        private readonly App _app;
        private TourTouristRepository TourTouristRepository { get; set; }
        public TourTouristService()
        {
            _app = (App)App.Current;
            TourTouristRepository = _app.TourTouristRepository;
        }

        public TourTourist GetByTouristID(int ID)
        {
            return TourTouristRepository.GetAll().First(tt => tt.TouristID == ID);
        }

        public List<Tourist> GetArrivedTourist(int tourID, List<Tourist> tourists)
        {
            List<Tourist> arrivedTourists = new List<Tourist>();
            foreach(TourTourist tourTourist in TourTouristRepository.GetAll())
            {
                foreach(Tourist tourist in tourists) 
                {
                    if(tourTourist.TourID == tourID && tourTourist.TouristID == tourist.ID && tourTourist.Arrived == true)
                    {
                        arrivedTourists.Add(tourist);
                    }
                }
            }

            return arrivedTourists;
        }
    }
}
