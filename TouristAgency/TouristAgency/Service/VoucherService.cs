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
    public class VoucherService : ICrud<Voucher>, ISubject
    {
        private readonly VoucherStorage _storage;
        private readonly List<Voucher> _vouchers;
        private List<IObserver> _observers;

        public VoucherService() 
        {
            _storage = new VoucherStorage();
            _vouchers = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _vouchers.Max(v => v.ID) + 1;
        }

        public Voucher FindById(int id)
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
            Voucher currentVoucher = FindById(id);

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
            Voucher deletedVoucher = FindById(id);
            _vouchers.Remove(deletedVoucher);
            _storage.Save(_vouchers);
            NotifyObservers();
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public int GetVouchersFromTours(int tourID)
        {
            int i = 0;
            foreach(Voucher voucher in _vouchers)
            {
                if(voucher.TourID == tourID)
                {
                    i++;
                }
            }

            return i;
        }

        public void UseVoucher(Voucher voucher, int tourID)
        {
            voucher.TourID = tourID;
            voucher.IsUsed = true;
            Update(voucher, voucher.ID);
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
