using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Storage;
using TouristAgency.Interfaces;

namespace TouristAgency.Service
{
    public class AccommodationService
    {
        private readonly List<Accommodation> _accommodations;

        public AccommodationService(List<Accommodation> accommodations)
        {
            _accommodations = accommodations;
        }

        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            names.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!names.Contains(accommodation.Name) && accommodation.Name != "")
                {
                    names.Add(accommodation.Name);
                }
            }

            return names;
        }

        public List<string> GetCities()
        {
            List<string> citites = new List<string>();
            citites.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!citites.Contains(accommodation.Location.City) && accommodation.Location.City != "")
                {
                    citites.Add(accommodation.Location.City);
                }
            }

            return citites;
        }

        public List<string> GetCountries()
        {
            List<string> countries = new List<string>();
            countries.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!countries.Contains(accommodation.Location.Country) && accommodation.Location.Country != "")
                {
                    countries.Add(accommodation.Location.Country);
                }
            }

            return countries;
        }

        public List<string> GetTypes()
        {
            List<string> types = new List<string>();
            types.Add(string.Empty);

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!types.Contains(accommodation.Type.ToString()) && accommodation.Type.ToString() != "")
                {
                    types.Add(accommodation.Type.ToString());
                }
            }

            return types;
        }

        public void LoadLocationsToAccommodations(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.LocationId == location.Id)
                    {
                        accommodation.Location = new Location(location);
                    }
                }
            }
        }

        public void LoadPhotosToAccommodations(List<Photo> photos)
        {
            foreach (Photo photo in photos)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.Id == photo.ExternalID && photo.Type == 'A')
                    {
                        accommodation.Photos.Add(new Photo(photo));
                    }
                }
            }
        }

        public void LoadOwnersToAccommodations(List<Owner> owners)
        {
            foreach (Owner owner in owners)
            {
                foreach (Accommodation accommodation in _accommodations)
                {
                    if (accommodation.OwnerId == owner.ID)
                    {
                        accommodation.Owner = owner;
                    }
                }
            }
        }

        public List<Accommodation> GetByOwnerId(int id = 0)
        {
            return _accommodations.FindAll(a => a.OwnerId == id);
        }

        public List<Accommodation> Search(string country, string city, string name, string type, int maxGuest, int minDays)
        {

            return _accommodations.Where(a => a.Location.Country.Contains(country) && a.Location.City.Contains(city) && a.Name.Contains(name) && a.Type.ToString().Contains(type) && a.MaxGuestNum >= maxGuest && a.MinNumOfDays <= minDays).ToList();
        }

    }
}
