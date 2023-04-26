using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Storage.FileStorage;

namespace TouristAgency.Service
{
    public class TourCheckpointService
    {

        private readonly App _app;
        public TourCheckpointRepository TourCheckpointRepository { get; set; }
        public TourCheckpointService()
        {
            _app = (App)App.Current;
            TourCheckpointRepository = _app.TourCheckpointRepository;
        }

        public ObservableCollection<TourCheckpoint> GetTourCheckpointsByTourID(int tourID, List<Checkpoint> checkpoints)
        {
            ObservableCollection<TourCheckpoint> tourCheckpoints = new ObservableCollection<TourCheckpoint>(TourCheckpointRepository.GetByID(tourID));
            foreach (TourCheckpoint tourCheckpoint in tourCheckpoints)
            {
                foreach (Checkpoint checkpoint in checkpoints)
                {
                    if (checkpoint.ID == tourCheckpoint.CheckpointID)
                    {
                        tourCheckpoint.Checkpoint = checkpoint;
                    }
                }
            }
            return tourCheckpoints;
        }

        public Checkpoint GetLatestCheckpoint(Tour tour)
        {
            List<TourCheckpoint> tourCheckpoints = TourCheckpointRepository.GetAll().FindAll(tc => tc.TourID == tour.ID);
            Checkpoint latestCheckpoint = new Checkpoint();
            foreach(TourCheckpoint tourCheckpoint in tourCheckpoints)
            {
                if(tourCheckpoint.IsVisited == true)
                {
                    latestCheckpoint = tourCheckpoint.Checkpoint;
                }
            }
            return latestCheckpoint;
        }
    }
}
