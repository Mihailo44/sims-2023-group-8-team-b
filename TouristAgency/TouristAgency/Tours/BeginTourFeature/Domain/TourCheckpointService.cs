using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TouristAgency.Tours.BeginTourFeature.Domain
{
    public class TourCheckpointService
    {

        private readonly App _app;
        public TourCheckpointRepository TourCheckpointRepository { get; set; }
        public TourCheckpointService()
        {
            _app = (App)System.Windows.Application.Current;
            TourCheckpointRepository = _app.TourCheckpointRepository;
        }

        public TourCheckpoint Create(TourCheckpoint newTourCheckpoint)
        {
            return TourCheckpointRepository.Create(newTourCheckpoint);
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
            foreach (TourCheckpoint tourCheckpoint in tourCheckpoints)
            {
                if (tourCheckpoint.IsVisited == true)
                {
                    latestCheckpoint = tourCheckpoint.Checkpoint;
                }
            }
            return latestCheckpoint;
        }
    }
}
