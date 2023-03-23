using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class GuideController
    {
        private readonly GuideDAO _guide;

        public GuideController()
        {
            _guide = new GuideDAO();
        }

        public int GenerateId()
        {
            return _guide.GenerateId();
        }

        public Guide FindById(int id)
        {
            return _guide.FindById(id);
        }

        public Guide Create(Guide newGuide)
        {
            return _guide.Create(newGuide);
        }

        public Guide Update(Guide newGuide, int id)
        {
            return _guide.Update(newGuide, id);
        }

        public void Delete(int id)
        {
            _guide.Delete(id);
        }

        public List<Guide> GetAll()
        {
            return _guide.GetAll();
        }

        public void LoadToursToGuide(List<Tour> tours)
        {
            _guide.LoadToursToGuide(tours);
        }

        public void Subscribe(IObserver observer)
        {
            _guide.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _guide.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _guide.NotifyObservers();
        }
    }
}
