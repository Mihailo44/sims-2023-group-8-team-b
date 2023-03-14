﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    internal class OwnerDAO : ICrud<Owner>, ISubject
    {
        private readonly OwnerStorage _storage;
        private readonly List<Owner> _owners;
        private List<IObserver> _observers;

        public OwnerDAO()
        {
            _storage = new OwnerStorage();
            _owners = _storage.Load();
            _observers = new List<IObserver>();
        }
       
        public int GenerateId()
        {
            return _owners.Max(o => o.ID) + 1;
        }

        public Owner FindById(int id)
        {
            return _owners.Find(o => o.ID == id);
        }

        public Owner Create(Owner newOwner)
        {
            newOwner.ID = GenerateId();
            _owners.Add(newOwner);
            _storage.Save(_owners);
            NotifyObservers();

            return newOwner;
        }

        public Owner Update(Owner updatedOwner,int id)
        {
            Owner currentOwner = FindById(id);
            if(currentOwner == null)
                return null;

            currentOwner.FirstName = updatedOwner.FirstName;
            currentOwner.LastName = updatedOwner.LastName;
            currentOwner.FullLocation = new Location(updatedOwner.FullLocation); //(!)
            currentOwner.DateOfBirth = updatedOwner.DateOfBirth;
            currentOwner.Phone = updatedOwner.Phone;
            currentOwner.Email = updatedOwner.Email;
            currentOwner.Password = updatedOwner.Password;
            currentOwner.Username = updatedOwner.Username;
            currentOwner.Accommodations = updatedOwner.Accommodations; // ne znam da li ovo radi dobro

            _storage.Save(_owners);
            NotifyObservers();

            return currentOwner;
        }

        public Owner Delete(int id)
        {
            Owner owner = FindById(id);

            if (owner == null)
                return null;

            _owners.Remove(owner);
            _storage.Save(_owners);
            NotifyObservers();

            return owner;
        }

        public List<Owner> GetAll()
        {
            return _owners;
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
