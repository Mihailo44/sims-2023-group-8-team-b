using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;
using TouristAgency.Vouchers;

namespace TouristAgency.Storage.FileStorage
{
    public class VoucherFileStorage : IStorage<Voucher>
    {
        private Serializer<Voucher> _serializer;
        private readonly string _file = "vouchers.txt";

        public VoucherFileStorage()
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
