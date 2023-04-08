using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage;

namespace TouristAgency.Service
{
    public class TourCheckpointService : ISubject
    {
        private readonly TourCheckpointStorage _storage;
        private readonly List<TourCheckpoint> _tourCheckpoint;
        private List<IObserver> _observers;

        public TourCheckpointService()
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

        public void Update(TourCheckpoint TourCheckpoint)
        {
            foreach (TourCheckpoint tourCheckpoint in _tourCheckpoint)
            {
                if (tourCheckpoint.TourID == TourCheckpoint.TourID &&
                    tourCheckpoint.CheckpointID == TourCheckpoint.CheckpointID)
                {
                    tourCheckpoint.IsVisited = TourCheckpoint.IsVisited;
                }
            }
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

        public ObservableCollection<TourCheckpoint> GetTourCheckpointsByTourID(int tourID, List<Checkpoint> checkpoints)
        {
            ObservableCollection<TourCheckpoint> tourCheckpoints = new ObservableCollection<TourCheckpoint>(FindByID(tourID));
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
            List<TourCheckpoint> tourCheckpoints = _tourCheckpoint.FindAll(tc => tc.TourID == tour.ID);
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
