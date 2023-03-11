using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;
using TouristAgency.Model;

namespace TouristAgency.Storage
{
    public class ReservationStorage
    {
        private Serializer<Reservation> _serializer;
        private readonly string _file = "reservations.txt";

        public ReservationStorage()
        {
            _serializer = new Serializer<Reservation>();
        }

        public List<Reservation> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Reservation> reservations)
        {
            _serializer.ToCSV(_file, reservations);
        }
    }
}
