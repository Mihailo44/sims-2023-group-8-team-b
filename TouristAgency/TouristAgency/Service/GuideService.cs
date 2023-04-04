using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage;

namespace TouristAgency.Service
{
    public class GuideService : ICrud<Guide>, ISubject
    {
        private readonly GuideStorage _storage;
        private readonly List<Guide> _guides;
        private List<IObserver> _observers;

        public GuideService()
        {
            _storage = new GuideStorage();
            _guides = _storage.Load();
            _observers = new List<IObserver>();
        }
        public int GenerateId()
        {
            return _guides.Max(g => g.ID) + 1;
        }

        public Guide FindById(int id)
        {
            return _guides.Find(g => g.ID == id);
        }

        public Guide Create(Guide newGuide)
        {
            newGuide.ID = GenerateId();
            _guides.Add(newGuide);
            _storage.Save(_guides);
            NotifyObservers();
            return newGuide;
        }

        public Guide Update(Guide newGuide, int id)
        {
            Guide currentGuide = FindById(id);
            if (currentGuide == null)
            {
                return null;
            }

            currentGuide.Username = newGuide.Username;
            currentGuide.Password = newGuide.Password;
            currentGuide.FirstName = newGuide.FirstName;
            currentGuide.LastName = newGuide.LastName;
            currentGuide.DateOfBirth = newGuide.DateOfBirth; //! Duboka kopija?
            currentGuide.Email = newGuide.Email;
            currentGuide.FullLocation = newGuide.FullLocation; //! -||-?
            currentGuide.Phone = newGuide.Phone;
            return currentGuide;
        }

        public void Delete(int id)
        {
            Guide deletedGuide = FindById(id);
            _guides.Remove(deletedGuide);
            _storage.Save(_guides);
            NotifyObservers();
        }

        public List<Guide> GetAll()
        {
            return _guides;
        }

        public void LoadToursToGuide(List<Tour> tours)
        {
            foreach (Tour tour in tours)
            {
                Guide selectedGuide = FindById(tour.AssignedGuideID);
                if (selectedGuide != null)
                    selectedGuide.AssignedTours.Add(tour);
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
