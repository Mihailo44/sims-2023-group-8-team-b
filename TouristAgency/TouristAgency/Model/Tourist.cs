using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Tourist
    {
        private int _touristID;
        private string _username;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _email;
        private Address _address;
        private string _phone;
        
        public Tourist()
        { 
        
        }

        public int TouristID
        { 
            get { return _touristID; }
            set { _touristID = value; }
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

        public string FirstName
        { 
            get { return _firstName; } 
            set {  _firstName = value; } 
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        { 
            get { return _email; }
            set { _email = value; } 
        }

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
    }
}
