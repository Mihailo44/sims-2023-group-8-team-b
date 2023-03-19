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
    public class TourTouristCheckpointController
    {
        private readonly TourTouristCheckpointDAO _tourTouristCheckpointDAO;

        public TourTouristCheckpointController()
        {
            _tourTouristCheckpointDAO = new TourTouristCheckpointDAO();
        }

        public void Create(TourTouristCheckpoint tourTouristCheckpoint)
        {
            _tourTouristCheckpointDAO.Create(tourTouristCheckpoint);
        }

        public void Delete(int touristID)
        {
            _tourTouristCheckpointDAO.Delete(touristID);
        }

        public List<TourTouristCheckpoint> GetAll()
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
