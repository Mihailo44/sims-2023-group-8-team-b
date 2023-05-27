﻿using System;
using System.Linq;
using System.Collections.Generic;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Accommodations.RenovationFeatures.Domain;

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

        public List<Location> GetHotLocations(AccommodationService accommodationService,ReservationService reservationService,PostponementRequestService postponementRequestService, RenovationRecommendationService renovationRecommendationService,List<Accommodation> ownersAccommodations)
        {
            Dictionary<Location, int> reservationsOnLocation = new();

            foreach(Location location in LocationRepository.GetAll().Distinct())
            {
                List<Accommodation> accommodationsOnLocation = ownersAccommodations.Where(a => a.Location.Equals(location)).ToList();
                foreach (Accommodation accommodation in accommodationsOnLocation)
                {
                    if (accommodation.Location.Equals(location))
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
            }
  
            Dictionary<Location, int> sortedDictionary = reservationsOnLocation.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return sortedDictionary.Keys.ToList();
        }
    }
}
