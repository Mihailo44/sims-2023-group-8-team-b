using System;
using System.Collections.Generic;
using TouristAgency.Interfaces;
using TouristAgency.Model.Enums;

namespace TouristAgency.Model
{
    public class Owner : User, ISerializable
    {
        private bool _superOwner;
        private double _average;
        private List<Accommodation> _accommodations;

        public Owner() : base()
        {
            _accommodations = new List<Accommodation>();
        }

        public Owner(string username, string password, string firstName, string lastName, DateOnly dateOfBirth, string email, Location location, string phone, UserType userType) :
            base(username, password, firstName, lastName, dateOfBirth, email, location, phone, userType)
        {
            _accommodations = new List<Accommodation>();
        }

        public bool SuperOwner
        {
            get => _superOwner;
            set
            {
                if (value != _superOwner)
                {
                    _superOwner = value;
                }
            }
        }

        public double Average
        {
            get => _average;
            set
            {
                if (value != _average)
                {
                    _average = value;
                }
            }
        }

        public List<Accommodation> Accommodations
        {
            get => _accommodations;
            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                }
            }
        }

        new public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            FirstName = values[1];
            LastName = values[2];
            DateOfBirth = DateOnly.Parse(values[3]);
            FullLocationID = int.Parse(values[4]);
            Phone = values[5];
            Email = values[6];
            SuperOwner = bool.Parse(values[7]);
        }


        new public string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                FirstName,
                LastName,
                DateOfBirth.ToString(),
                FullLocationID.ToString(),
                Phone.ToString(),
                Email.ToString(),
                SuperOwner.ToString()
            };

            return csvValues;
        }
    }
}
