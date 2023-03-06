using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Guide    //TODO Naslediti user kada se doda
    {
        private int _id;
        private string _name;
        private string _lastName;
        private string _username;  //!
        private string _password; //! Mozda hash?
        private DateTime _dateOfBirth;
        private string _phoneNumber;
        private List<Tour> _assignedTours;
        //Address address;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public List<Tour> AssignedTours
        {
            get => _assignedTours;
            set => _assignedTours = value;
        }

        public Guide()
        {
            _id = -1;
            _name = "";
            _lastName = "";
            _username = "";
            _password = "";
            _dateOfBirth = DateTime.MinValue;
            _phoneNumber = "";
            _assignedTours = new List<Tour>();
        }

        public Guide(int id, string name, string lastName, string username, string password, DateTime dateOfBirth, 
            string phoneNumber, List<Tour> assignedTours)
        {
            _id = id;
            _name = name;
            _lastName = lastName;
            _username = username; //!
            _password = password; //!
            _dateOfBirth = dateOfBirth;
            _phoneNumber = phoneNumber;
            _assignedTours = new List<Tour>();
            foreach (Tour tour in assignedTours)
            {
                _assignedTours.Add(tour);
            }
        }

        public Guide(Guide originalGuide)
        {
            _id = originalGuide.Id;
            _name = originalGuide.Name;
            _lastName = originalGuide.LastName;
            _username = originalGuide.Username;
            _password = originalGuide.Password;
            _dateOfBirth = originalGuide.DateOfBirth;
            _phoneNumber = originalGuide.PhoneNumber;
            _assignedTours = new List<Tour>();
            foreach (Tour tour in originalGuide.AssignedTours)
            {
                _assignedTours.Add(tour);
            }
        }
    }
}
