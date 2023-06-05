using System.Collections.Generic;
using System.Windows.Documents;

namespace TouristAgency.Vouchers
{
    public class VoucherService
    {
        private readonly App _app;
        public VoucherRepository VoucherRepository { get; }

        public VoucherService()
        {
            _app = (App)System.Windows.Application.Current;
            VoucherRepository = _app.VoucherRepository;
        }

        public Voucher Create(Voucher voucher)
        {
            return VoucherRepository.Create(voucher);
        }

        public List<Voucher> GetAll() 
        {
            return VoucherRepository.GetAll();
        }

        public Voucher Update(Voucher voucher, int voucherID) 
        {
            return VoucherRepository.Update(voucher, voucherID);
        }

        public void Delete(int voucherID) 
        {
            VoucherRepository.Delete(voucherID);
        }

        public int GetVouchersFromTours(int tourID)
        {
            int i = 0;
            foreach (Voucher voucher in VoucherRepository.GetAll())
            {
                if (voucher.TourID == tourID)
                {
                    i++;
                }
            }

            return i;
        }

        public Voucher WinVoucher(int touristID, int tourID, int numOfReservations)
        {
            if(numOfReservations == 5) 
            {
                Voucher voucher = new Voucher(touristID, tourID, "Won voucher", false, System.DateTime.Now.AddMonths(6));
                Create(voucher);
                return voucher;
            }
            return null;
        }

        public void UseVoucher(Voucher voucher, int tourID)
        {
            voucher.TourID = tourID;
            voucher.IsUsed = true;
            VoucherRepository.Update(voucher, voucher.ID);
        }

        public List<Voucher> GetByTouristID(int touristID) 
        {
            return GetAll().FindAll(v => v.TouristID == touristID);
        }
    }
}
