using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;
using TouristAgency.Storage.FileStorage;

namespace TouristAgency.Service
{
    public class LocationService
    {
        private readonly App _app;
        private LocationRepository _locationRepository;

        public LocationService()
        {
            _app = (App)App.Current;
            _locationRepository = _app.LocationRepository;
        }

        public int FindLocationId(Location location)
        {
            return _locationRepository.GetAll().Find(l => l.Equals(location)).Id;
        }

        public Location FindByCountryAndCity(string country, string city)
        {
            Location location = _locationRepository.GetAll().Find(l => l.Country.ToLower() == country.ToLower() && l.City.ToLower() == city.ToLower());
            if (location == null)
            {
                location = new Location(country, city);
                _locationRepository.Create(location);
            }

            return location;
        }
    }
}
