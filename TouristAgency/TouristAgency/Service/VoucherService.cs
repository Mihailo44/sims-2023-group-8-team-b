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
    public class VoucherService
    {
        private readonly App _app;
        private VoucherRepository _voucherRepository;
        public VoucherService(IStorage<Voucher> storage) 
        {
            _app = (App)App.Current;
            _voucherRepository = _app.VoucherRepository;
        }

        public int GetVouchersFromTours(int tourID)
        {
            int i = 0;
            foreach(Voucher voucher in _voucherRepository.GetAll())
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
            _voucherRepository.Update(voucher, voucher.ID);
        }
    }
}
