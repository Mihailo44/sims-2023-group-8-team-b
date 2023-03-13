using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class TouristDAO : ICrud, ISubject
    {
        private readonly TouristStorage _storage;
        private readonly List<Tourist> _tourists;
        private List<IObserver> _observers;

        public TouristDAO()
        {
            _storage = new TouristStorage();
            _tourists = new List<Tourist>();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _tourists.Max(t => t.ID);
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
            currentTourist.Address = newTourist.Address; //! -||-?
            currentTourist.Phone = newTourist.Phone;

            return currentTourist;
        }

        public Tourist Delete(int id)
        {
            Tourist deletedTourist = FindById(id);

            if (deletedTourist == null)
            {
                return null;
            }

            _tourists.Remove(deletedTourist);
            _storage.Save(_tourists);
            NotifyObservers();

            return deletedTourist; //TODO VOID
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
