using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class GuestReview : ISerializable
    {
        private int _id;
        private Guest _guest;
        private int _guestId;
        private DateTime _reviewDate;
        private int _cleanliness;
        private int _ruleAbiding;
        private string _comment;
       

        public GuestReview()
        {
            _id = -1;
            _reviewDate = DateTime.Now; // mozda DateTime.Today
        }

        public GuestReview(Guest guest, int cleanliness, int ruleAbiding, string comment="")
        {
            _guest = guest;
            _guestId = guest.ID;
            _reviewDate = DateTime.Now;
            _cleanliness = cleanliness;
            _ruleAbiding = ruleAbiding;
            _comment = comment;
        }

        public int Id
        {
            get => _id;
            set
            {
                if(value != _id)
                {
                    _id = value;
                }
            }
        }

        public Guest Guest
        {
            get => _guest;
            set
            {
                if(value != _guest)
                {
                    _guest = value;
                }
            }
        }

        public int GuestId
        {
            get => _guestId;
            set => _guestId = value;
        }

        public DateTime ReviewDate
        {
            get => _reviewDate;
            set
            {
                if(value != _reviewDate)
                {
                    _reviewDate = value;
                }
            }
        }

        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if(value != _cleanliness)
                {
                    _cleanliness = value;
                }
            }
        }

        public int RuleAbiding
        {
            get => _ruleAbiding; 
            set
            {
                if (value != _ruleAbiding)
                {
                    _ruleAbiding = value;
                }
            }
        }

        public string Comment
        {
            get => _comment; 
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                }
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            ReviewDate = DateTime.Parse(values[2]);
            Cleanliness = int.Parse(values[3]);
            RuleAbiding = int.Parse(values[4]);
            Comment = values[5];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                ReviewDate.ToShortDateString(), // mozda treba drugacije
                Cleanliness.ToString(),
                RuleAbiding.ToString(),
                Comment
            };

            return csvValues;
        }
    }
}
