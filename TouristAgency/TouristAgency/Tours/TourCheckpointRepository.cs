using System.Collections.Generic;
using TouristAgency.Interfaces;

namespace TouristAgency.Tours
{
    public class TourCheckpointRepository : ISubject
    {
        private readonly IStorage<TourCheckpoint> _storage;
        private readonly List<TourCheckpoint> _tourCheckpoints;
        private List<IObserver> _observers;

        public TourCheckpointRepository(IStorage<TourCheckpoint> storage)
        {
            _storage = storage;
            _tourCheckpoints = _storage.Load();
            _observers = new List<IObserver>();
        }
        public void Create(TourCheckpoint TourCheckpoint)
        {
            _tourCheckpoints.Add(TourCheckpoint);
            _storage.Save(_tourCheckpoints);
            NotifyObservers();
        }

        public List<TourCheckpoint> GetByID(int id)
        {
            return _tourCheckpoints.FindAll(tc => tc.TourID == id);
        }
        public List<TourCheckpoint> GetAll()
        {
            return _tourCheckpoints;
        }

        public void Update(TourCheckpoint TourCheckpoint)
        {
            foreach (TourCheckpoint tourCheckpoint in _tourCheckpoints)
            {
                if (tourCheckpoint.TourID == TourCheckpoint.TourID &&
                    tourCheckpoint.CheckpointID == TourCheckpoint.CheckpointID)
                {
                    tourCheckpoint.IsVisited = TourCheckpoint.IsVisited;
                }
            }
            _storage.Save(_tourCheckpoints);
            NotifyObservers();
        }

        public void Delete(int tourID)
        {
            TourCheckpoint deletedTourCheckpoint = _tourCheckpoints.Find(t => t.TourID == tourID);
            _tourCheckpoints.Remove(deletedTourCheckpoint);
            _storage.Save(_tourCheckpoints);
            NotifyObservers();
        }

        public void LoadCheckpoints(List<Checkpoint> checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                foreach (TourCheckpoint tourCheckpoint in _tourCheckpoints)
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
