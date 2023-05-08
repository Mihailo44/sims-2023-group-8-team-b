using System;
using System.Collections.Generic;
using System.Linq;
using TouristAgency.Vouchers;

namespace TouristAgency.Users
{
    public class TouristService
    {
        private readonly App _app;
        public TouristRepository TouristRepository { get; }

        public TouristService()
        {
            _app = (App)System.Windows.Application.Current;
            TouristRepository = _app.TouristRepository;
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
                if (voucher.ExpirationDate < min && voucher.IsUsed == false)
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
            foreach (Voucher voucher in tourist.WonVouchers)
            {
                if (voucher.ExpirationDate > nowDateTime && voucher.IsUsed == false)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
