using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Guest : User, ISerializable
    {
        private List<Reservation> _reservations;
        public Guest()
        {
            _reservations = new List<Reservation>();
        }

        public List<Reservation> Reservations
        {
            get { return _reservations; }
            set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                }
            }
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public Guest(User user) : base(user)
        {
            _reservations = new List<Reservation>();
        }


        public new void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _firstName = values[1];
            _lastName = values[2];
            _dateOfBirth = DateOnly.Parse(values[3]);
            _email = values[4];
            _phone = values[5];
            _fullLocationID = Convert.ToInt32(6);
        }

        public new string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _firstName,
                _lastName,
                _dateOfBirth.ToString(),
                _email,
                _phone,
                _fullLocationID.ToString()
            };
            return csvValues;
        }
    }
}
