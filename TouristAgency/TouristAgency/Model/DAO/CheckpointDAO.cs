﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;
using TouristAgency.View.Home;

namespace TouristAgency.Model.DAO
{
    public class CheckpointDAO : ICrud<Checkpoint>, ISubject
    {
        private readonly CheckpointStorage _storage;
        private readonly List<Checkpoint> _checkpoints;
        private List<IObserver> _observers;

        public CheckpointDAO()
        {
            _storage = new CheckpointStorage();
            _checkpoints = new List<Checkpoint>();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _checkpoints.Max(c => c.ID) + 1;
        }

        public Checkpoint FindById(int id)
        {
            return _checkpoints.Find(c => c.ID == id);
        }

        public Checkpoint Create(Checkpoint newCheckpoint)
        {
            newCheckpoint.ID = GenerateId();
            _checkpoints.Add(newCheckpoint);
            _storage.Save(_checkpoints);
            NotifyObservers();
            return newCheckpoint;
        }

        public Checkpoint Update(Checkpoint newCheckpoint, int id)
        {
            Checkpoint currentCheckpoint = FindById(id);
            if (currentCheckpoint == null)
            {
                return null;
            }
            currentCheckpoint.AttractionName = newCheckpoint.AttractionName;
            currentCheckpoint.IsVisited = newCheckpoint.IsVisited;
            currentCheckpoint.Address = newCheckpoint.Address; //! Duboka kopija?
            return currentCheckpoint;
        }

        public Checkpoint Delete(int id)
        {
            Checkpoint currentCheckpoint = FindById(id);
            if (currentCheckpoint == null)
            {
                return null;
            }
            _checkpoints.Remove(currentCheckpoint);
            return currentCheckpoint; //TODO skloni
        }

        public List<Checkpoint> GetAll()
        {
            return _checkpoints;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);;
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);;
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
