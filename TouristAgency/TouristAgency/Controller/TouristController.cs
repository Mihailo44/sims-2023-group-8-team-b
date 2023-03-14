using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.DAO;

namespace TouristAgency.Controller
{
    public class TouristController
    {
        private readonly TouristDAO _tourist;

        public TouristController()
        {
            _tourist = new TouristDAO();
        }

        public int GenerateId()
        {
            return _tourist.GenerateId();
        }

        public Tourist FindById(int id)
        {
            return _tourist.FindById(id);
        }

        public Tourist Create(Tourist newTourist)
        {
            return _tourist.Create(newTourist);
        }

        public Tourist Update(Tourist newTourist, int id)
        {
            return _tourist.Update(newTourist, id);
        }

        public Tourist Delete(int id)
        {
            return _tourist.Delete(id);
        }

        public List<Tourist> GetAll()
        {
            return _tourist.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _tourist.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourist.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourist.NotifyObservers();
        }
    }
}
