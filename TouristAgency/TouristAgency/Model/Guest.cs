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

        public Guest(User user) : base(user)
        {
            _reservations = new List<Reservation>();
        }


        public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _username = values[1];
            _password = values[2];
            _firstName = values[3];
            _lastName = values[4];
            _dateOfBirth = DateOnly.Parse(values[5]);
            _email = values[6];
            _phone = values[7];
            _addressID = Convert.ToInt32(8);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _ID.ToString(),
                _username,
                _password,
                _firstName,
                _lastName,
                _dateOfBirth.ToString(),
                _email,
                _phone,
                _addressID.ToString()
            };
            return csvValues;
        }
    }
}
