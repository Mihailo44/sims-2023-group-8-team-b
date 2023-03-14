using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class Owner : User,ISerializable
    {
        private bool _superOwner;
        private double _average;
        private List<Accommodation> _accommodations;
        
        public Owner(): base()
        {
            _superOwner = false;
            _accommodations = new List<Accommodation>();
        }

        public Owner(string username,string password,string firstName,string lastName,DateOnly dateOfBirth,string email,Location location,string phone) : 
            base(username,password,firstName,lastName,dateOfBirth,email,location,phone)
        {
            _accommodations = new List<Accommodation>();   
        }

        public bool SuperOwner
        {
            get => _superOwner;
            set
            {
                if(value != _superOwner)
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
                if(value != _average)
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
        //Dodati location
        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[1]);
            FirstName = values[1];
            LastName = values[2];
            DateOfBirth = DateOnly.Parse(values[3]);
            Phone = values[3];
            Email = values[4];
            Username = values[5];
            Password = values[6];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                FirstName,
                LastName,
                DateOfBirth.ToString(),
                Phone,
                Email,
                Username,
                Password
            };

            return csvValues;
        }
    }
}
