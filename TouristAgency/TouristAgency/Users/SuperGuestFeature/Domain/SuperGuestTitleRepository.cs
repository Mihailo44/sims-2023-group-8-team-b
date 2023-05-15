using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Users.SuperGuestFeature.Domain
{
    public class SuperGuestTitleRepository : ICrud<SuperGuestTitle>, ISubject
    {
        private readonly IStorage<SuperGuestTitle> _storage;
        private readonly List<SuperGuestTitle> _superGuestTitles;
        private List<IObserver> _observers;

        public SuperGuestTitleRepository(IStorage<SuperGuestTitle> storage)
        {
            _storage = storage;
            _superGuestTitles = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _superGuestTitles.Count() == 0 ? 0 : _superGuestTitles.Max(s => s.Id) + 1;
        }

        public SuperGuestTitle GetById(int id)
        {
            return _superGuestTitles.Find(s => s.Id == id);
        }

        public SuperGuestTitle Create(SuperGuestTitle newSuperGuestTitle)
        {
            newSuperGuestTitle.Id = GenerateId();
            _superGuestTitles.Add(newSuperGuestTitle);
            _storage.Save(_superGuestTitles);
            NotifyObservers();

            return newSuperGuestTitle;
        }

        public SuperGuestTitle Update(SuperGuestTitle updatedSuperGuestTitle, int id)
        {
            SuperGuestTitle currentSuperGuestTitle = GetById(id);

            if (currentSuperGuestTitle == null)
            {
                return null;
            }

            currentSuperGuestTitle.NumOfReservations = updatedSuperGuestTitle.NumOfReservations;
            currentSuperGuestTitle.Points = updatedSuperGuestTitle.Points;
            currentSuperGuestTitle.GuestId = updatedSuperGuestTitle.GuestId;
            currentSuperGuestTitle.Guest = updatedSuperGuestTitle.Guest;
            currentSuperGuestTitle.LastUpdated = updatedSuperGuestTitle.LastUpdated;

            _storage.Save(_superGuestTitles);
            NotifyObservers();

            return currentSuperGuestTitle;
        }

        public void Delete(int id)
        {
            SuperGuestTitle deletedSuperGuestTitle = GetById(id);
            _superGuestTitles.Remove(deletedSuperGuestTitle);
            _storage.Save(_superGuestTitles);
            NotifyObservers();
        }

        public List<SuperGuestTitle> GetAll()
        {
            return _superGuestTitles;
        }

        public void LoadGuestsToSuperGuestTitles(List<Guest> guests)
        {
            foreach (var superGuestTitle in _superGuestTitles)
            {
                Guest guest = guests.Find(g => g.ID == superGuestTitle.GuestId);
                if (guest != null)
                {
                    superGuestTitle.Guest = guest;
                }
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
