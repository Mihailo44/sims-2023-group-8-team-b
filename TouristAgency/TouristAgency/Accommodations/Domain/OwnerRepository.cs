using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;
using TouristAgency.Users.Domain;
using TouristAgency.Util;

namespace TouristAgency.Accommodations.Domain
{
    public class OwnerRepository : ICrud<Owner>, ISubject
    {
        private readonly IStorage<Owner> _storage;
        private readonly List<Owner> _owners;
        private List<IObserver> _observers;

        public OwnerRepository(IStorage<Owner> storage)
        {
            _storage = storage;
            _owners = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if (_owners.Count == 0)
                return 0;
            else
                return _owners.Max(o => o.ID) + 1;
        }

        public Owner GetById(int id)
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

        public Owner Update(Owner updatedOwner, int id)
        {
            Owner currentOwner = GetById(id);
            if (currentOwner == null)
                return null;

            currentOwner.FirstName = updatedOwner.FirstName;
            currentOwner.LastName = updatedOwner.LastName;
            currentOwner.DateOfBirth = updatedOwner.DateOfBirth;
            currentOwner.Phone = updatedOwner.Phone;
            currentOwner.Email = updatedOwner.Email;
            currentOwner.Password = updatedOwner.Password;
            currentOwner.Username = updatedOwner.Username;
            currentOwner.Accommodations = updatedOwner.Accommodations;
            currentOwner.SuperOwner = updatedOwner.SuperOwner;

            _storage.Save(_owners);
            NotifyObservers();

            return currentOwner;
        }

        public void Delete(int id)
        {
            Owner owner = GetById(id);
            _owners.Remove(owner);
            _storage.Save(_owners);
            NotifyObservers();
        }

        public List<Owner> GetAll()
        {
            return _owners;
        }

        public void LoadAccommodationsToOwners(List<Accommodation> accommodations)
        {
            foreach (var accommodation in accommodations)
            {
                Owner owner = _owners.Find(o => o.ID == accommodation.Owner.ID);
                if (owner != null)
                {
                    owner.Accommodations.Add(accommodation);
                    accommodation.Owner = owner;
                }
            }
        }

        public void LoadLocationsToOwners(List<Location> locations)
        {
            foreach (var owner in _owners)
            {
                owner.FullLocation = locations.Find(l => l.Id == owner.FullLocationID);
            }
        }

        public void LoadOwnerCredentials(List<User> users)
        {
            foreach(var owner in _owners)
            {
                User user = users.Find(u => u.ID == owner.ID);
                if(user != null)
                {
                    owner.Username = user.Username;
                    owner.Password = user.Password;
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
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
