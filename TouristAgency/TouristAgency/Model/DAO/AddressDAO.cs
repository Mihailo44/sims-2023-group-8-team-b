using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class AddressDAO : ICrud, ISubject
    {
        private readonly AddressStorage _storage;
        private readonly List<Address> _addresses;
        private List<IObserver> _observers;

        public AddressDAO()
        {
            _storage = new AddressStorage();
            _addresses = new List<Address>();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _addresses.Max(a => a.ID);
        }

        public Address FindById(int id)
        {
            return _addresses.Find(a => a.ID == id);
        }

        public Address Create(Address newAddress)
        {
            newAddress.ID = GenerateId();
            _addresses.Add(newAddress);
            _storage.Save(_addresses);
            NotifyObservers();

            return newAddress;
        }

        public Address Update(Address newAddress, int id)
        {
            Address currentAddress = FindById(id);

            if (currentAddress == null)
            {
                return null;
            }

            currentAddress.ID = newAddress.ID;
            currentAddress.Street = newAddress.Street;
            currentAddress.StreetNumber = newAddress.StreetNumber;
            currentAddress.City = newAddress.City;
            currentAddress.Country = newAddress.Country;

            return currentAddress;
        }

        public Address Delete(int id)
        {
            Address deletedAddress = FindById(id);

            if (deletedAddress == null)
            {
                return null;
            }

            _addresses.Remove(deletedAddress);
            _storage.Save(_addresses);
            NotifyObservers();

            return deletedAddress; //TODO VOID
        }

        public List<Address> GetAll()
        {
            return _addresses;
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
