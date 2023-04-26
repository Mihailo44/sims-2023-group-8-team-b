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
        public VoucherRepository VoucherRepository { get; }

        public VoucherService() 
        {
            _app = (App)App.Current;
            VoucherRepository = _app.VoucherRepository;
        }

        public int GetVouchersFromTours(int tourID)
        {
            int i = 0;
            foreach(Voucher voucher in VoucherRepository.GetAll())
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
            VoucherRepository.Update(voucher, voucher.ID);
        }
    }
}
