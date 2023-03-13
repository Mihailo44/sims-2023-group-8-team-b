﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Tourist : User, ISerializable
    {
        private List<Tour> _appliedTours;

        public Tourist()
        {
            _appliedTours = new List<Tour>();
        }

        public List<Tour> AppliedTours
        { 
            get { return _appliedTours; }
            set
            { 
                if(_appliedTours != value) 
                {
                    _appliedTours = value;
                }
            }
        }

        public Tourist(User user) : base(user)
        {
            _appliedTours = new List<Tour>();
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
    }
}
