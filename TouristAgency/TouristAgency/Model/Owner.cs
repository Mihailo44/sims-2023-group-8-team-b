using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Serialization;

namespace TouristAgency.Model
{
    public class Owner : User,ISerializable
    {
        private int _id;
        private bool _superOwner;
        private double _average;
        private List<Accommodation> _accommodations;
        
        public Owner()
        {
            _id = -1;
            _superOwner = false;
            _accommodations = new List<Accommodation>();
        }

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
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

        public List<Accommodation> Accommodations // ne znam da li moze ovako
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
            Id = int.Parse(values[0]);
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
                Id.ToString(),
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
