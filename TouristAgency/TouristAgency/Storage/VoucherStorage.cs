using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Serialization;

namespace TouristAgency.Storage
{
    public class VoucherStorage
    {
        private Serializer<Voucher> _serializer;
        private readonly string _file = "vouchers.txt";

        public VoucherStorage()
        {
            _serializer = new Serializer<Voucher>();
        }

        public List<Voucher> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(_file, vouchers);
        }
    }
}
