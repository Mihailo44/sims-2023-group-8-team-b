﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Storage;
using TouristAgency.Interfaces;

namespace TouristAgency.Model.DAO
{
    internal class AccommodationDAO : ICrud<Accommodation>,ISubject
    {
        private readonly AccommodationStorage _storage;
        private readonly List<Accommodation> _accommodations;
        private List<IObserver> _observers;

        public AccommodationDAO()
        {
            _storage = new AccommodationStorage();
            _accommodations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _accommodations.Max(a => a.Id) + 1;
        }

        public Accommodation FindById(int id)
        {
            return _accommodations.Find(a => a.Id == id);
        }

        public Accommodation Create(Accommodation newAccommodation)
        {
            newAccommodation.Id = GenerateId();
            _accommodations.Add(newAccommodation);
            _storage.Save(_accommodations);
            NotifyObservers();

            return newAccommodation;
        }

        public Accommodation Update(Accommodation updatedAccommodation,int id)
        {
            Accommodation currentAccommodation = FindById(id);
            if(currentAccommodation == null)
                return null;

            currentAccommodation.OwnerId = updatedAccommodation.OwnerId;
            currentAccommodation.Owner = updatedAccommodation.Owner; //proveriti
            currentAccommodation.Name = updatedAccommodation.Name;
            currentAccommodation.MaxGuestNum = updatedAccommodation.MaxGuestNum;
            currentAccommodation.MinNumOfDays = updatedAccommodation.MinNumOfDays;
            currentAccommodation.AllowedNumOfDaysForCancelation = updatedAccommodation.AllowedNumOfDaysForCancelation;

            _storage.Save(_accommodations);
            NotifyObservers();

            return currentAccommodation;

        }

        public void Delete(int id)
        {
            Accommodation accommodation = FindById(id);
            _accommodations.Remove(accommodation);
            _storage.Save(_accommodations);
            NotifyObservers();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
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
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

    }
}
