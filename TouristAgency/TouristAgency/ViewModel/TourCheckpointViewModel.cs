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
    public class TourCheckpointViewModel
    {
        private readonly TourCheckpointService _tourCheckpointService;

        public TourCheckpointViewModel()
        {
            _tourCheckpointService = new TourCheckpointService();
        }

        public List<TourCheckpoint> FindByID(int id)
        {
            return _tourCheckpointService.FindByID(id);
        }

        public void Create(TourCheckpoint TourCheckpoint)
        {
            _tourCheckpointService.Create(TourCheckpoint);
        }

        public void Update(TourCheckpoint TourCheckpoint)
        {
            _tourCheckpointService.Update(TourCheckpoint);
        }

        public void Delete(int tourID)
        {
            _tourCheckpointService.Delete(tourID);
        }

        public List<TourCheckpoint> GetAll()
        {
            return _tourCheckpointService.GetAll();
        }

        public void LoadCheckpoints(List<Checkpoint> checkpoints)
        {
            _tourCheckpointService.LoadCheckpoints(checkpoints);
        }

        public ObservableCollection<TourCheckpoint> GetTourCheckpointsByTourID(int tourID, List<Checkpoint> checkpoints)
        {
            return _tourCheckpointService.GetTourCheckpointsByTourID(tourID, checkpoints);
        }

        public void Subscribe(IObserver observer)
        {
            _tourCheckpointService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourCheckpointService.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourCheckpointService.NotifyObservers();
        }

    }
}
