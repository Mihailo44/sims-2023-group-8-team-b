using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourCheckpointAgency.Storage;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class TourCheckpointDAO : ISubject
    {
        private readonly TourCheckpointStorage _storage;
        private readonly List<TourCheckpoint> _tourCheckpoint;
        private List<IObserver> _observers;

        public TourCheckpointDAO()
        {
            _storage = new TourCheckpointStorage();
            _tourCheckpoint = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Create(TourCheckpoint TourCheckpoint)
        {
            _tourCheckpoint.Add(TourCheckpoint);
            _storage.Save(_tourCheckpoint);
            NotifyObservers();
        }

        public void Delete(int tourID)
        {
            TourCheckpoint deletedTourCheckpoint = _tourCheckpoint.Find(t => t.TourID == tourID);
            _tourCheckpoint.Remove(deletedTourCheckpoint);
            _storage.Save(_tourCheckpoint);
            NotifyObservers();
        }

        public List<TourCheckpoint> GetAll()
        {
            return _tourCheckpoint;
        }

        public List<TourCheckpoint> FindByID(int id)
        {
            return _tourCheckpoint.FindAll(tc => tc.TourID == id);
        }

        public void LoadCheckpoints(List<Checkpoint> checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                foreach (TourCheckpoint tourCheckpoint in _tourCheckpoint)
                {
                    if (tourCheckpoint.CheckpointID == checkpoint.ID)
                    {
                        tourCheckpoint.Checkpoint = checkpoint;
                    }
                }
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
