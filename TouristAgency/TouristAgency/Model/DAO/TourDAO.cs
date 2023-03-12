using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    internal class TourDAO : ICrud<Tour>, ISubject
    {
        private readonly TourStorage _storage;
        private readonly List<Tour> _tours;
        private List<IObserver> _observers;

        public TourDAO()
        {
            _storage = new TourStorage();
            _tours = new List<Tour>();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _tours.Max(t => t.Id);
        }

        public Tour FindById(int id)
        {
            return _tours.Find(t => t.Id == id);
        }

        public Tour Create(Tour newTour)
        {
            newTour.Id = GenerateId();
            _tours.Add(newTour);
            _storage.Save(_tours);
            NotifyObservers();
            return newTour;
        }

        public Tour Update(Tour newTour, int id)
        {
            Tour updatedTour = FindById(id);
            if (updatedTour == null)
            {
                return null;
            }
            updatedTour.Name = newTour.Name;
            updatedTour.Description = newTour.Description;
            updatedTour.Location = newTour.Location;
            updatedTour.Language = newTour.Language;
            updatedTour.MaxAttendants = newTour.MaxAttendants;
            updatedTour.Duration = newTour.Duration;
            updatedTour.StartDate = newTour.StartDate; //! Mozda mora new!
            //TODO liste, kada napravis update formu
            //updatedTour.GuideID = ..
            //slike...
            NotifyObservers();
            return updatedTour;
        }

        public Tour Delete(int id)
        {
            Tour deletedTour = FindById(id);
            if (deletedTour == null)
            {
                return null;
            }
            _tours.Remove(deletedTour);
            _storage.Save(_tours);
            NotifyObservers();
            return deletedTour; //TODO VOID
        }

        public List<Tour> GetAll()
        {
            return _tours;
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
