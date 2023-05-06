using System;
using System.Collections.Generic;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Users.Domain;

namespace TouristAgency.Users
{
    public class Guide : User, ISerializable

    {
        private List<Tour> _assignedTours;

        public List<Tour> AssignedTours
        {
            get { return _assignedTours; }
            set
            {
                if (value != _assignedTours)
                {
                    _assignedTours = value;
                }
            }
        }

        public Guide()
        {
            UserType = Base.UserType.GUIDE;
            _assignedTours = new List<Tour>();
        }

        public Guide(User user) : base(user)
        {
            UserType = Base.UserType.GUIDE;
            _assignedTours = new List<Tour>();
        }

        new public string[] ToCSV()
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

        new public void FromCSV(string[] values)
        {
            _ID = Convert.ToInt32(values[0]);
            _firstName = values[1];
            _lastName = values[2];
            _dateOfBirth = DateOnly.Parse(values[3]);
            _email = values[4];
            _phone = values[5];
            _fullLocationID = Convert.ToInt32(6);
        }
    }
}
