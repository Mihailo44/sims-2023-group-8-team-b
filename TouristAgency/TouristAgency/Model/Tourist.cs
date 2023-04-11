using System;
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
        private List<Voucher> _wonVouchers;

        public Tourist()
        {
            UserType = Enums.UserType.TOURIST;
            _appliedTours = new List<Tour>();
            _wonVouchers = new List<Voucher>();
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

        public List<Voucher> WonVouchers
        {
            get { return _wonVouchers; }
            set
            {
                if (_wonVouchers != value)
                {
                    _wonVouchers = value;
                }
            }
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public Tourist(User user) : base(user)
        {
            UserType = Enums.UserType.TOURIST;
            _appliedTours = new List<Tour>();
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

        public int GetAgeCategory()
        {
            //0 young, 1 adult, 2 old
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            int ageDifference = today.Year - DateOfBirth.Year;
            if (ageDifference < 18)
                return 0;
            else if (ageDifference < 50)
                return 1;
            else
                return 2;
        }
    }
}
