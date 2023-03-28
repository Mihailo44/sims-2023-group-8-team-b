using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ISerializable = TouristAgency.Interfaces.ISerializable;

namespace TouristAgency.Model
{
    public class OwnerReview : ISerializable
    {
        private int _id;
        private Owner _owner;
        private Accommodation _accommodation;
        private int _ownerId;
        private int _accommodationId;
        private DateTime _reviewDate;
        private int _cleanliness;
        private int _ownerCorrectness;
        private int _location;
        private int _comfort;
        private int _wifi;
        private string _comment;
        private List<Photo> _photos;

        public OwnerReview()
        {
            _id = -1;
            _cleanliness = 1;
            _ownerCorrectness = 1;
            _location = 1;
            _comfort = 1;
            _wifi = 1;
            _reviewDate = DateTime.Now;
            _photos = new List<Photo>();
        }

        public OwnerReview(Owner owner, Accommodation accommodation, int cleanliness, int ownerCorrectness, int location, int comfort, int wifi, string comment = "")
        {
            _owner = owner;
            _accommodation = accommodation;
            _ownerId = owner.ID;
            _accommodationId = accommodation.Id;
            _reviewDate = DateTime.Now;
            _cleanliness = cleanliness;
            _ownerCorrectness = ownerCorrectness;
            _location = location;
            _comfort = comfort;
            _wifi = wifi;
            _comment = comment;
        }

        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                }
            }
        }

        public Owner Owner
        {
            get => _owner;
            set
            {
                if (value != _owner)
                {
                    _owner = value;
                }
            }
        }

        public int OwnerId
        {
            get => _ownerId;
            set => _ownerId = value;
        }

        public Accommodation Accommodation
        {
            get => _accommodation;
            set
            {
                if (value != _accommodation)
                {
                    _accommodation = value;
                }
            }
        }

        public int AccommodationId
        {
            get => _accommodationId;
            set => _accommodationId = value;
        }

        public DateTime ReviewDate
        {
            get => _reviewDate;
            set
            {
                if (value != _reviewDate)
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
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                }
            }
        }

        public int OwnerCorrectness
        {
            get => _ownerCorrectness;
            set
            {
                if (value != _ownerCorrectness)
                {
                    _ownerCorrectness = value;
                }
            }
        }

        public int Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _cleanliness = value;
                }
            }
        }

        public int Comfort
        {
            get => _comfort;
            set
            {
                if (value != _comfort)
                {
                    _comfort = value;
                }
            }
        }

        public int Wifi
        {
            get => _wifi;
            set
            {
                if (value != _wifi)
                {
                    _wifi = value;
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

        public List<Photo> Photos
        {
            get => _photos;
            set => _photos = value;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            OwnerId = int.Parse(values[1]);
            AccommodationId = int.Parse(values[2]);
            ReviewDate = DateTime.Parse(values[3]);
            Cleanliness = int.Parse(values[4]);
            OwnerCorrectness = int.Parse(values[5]);
            Location = int.Parse(values[6]);
            Comfort = int.Parse(values[7]);
            Wifi = int.Parse(values[8]);
            Comment = values[9];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                OwnerId.ToString(),
                AccommodationId.ToString(),
                ReviewDate.ToShortDateString(), 
                Cleanliness.ToString(),
                OwnerCorrectness.ToString(),
                Location.ToString(),
                Comfort.ToString(),
                Wifi.ToString(),
                Comment
            };

            return csvValues;
        }
    }
}
