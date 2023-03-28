using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class LocationViewModel
    {
        private readonly LocationService _location;

        public LocationViewModel()
        {
            _location = new LocationService();
        }

        public List<Location> GetAll()
        {
            return _location.GetAll();
        }

        public void Create(Location newLocation)
        {
            _location.Create(newLocation);
        }

        public void Update(Location updatedLocation, int id)
        {
            _location.Update(updatedLocation, id);
        }

        public void Delete(Location location)
        {
            _location.Delete(location.Id);
        }

        public Location FindByCountryAndCity(string country, string city)
        {
            return _location.FindByCountryAndCity(country, city);
        }

        public int FindLocationID(Location location)
        {
            int? locationID = _location.FindLocationId(location);
            if (locationID != null)
            {
                return (int)locationID;
            }
            return -1;
        }

        public void Subsribe(IObserver observer)
        {
            _location.Subscribe(observer);
        }
    }
}
