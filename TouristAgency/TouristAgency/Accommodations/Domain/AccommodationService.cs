using System.Collections.Generic;
using System.Linq;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;
using System;
using TouristAgency.Accommodations.Domain.DTO;
using TouristAgency.Util;

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

        private double[] CalculateMonthlyOccupancy(ReservationService reservationService, Accommodation accommodation, int year)
        {
            var monthGroups = reservationService.GetByAccommodationId(accommodation.Id).FindAll(r => r.Start.Year == year && r.IsCanceled == false).GroupBy(r => r.Start.Month);

            double[] monthlyOccupancy = new double[12];

            foreach (var month in monthGroups)
            {
                foreach (var reservation in month)
                {
                    if (reservation.End.Month == reservation.Start.Month)
                    {
                        //moze i month.Key, kljucevi grupa idu bas po pravom broju meseca, a ne od 0,1,2..
                        monthlyOccupancy[reservation.Start.Month - 1] += (reservation.End - reservation.Start).TotalDays;
                    }
                    else if (reservation.End.Month > reservation.Start.Month)
                    {
                        if (reservation.Start.Month == 12)
                        {
                            monthlyOccupancy[reservation.Start.Month - 1] += (DateTime.DaysInMonth(year, month.Key) - reservation.Start.Day);
                            monthlyOccupancy[0] += reservation.End.Day;
                        }
                        else
                        {
                            monthlyOccupancy[reservation.Start.Month - 1] += (DateTime.DaysInMonth(year, month.Key) - reservation.Start.Day);
                            monthlyOccupancy[reservation.Start.Month] += reservation.End.Day;
                        }
                    }
                }
            }

            for (int i = 0; i < monthlyOccupancy.Length; i++)
            {
                if (i == 1)
                {
                    if (DateTime.IsLeapYear(year))
                        monthlyOccupancy[i] = monthlyOccupancy[i] / 29;
                    else
                        monthlyOccupancy[i] = monthlyOccupancy[i] / 28;

                    continue;
                }

                if (i % 2 == 0)
                    monthlyOccupancy[i] = monthlyOccupancy[i] / 30;
                if (i % 2 == 1)
                    monthlyOccupancy[i] = monthlyOccupancy[i] / 31;
            }

            return monthlyOccupancy;
        }

        private int GetBusiestMonthIndex(double[] monthlyOccupancy)
        {
            int busiestMonth = 0;
            double max = 0;

            for (int i = 0; i < monthlyOccupancy.Length; i++)
            {
                if (max < monthlyOccupancy[i])
                {
                    max = monthlyOccupancy[i];
                    busiestMonth = i;
                }
            }

            return busiestMonth;
        }

        public List<int> GetAccommodationStatsByYear(ReservationService reservationService, PostponementRequestService postponementRequestService, RenovationRecommendationService renovationRecommendationService, Accommodation accommodation, int year)
        {
            List<int> results = new List<int>();
            int reservations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.IsCanceled == false).Count();
            int cancelations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.IsCanceled == true).Count();
            int postponations = postponementRequestService.PostponementRequestRepository.GetAll().FindAll(p => p.Reservation.Start.Year == year && p.Reservation.AccommodationId == accommodation.Id).Count();
            int reccommendations = renovationRecommendationService.RenovationRecommendationRepository.GetAll().FindAll(r => r.Reservation.AccommodationId == accommodation.Id && r.Reservation.Start.Year == year && r.Reservation.IsCanceled == false).Count();
            int busiestMonth = 0;

            double[] monthlyOccupancy = CalculateMonthlyOccupancy(reservationService, accommodation, year);
            busiestMonth = GetBusiestMonthIndex(monthlyOccupancy);

            results.Add(reservations);
            results.Add(cancelations);
            results.Add(postponations);
            results.Add(reccommendations);
            results.Add(busiestMonth + 1); //meseci u nizu pocinju od 0,a u DateTime od 1

            return results;
        }

        public AccommodationStatisticsDTO GetAccommodationStatsByMonth(ReservationService reservationService, PostponementRequestService postponementRequestService, RenovationRecommendationService renovationRecommendationService, Accommodation accommodation, int year, int monthNumber)
        {
            AccommodationStatisticsDTO result = new();

            int reservations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.Start.Month == monthNumber && r.IsCanceled == false).Count();
            int cancelations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.Start.Month == monthNumber && r.IsCanceled == true).Count();
            int postponements = postponementRequestService.PostponementRequestRepository.GetAll().FindAll(p => p.Reservation.Start.Year == year && p.Reservation.Start.Month == monthNumber && p.Reservation.AccommodationId == accommodation.Id).Count();
            int reccommendations = renovationRecommendationService.RenovationRecommendationRepository.GetAll().FindAll(r => r.Reservation.AccommodationId == accommodation.Id && r.Reservation.IsCanceled == false && r.Reservation.Start.Year == year && r.Reservation.Start.Month == monthNumber).Count();

            result.Reservations = reservations;
            result.Cancelations = cancelations;
            result.Postponations = postponements;
            result.Reccommendations = reccommendations;

            return result;
        }

        public void SetHotLocationsStatus(LocationService locationService, AccommodationService accommodationService, ReservationService reservationService, PostponementRequestService postponementRequestService, RenovationRecommendationService renovationRecommendationService)
        {
            List<Location> locations = locationService.GetHotLocations(accommodationService, reservationService, postponementRequestService, renovationRecommendationService, GetByOwnerId(app.LoggedUser.ID));

            List<Location> hotLocations = locations.Take(2).ToList();

            foreach (Accommodation accommodation in GetByOwnerId(app.LoggedUser.ID))
            {
                if(hotLocations.Contains(accommodation.Location))
                {
                    accommodation.HotLocation = true;
                    AccommodationRepository.Update(accommodation, accommodation.Id);
                }
                else
                {
                    accommodation.HotLocation = false;
                    AccommodationRepository.Update(accommodation, accommodation.Id);
                }
            }
        }
    }
}
