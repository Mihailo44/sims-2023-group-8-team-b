using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model.DAO;
using TouristAgency.Model;

namespace TouristAgency.Controller
{
    public class TourController
    {
        private readonly TourDAO _tour;

        public TourController()
        {
            _tour = new TourDAO();
        }

        public List<Tour> GetAll()
        {
            return _tour.GetAll();
        }

        public void Create(Tour newTour)
        {
            _tour.Create(newTour);
        }

        public void Update(Tour updatedTour, int id)
        {
            _tour.Update(updatedTour, id);
        }

        public void Delete(Tour Tour)
        {
            _tour.Delete(Tour.ID);
        }

        public void Subsribe(IObserver observer)
        {
            _tour.Subscribe(observer);
        }
    }
}
