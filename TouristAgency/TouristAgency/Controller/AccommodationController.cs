using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model.DAO;
using TouristAgency.Model;
using TouristAgency.Interfaces;

namespace TouristAgency.Controller
{
    public class AccommodationController
    {
        private readonly AccommodationDAO _accommodation;

        public AccommodationController()
        {
            _accommodation = new AccommodationDAO();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodation.GetAll();
        }

        public void Create(Accommodation newAccommodation)
        {
            _accommodation.Create(newAccommodation);
        }

        public void Update(Accommodation newAccommodation,int id)
        {
            _accommodation.Update(newAccommodation,id);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodation.Delete(accommodation.Id);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodation.Subscribe(observer);
        }
    }
}
