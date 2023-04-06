using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

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
        
        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
            FirstName = values[values.Length - 6]; 
            LastName = values[values.Length - 5];
            DateOfBirth = DateOnly.Parse(values[values.Length - 4]);
            FullLocationID = int.Parse(values[values.Length - 3]);
            Phone = values[values.Length - 2];
            Email = values[values.Length - 1];
        }

        public override string[] ToCSV()
        {
            string[] csvValues = base.ToCSV();

            csvValues.Append(FirstName);
            csvValues.Append(LastName);
            csvValues.Append(DateOfBirth.ToString());
            csvValues.Append(FullLocationID.ToString());
            csvValues.Append(Phone);
            csvValues.Append(Email);

            return csvValues;
        }
    }
}
