using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class TourTouristController
    {
        private readonly TourTouristDAO _tourTouristCheckpointDAO;

        public TourTouristController()
        {
            _tourTouristCheckpointDAO = new TourTouristDAO();
        }

        public void Create(TourTourist tourTourist)
        {
            _tourTouristCheckpointDAO.Create(tourTourist);
        }

        public void Delete(int touristID)
        {
            _tourTouristCheckpointDAO.Delete(touristID);
        }

        public List<TourTourist> GetAll()
        {
            return _tourTouristCheckpointDAO.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _tourTouristCheckpointDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourTouristCheckpointDAO.Subscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourTouristCheckpointDAO.NotifyObservers();
        }
    }
}
