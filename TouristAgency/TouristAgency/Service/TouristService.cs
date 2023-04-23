using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Storage.FileStorage;

namespace TouristAgency.Service
{
    public class TouristService
    {
        private readonly App _app;
        private TouristRepository _touristRepository;

        public TouristService()
        {
            _app = (App)App.Current;
            _touristRepository = _app.TouristRepository;
        }

        public List<Voucher> GetValidVouchers(Tourist tourist)
        {
            DateTime nowDateTime = DateTime.Now;
            return tourist.WonVouchers.Where(v => v.ExpirationDate > nowDateTime && v.IsUsed == false).ToList();
        }

        public Voucher FindFirstToExpire(Tourist tourist)
        {
            DateTime min = DateTime.MaxValue;
            Voucher foundVoucher = new Voucher();
            foreach (Voucher voucher in tourist.WonVouchers)
            {
                if(voucher.ExpirationDate < min && voucher.IsUsed == false)
                {
                    min = voucher.ExpirationDate;
                    foundVoucher = voucher;
                }
            }

            return foundVoucher;
        }

        public bool HasActiveVoucher(Tourist tourist)
        {
            DateTime nowDateTime = DateTime.Now;
            foreach(Voucher voucher in tourist.WonVouchers)
            {
                if(voucher.ExpirationDate > nowDateTime && voucher.IsUsed == false)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
