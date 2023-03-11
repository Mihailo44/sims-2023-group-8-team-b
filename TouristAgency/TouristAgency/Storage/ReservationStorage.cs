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
        private Serializer<Reservation> serializer;
        private readonly string file = "reservations.txt";

        public ReservationStorage()
        {
            serializer = new Serializer<Reservation>();
        }

        public List<Reservation> Load()
        {
            return serializer.FromCSV(file);
        }

        public void Save(List<Reservation> reservations)
        {
            serializer.ToCSV(file, reservations);
        }
    }
}
