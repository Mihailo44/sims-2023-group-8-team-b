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
        
        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            FirstName = values[1];
            LastName = values[2];
            DateOfBirth = DateOnly.Parse(values[3]);
            FullLocationID = int.Parse(values[4]);
            Phone = values[5];
            Email = values[6];
            Username = values[7];
            Password = values[8];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                FirstName,
                LastName,
                DateOfBirth.ToString(),
                FullLocationID.ToString(),
                Phone,
                Email,
                Username,
                Password
            };

            return csvValues;
        }
    }
}
