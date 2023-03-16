using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TouristAgency.Interfaces;
using TouristAgency.Model.DAO;
using TouristAgency.Model;

namespace TouristAgency.Controller
{
    public class CheckpointController
    {
        private readonly CheckpointDAO _checkpoint;

        public CheckpointController()
        {
            _checkpoint = new CheckpointDAO();
        }

        public List<Checkpoint> GetAll()
        {
            return _checkpoint.GetAll();
        }

        public void LoadLocationsToCheckpoints(List<Location> locations)
        {
            _checkpoint.LoadLocationsToCheckpoints(locations);
        }

        public List<Checkpoint> FindSuitableByLocation(Location location)
        {
            return _checkpoint.FindSuitableByLocation(location);
        }

        public void Create(Checkpoint newCheckpoint)
        {
            _checkpoint.Create(newCheckpoint);
        }

        public void Update(Checkpoint updatedCheckpoint, int id)
        {
            _checkpoint.Update(updatedCheckpoint, id);
        }

        public void Delete(Checkpoint Checkpoint)
        {
            _checkpoint.Delete(Checkpoint.ID);
        }

        public void Subscribe(IObserver observer)
        {
            _checkpoint.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _checkpoint.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _checkpoint.NotifyObservers();
        }
    }
}
