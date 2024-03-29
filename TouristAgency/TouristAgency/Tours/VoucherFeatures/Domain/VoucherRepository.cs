﻿using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;

namespace TouristAgency.Vouchers
{
    public class VoucherRepository : ICrud<Voucher>, ISubject
    {
        private readonly IStorage<Voucher> _storage;
        private readonly List<Voucher> _vouchers;
        private List<IObserver> _observers;

        public VoucherRepository(IStorage<Voucher> storage)
        {
            _storage = storage;
            _vouchers = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _vouchers.Max(v => v.ID) + 1;
        }

        public Voucher GetById(int id)
        {
            return _vouchers.Find(v => v.ID == id);
        }

        public Voucher Create(Voucher newVoucher)
        {
            newVoucher.ID = GenerateId();
            _vouchers.Add(newVoucher);
            _storage.Save(_vouchers);
            NotifyObservers();

            return newVoucher;
        }

        public Voucher Update(Voucher newVoucher, int id)
        {
            Voucher currentVoucher = GetById(id);

            if (currentVoucher == null)
            {
                return null;
            }

            currentVoucher.TouristID = currentVoucher.TouristID;
            currentVoucher.TourID = newVoucher.TourID;
            currentVoucher.Name = newVoucher.Name;
            currentVoucher.IsUsed = currentVoucher.IsUsed;
            currentVoucher.ExpirationDate = currentVoucher.ExpirationDate;

            _storage.Save(_vouchers);
            NotifyObservers();

            return currentVoucher;
        }

        public void Delete(int id)
        {
            Voucher deletedVoucher = GetById(id);
            _vouchers.Remove(deletedVoucher);
            _storage.Save(_vouchers);
            NotifyObservers();
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
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
