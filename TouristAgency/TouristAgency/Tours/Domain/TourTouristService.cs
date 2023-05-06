using System.Collections.Generic;
using System.Linq;
using TouristAgency.Users;

namespace TouristAgency.Tours
{
    public class TourTouristService
    {

        private readonly App _app;
        public TourTouristRepository TourTouristRepository { get; set; }
        public TourTouristService()
        {
            _app = (App)System.Windows.Application.Current;
            TourTouristRepository = _app.TourTouristRepository;
        }

        public TourTourist GetByTouristID(int ID)
        {
            return TourTouristRepository.GetAll().First(tt => tt.TouristID == ID);
        }

        public List<Tourist> GetArrivedTourist(int tourID, List<Tourist> tourists)
        {
            List<Tourist> arrivedTourists = new List<Tourist>();
            foreach (TourTourist tourTourist in TourTouristRepository.GetAll())
            {
                foreach (Tourist tourist in tourists)
                {
                    if (tourTourist.TourID == tourID && tourTourist.TouristID == tourist.ID && tourTourist.Arrived == true)
                    {
                        arrivedTourists.Add(tourist);
                    }
                }
            }

            return arrivedTourists;
        }
    }
}
