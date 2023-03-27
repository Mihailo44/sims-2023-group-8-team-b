using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class TourCheckpointController
    {
        private readonly TourCheckpointDAO _tourCheckpointDAO;

        public TourCheckpointController()
        {
            _tourCheckpointDAO = new TourCheckpointDAO();
        }

        public List<TourCheckpoint> FindByID(int id)
        {
            return _tourCheckpointDAO.FindByID(id);
        }

        public void Create(TourCheckpoint TourCheckpoint)
        {
            _tourCheckpointDAO.Create(TourCheckpoint);
        }

        public void Update(TourCheckpoint TourCheckpoint)
        {
            _tourCheckpointDAO.Update(TourCheckpoint);
        }

        public void Delete(int tourID)
        {
            _tourCheckpointDAO.Delete(tourID);
        }

        public List<TourCheckpoint> GetAll()
        {
            return _tourCheckpointDAO.GetAll();
        }

        public void LoadCheckpoints(List<Checkpoint> checkpoints)
        {
            _tourCheckpointDAO.LoadCheckpoints(checkpoints);
        }

        public ObservableCollection<TourCheckpoint> GetTourCheckpointsByTourID(int tourID, List<Checkpoint> checkpoints)
        {
            return _tourCheckpointDAO.GetTourCheckpointsByTourID(tourID, checkpoints);
        }

        public void Subscribe(IObserver observer)
        {
            _tourCheckpointDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourCheckpointDAO.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourCheckpointDAO.NotifyObservers();
        }

    }
}
