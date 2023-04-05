using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class VoucherViewModel
    {
        private readonly VoucherService _voucher;

        public VoucherViewModel()
        {
            _voucher = new VoucherService();
        }

        public int GenerateId()
        {
            return _voucher.GenerateId();
        }

        public Voucher FindById(int id)
        {
            return _voucher.FindById(id);
        }

        public Voucher Create(Voucher newVoucher)
        {
            return _voucher.Create(newVoucher);
        }

        public Voucher Update(Voucher newVoucher, int id)
        {
            return _voucher.Update(newVoucher, id);
        }

        public void Delete(int id)
        {
            _voucher.Delete(id);
        }

        public List<Voucher> GetAll()
        {
            return _voucher.GetAll();
        }

        public int GetVouchersFromTours(int tourID)
        {
            return _voucher.GetVouchersFromTours(tourID);
        }

        public void UseVoucher(Voucher voucher, int tourID)
        {
            _voucher.UseVoucher(voucher, tourID);
        }

        public void Subscribe(IObserver observer)
        {
            _voucher.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _voucher.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _voucher.NotifyObservers();
        }
    }
}
