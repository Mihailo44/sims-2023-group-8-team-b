using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model.Enums;

namespace TouristAgency.Model
{
    public class Owner : User
    {
        private bool _superOwner;
        private double _average;
        private List<Accommodation> _accommodations;
        
        public Owner(): base()
        {
            _accommodations = new List<Accommodation>();
        }

        public Owner(string username,string password,string firstName,string lastName,DateOnly dateOfBirth,string email,Location location,string phone,UserType userType) : 
            base(username,password,firstName,lastName,dateOfBirth,email,location,phone,userType)
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
        
        public new void FromCSV(string[] values)
        {
            ID = int.Parse(values[values.Length - 7]);
            FirstName = values[values.Length - 6];
            LastName = values[values.Length - 5];
            DateOfBirth = DateOnly.Parse(values[values.Length - 4]);
            FullLocationID = int.Parse(values[values.Length - 3]);
            Phone = values[values.Length - 2];
            Email = values[values.Length - 1];
        }

        public new string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                FirstName,
                LastName,
                DateOfBirth.ToString(),
                FullLocationID.ToString(),
                Phone.ToString(),
                Email.ToString()
            };

            return csvValues;
        }
    }
}
