using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Model
{
    public class GuideReview : ISerializable
    {
        private int _id;
        private Tour _tour;
        private int _tourID;
        private Tourist _tourist;
        private int _touristID;
        private DateTime _reviewDate;
        private int _quality;
        private int _tourOrganization;
        private int _attractions;
        private int _knowledge;
        private int _language;
        private int _socialInteraction;
        private string _comment;
        private List<Photo> _photos;

        public GuideReview()
        {
            _id = -1;
            _tourID = 1;
            _touristID = -1;
            _reviewDate = DateTime.Now;
            _quality = 1;
            _tourOrganization = 1;
            _attractions = 1;
            _knowledge = 1;
            _language = 1;
            _socialInteraction = 1;
            _photos = new List<Photo>();
        }

        public GuideReview(int id, Tour tour, int tourID, DateTime reviewDate, int quality, int tourOrganization, int attractions, int knowledge, int language, int socialInteraction, string comment, List<Photo> photos)
        {
            _id = id;
            _tour = tour;
            _tourID = tourID;
            _touristID = -1;
            _reviewDate = reviewDate;
            _quality = quality;
            _tourOrganization = tourOrganization;
            _attractions = attractions;
            _knowledge = knowledge;
            _language = language;
            _socialInteraction = socialInteraction;
            _comment = comment;
            _photos = photos;
        }

        public int ID
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

        public Tour Tour
        {
            get => _tour;
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                }
            }
        }

        public int TourID
        {
            get => _tourID;
            set
            {
                if (value != _tourID)
                {
                    _tourID = value;
                }
            }
        }

        public Tourist Tourist
        {
            get => _tourist;
            set
            {
                if(value != _tourist)
                {
                    _tourist = value;
                }
            }
        }

        public int TouristID
        {
            get => _touristID;
            set
            {
                if(value != _touristID)
                {
                    _touristID = value;
                }
            }
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

        public int Quality
        {
            get => _quality;
            set
            {
                if (value != _quality)
                {
                    _quality = value;
                }
            }
        }

        public int TourOrganization
        {
            get => _tourOrganization;
            set
            {
                if (value != _tourOrganization)
                {
                    _tourOrganization = value;
                }
            }
        }

        public int Attractions
        {
            get => _attractions;
            set
            {
                if (value != _attractions)
                {
                    _attractions = value;
                }
            }
        }

        public int Knowledge
        {
            get => _knowledge;
            set
            {
                if (value != _knowledge)
                {
                    _knowledge = value;
                }
            }
        }

        public int Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                }
            }
        }

        public int SocialInteraction
        {
            get => _socialInteraction;
            set
            {
                if (value != _socialInteraction)
                {
                    _socialInteraction = value;
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
            ID = int.Parse(values[0]);
            TourID = int.Parse(values[1]);
            TouristID = int.Parse(values[2]);
            ReviewDate = DateTime.Parse(values[3]);
            Quality = int.Parse(values[4]);
            TourOrganization = int.Parse(values[5]);
            Attractions = int.Parse(values[6]);
            Knowledge = int.Parse(values[7]);
            Language = int.Parse(values[8]);
            SocialInteraction = int.Parse(values[9]);
            Comment = values[10];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ID.ToString(),
                TourID.ToString(),
                TouristID.ToString(),
                ReviewDate.ToString(),
                Quality.ToString(),
                TourOrganization.ToString(),
                Attractions.ToString(),
                Knowledge.ToString(),
                Language.ToString(),
                SocialInteraction.ToString(),
                Comment
            };

            return csvValues;
        }
    }
}
