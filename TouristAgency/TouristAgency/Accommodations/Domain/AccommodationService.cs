using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Accommodations.Domain
{
    public class AccommodationService
    {
        private readonly App app = (App)System.Windows.Application.Current;

        public AccommodationRepository AccommodationRepository { get; }

        public AccommodationService()
        {
            AccommodationRepository = app.AccommodationRepository;
        }

        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            names.Add(string.Empty);

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

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

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

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

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

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

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!types.Contains(accommodation.Type.ToString()) && accommodation.Type.ToString() != "")
                {
                    types.Add(accommodation.Type.ToString());
                }
            }

            return types;
        }

        public List<Accommodation> GetByOwnerId(int id)
        {
            return AccommodationRepository.GetAll().FindAll(a => a.OwnerId == id);
        }

        public List<Accommodation> Search(string country, string city, string name, string type, int maxGuest, int minDays)
        {
            return AccommodationRepository.GetAll().Where(a => a.Location.Country.Contains(country) && a.Location.City.Contains(city) && a.Name.Contains(name) && a.Type.ToString().Contains(type) && a.MaxGuestNum >= maxGuest && a.MinNumOfDays <= minDays).OrderByDescending(a => a.Owner.SuperOwner).ToList();
        }
    }
}
