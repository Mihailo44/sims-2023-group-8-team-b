using System;
using System.Collections.Generic;
using System.Linq;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.RenovationFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;

namespace TouristAgency.Util
{
    public class LocationService
    {
        private readonly App _app;
        public LocationRepository LocationRepository { get; }

        public LocationService()
        {
            _app = (App)System.Windows.Application.Current;
            LocationRepository = _app.LocationRepository;
        }

        public Location Create(Location newLocation)
        {
            return LocationRepository.Create(newLocation);
        }

        public int FindLocationId(Location location)
        {
            return LocationRepository.GetAll().Find(l => l.Equals(location)).ID;
        }

        public Location FindByCountryAndCity(string country, string city)
        {
            Location location = LocationRepository.GetAll().FirstOrDefault(l => l.Country.ToLower() == country.ToLower() && l.City.ToLower() == city.ToLower());
            if (location == null)
            {
                location = new Location(country, city);
                LocationRepository.Create(location);
            }

            return location;
        }

        public List<Location> GetLocationsStats(AccommodationService accommodationService, ReservationService reservationService, PostponementRequestService postponementRequestService, RenovationRecommendationService renovationRecommendationService, List<Accommodation> ownersAccommodations)
        {
            Dictionary<Location, int> reservationsOnLocation = new();

            foreach (Location location in LocationRepository.GetAll())
            {
                List<Accommodation> accommodationsOnLocation = ownersAccommodations.Where(a => a.Location.ID == location.ID).ToList();

                foreach (Accommodation accommodation in accommodationsOnLocation)
                {
                    List<int> stats = accommodationService.GetAccommodationStatsByYear(reservationService, postponementRequestService, renovationRecommendationService, accommodation, DateTime.Today.Year);

                    if (reservationsOnLocation.ContainsKey(location))
                    {
                        reservationsOnLocation[location] += stats[0];
                    }
                    else
                    {
                        reservationsOnLocation[location] = stats[0];
                    }
                }
            }

            Dictionary<Location, int> sortedDictionary = reservationsOnLocation.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            
            return sortedDictionary.Keys.ToList();
        }

        public List<string> GetCities()
        {
            List<string> cities = new List<string>();

            foreach(Location location in LocationRepository.GetAll())
            {
                if (!cities.Contains(location.City))
                {
                    cities.Add(location.City);
                }

            }
            return cities;
        }

        public Location GetByCity(string city)
        {
            return LocationRepository.GetAll().FirstOrDefault(l => l.City == city);
        }

        public bool HasAccommodationOnLocation(Owner owner, Location location)
        {
            Accommodation accommodation = owner.Accommodations.Find(a => a.Location.ID == location.ID);

            return accommodation != null;
        }
    }
}
