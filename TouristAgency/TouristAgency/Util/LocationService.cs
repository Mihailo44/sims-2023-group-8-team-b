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

        public int FindLocationId(Location location)
        {
            return LocationRepository.GetAll().Find(l => l.Equals(location)).Id;
        }

        public Location FindByCountryAndCity(string country, string city)
        {
            Location location = LocationRepository.GetAll().Find(l => l.Country.ToLower() == country.ToLower() && l.City.ToLower() == city.ToLower());
            if (location == null)
            {
                location = new Location(country, city);
                LocationRepository.Create(location);
            }

            return location;
        }
    }
}
