﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.DAO;

namespace TouristAgency.Controller
{
    public class TourCheckpointController
    {
        private readonly TourCheckpointDAO _tourCheckpointDAO;

        public TourCheckpointController()
        {
            _tourCheckpointDAO = new TourCheckpointDAO();
        }

        public void Create(TourCheckpoint TourCheckpoint)
        {
            _tourCheckpointDAO.Create(TourCheckpoint);
        }

        public void Delete(int tourID)
        {
            _tourCheckpointDAO.Delete(tourID);
        }

        public List<TourCheckpoint> GetAll()
        {
            return _tourCheckpointDAO.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _tourCheckpointDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourCheckpointDAO.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourCheckpointDAO.NotifyObservers();
        }

    }
}