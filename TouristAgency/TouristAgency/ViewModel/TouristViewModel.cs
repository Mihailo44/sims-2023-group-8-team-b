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
    public class TouristViewModel
    {
        private readonly TouristService _tourist;

        public TouristViewModel()
        {
            _tourist = new TouristService();
        }

        public int GenerateId()
        {
            return _tourist.GenerateId();
        }

        public Tourist FindById(int id)
        {
            return _tourist.FindById(id);
        }

        public Tourist Create(Tourist newTourist)
        {
            return _tourist.Create(newTourist);
        }

        public Tourist Update(Tourist newTourist, int id)
        {
            return _tourist.Update(newTourist, id);
        }

        public void Delete(int id)
        {
            _tourist.Delete(id);
        }

        public List<Tourist> GetAll()
        {
            return _tourist.GetAll();
        }

        public void LoadToursToTourist(List<TourTourist> tourTourists, List<Tour> tours)
        {
            _tourist.LoadToursToTourist(tourTourists, tours);
        }

        public void LoadVouchersToTourist(List<Voucher> vouchers)
        {
            _tourist.LoadVouchersToTourist(vouchers);
        }

        public Voucher FindFirstToExpire(Tourist tourist)
        {
            return _tourist.FindFirstToExpire(tourist);
        }

        public bool HasActiveVoucher(Tourist tourist)
        {
            return _tourist.HasActiveVoucher(tourist);
        }

        public void Subscribe(IObserver observer)
        {
            _tourist.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourist.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourist.NotifyObservers();
        }
    }
}
