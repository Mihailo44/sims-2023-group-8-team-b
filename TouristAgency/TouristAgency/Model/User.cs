using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    public class User
    {
        protected int _ID;
        protected string _username;
        protected string _password;
        protected string _firstName;
        protected string _lastName;
        protected DateOnly _dateOfBirth;
        protected string _email;
        protected Address _address;
        protected int _addressID;
        protected string _phone;

        public User()
        {
            _ID = -1;
            _dateOfBirth = DateOnly.MinValue;
            _address = new Address();
        }

        public User(string username, string password, string firstName, string lastName, DateOnly dateOfBirth, string email, Address address, string phone)
        {
            _username = username;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _dateOfBirth = dateOfBirth;
            _email = email;
            _address = new Address(address);
            _phone = phone;
        }

        public User(User originalUser)
        {
            _ID = originalUser.ID;
            _username = originalUser.Username;
            _password = originalUser.Password;
            _firstName = originalUser.FirstName;
            _lastName = originalUser.LastName;
            _dateOfBirth = originalUser.DateOfBirth; //!
            _email = originalUser.Email;
            _address = new Address(originalUser.Address);
            _phone = originalUser.Phone;
        }

        public int ID
        {
            get { return _ID; }
            set 
            { 
                if(value != _ID) 
                {
                    _ID = value;
                }
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                }
            }
        }

        public DateOnly DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value != _dateOfBirth)
                {
                    _dateOfBirth = value;
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                }
            }
        }

        public Address Address
        {
            get { return _address; }
            set
            {
                if (value != _address)
                {
                    _address = value;
                }
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value != _phone)
                {
                    _phone = value;
                }
            }
        }
    }
}
