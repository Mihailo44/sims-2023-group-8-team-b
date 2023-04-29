using System.Collections.Generic;
using TouristAgency.Util;

namespace TouristAgency.Tours
{
    public class CheckpointService
    {
        private readonly App _app;
        public CheckpointRepository CheckpointRepository { get; set; }

        public CheckpointService()
        {
            _app = (App)System.Windows.Application.Current;
            CheckpointRepository = _app.CheckpointRepository;
        }

        public List<Checkpoint> GetSuitableByLocation(Location location)
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();
            foreach (Checkpoint checkpoint in CheckpointRepository.GetAll())
            {
                if (checkpoint.Location.Equals(location))
                {
                    checkpoints.Add(checkpoint);
                }
            }
            return checkpoints;
        }

    }
}
