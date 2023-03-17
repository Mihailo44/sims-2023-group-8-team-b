using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.DAO;

namespace TouristAgency.Controller
{
    public class TourTouristController
    {
        private readonly TourTouristDAO _tourTouristDAO;

        public TourTouristController()
        {
            _tourTouristDAO = new TourTouristDAO();
        }

        public void Create(TourTourist tourTourist)
        {
            _tourTouristDAO.Create(tourTourist);
        }

        public void Delete(int touristID)
        {
            _tourTouristDAO.Delete(touristID);
        }

        public List<TourTourist> GetAll()
        {
            return _tourTouristDAO.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _tourTouristDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourTouristDAO.Subscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourTouristDAO.NotifyObservers();
        }
    }
}
