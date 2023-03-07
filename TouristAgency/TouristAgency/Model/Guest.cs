using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Guest
    {
        private int _guestID;
        private string _name;
        private string _lastName;
        private string _username;
        private string _password;
        private string _email;
        private string _phoneNumber;
        private Address _address;
        private DateTime _dateOfBirth;

        public Guest()
        {

        }

        public int GuestID
        {
            get { return _guestID; }
            set { _guestID = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
    }
}
