using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Storage.FileStorage;
using TouristAgency.View.Home;

namespace TouristAgency.Service
{
    public class CheckpointService
    {
        private readonly App _app;
        public CheckpointRepository CheckpointRepository { get; set; }

        public CheckpointService()
        {
            _app = (App)App.Current;
            CheckpointRepository = _app.CheckpointRepository;
        }

        public List<Checkpoint> FindSuitableByLocation(Location location)
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
