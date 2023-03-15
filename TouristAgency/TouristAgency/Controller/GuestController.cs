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
    public class GuestController
    {
        private readonly GuestDAO _guest;

        public GuestController() 
        {
            _guest = new GuestDAO();
        }

        public int GenerateId()
        {
            return _guest.GenerateId();
        }

        public Guest FindById(int id)
        {
            return _guest.FindById(id);
        }

        public Guest Create(Guest newGuest)
        {
            return _guest.Create(newGuest);
        }

        public Guest Update(Guest newGuest, int id)
        {
            return _guest.Update(newGuest, id);
        }

        public Guest Delete(int id)
        {
            return _guest.Delete(id);
        }

        public List<Guest> GetAll()
        {
            return _guest.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _guest.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _guest.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _guest.NotifyObservers();
        }
    }
}
