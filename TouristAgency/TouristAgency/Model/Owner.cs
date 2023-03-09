using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Owner : User
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
    }
}
