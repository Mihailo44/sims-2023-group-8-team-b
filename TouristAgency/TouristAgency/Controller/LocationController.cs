using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model.DAO;
using TouristAgency.Model;
using TouristAgency.Interfaces;

namespace TouristAgency.Controller
{
    public class LocationController
    {
        private readonly LocationDAO _location;

        public LocationController()
        {
            _location = new LocationDAO();
        }

        public List<Location> GetAll()
        {
            return _location.GetAll();
        }

        public void Create(Location newLocation)
        {
            _location.Create(newLocation);
        }

        public void Update(Location updatedLocation,int id)
        {
            _location.Update(updatedLocation, id);
        }

        public void Delete(Location location)
        {
            _location.Delete(location.Id);
        }

        public Location FindByCountryAndCity(string country,string city)
        {
            return _location.FindByCountryAndCity(country, city);
        }

        public void Subsribe(IObserver observer)
        {
            _location.Subscribe(observer);
        }
    }
}
