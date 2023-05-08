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

        public void UseVoucher(Voucher voucher, int tourID)
        {
            voucher.TourID = tourID;
            voucher.IsUsed = true;
            VoucherRepository.Update(voucher, voucher.ID);
        }
    }
}
