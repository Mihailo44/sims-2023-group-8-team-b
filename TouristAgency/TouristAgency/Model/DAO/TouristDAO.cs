using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class TouristDAO : ICrud<Tourist>, ISubject
    {
        private readonly TouristStorage _storage;
        private readonly List<Tourist> _tourists;
        private List<IObserver> _observers;

        public TouristDAO()
        {
            _storage = new TouristStorage();
            _tourists = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _tourists.Max(t => t.ID) + 1;
        }

        public Tourist FindById(int id)
        {
            return _tourists.Find(t => t.ID == id);
        }

        public Tourist Create(Tourist newTourist)
        {
            newTourist.ID = GenerateId();
            _tourists.Add(newTourist);
            _storage.Save(_tourists);
            NotifyObservers();

            return newTourist;
        }

        public Tourist Update(Tourist newTourist, int id)
        {
            Tourist currentTourist = FindById(id);

            if (currentTourist == null)
            {
                return null;
            }

            currentTourist.Username = newTourist.Username;
            currentTourist.Password = newTourist.Password;
            currentTourist.FirstName = newTourist.FirstName;
            currentTourist.LastName = newTourist.LastName;
            currentTourist.DateOfBirth = newTourist.DateOfBirth; //! Duboka kopija?
            currentTourist.Email = newTourist.Email;
            currentTourist.FullLocation = newTourist.FullLocation; //! -||-?
            currentTourist.Phone = newTourist.Phone;

            return currentTourist;
        }

        public void Delete(int id)
        {
            Tourist deletedTourist = FindById(id);
            _tourists.Remove(deletedTourist);
            _storage.Save(_tourists);
            NotifyObservers();
        }

        public List<Tourist> GetAll()
        {
            return _tourists;
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
