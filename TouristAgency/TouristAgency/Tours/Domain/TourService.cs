using System;
using System.Collections.Generic;
using System.Linq;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours
{
    public class TourService
    {
        private readonly App _app;
        public TourRepository TourRepository { get; set; }

        public TourService()
        {
            _app = (App)System.Windows.Application.Current;
            TourRepository = _app.TourRepository;
        }

        public List<Tour> GetTodayTours(int guideID)
        {
            List<Tour> todayTours = new List<Tour>();
            foreach (Tour tour in TourRepository.GetAll())
            {
                if (tour.StartDateTime.Date == DateTime.Now.Date && tour.AssignedGuideID == guideID && tour.Status != TourStatus.ENDED)
                {
                    todayTours.Add(tour);
                }
            }
            return todayTours;
        }

        public List<Tour> GetCancellabeTours()
        {
            return TourRepository.GetAll().Where(t => (DateTime.Today.Date - t.StartDateTime.Date).Days <= -2).ToList();
        }

        public List<Tour> GetValidTours()
        {
            return TourRepository.GetAll().Where(t => t.StartDateTime.Date >= DateTime.Today.Date && t.Status == TourStatus.NOT_STARTED).ToList();
        }

        public List<Tour> GetFinishedToursByTourist(Tourist tourist)
        {
            return TourRepository.GetAll().FindAll(t => t.RegisteredTourists.Contains(tourist) && t.Status == TourStatus.ENDED);
        }

        public List<Tour> GetFinishedToursByGuide(Guide guide)
        {
            return TourRepository.GetAll().FindAll(t => t.AssignedGuideID == guide.ID && t.Status == TourStatus.ENDED);
        }

        public List<Tour> GetActiveTours(Tourist tourist)
        {
            return TourRepository.GetAll().FindAll(t => t.RegisteredTourists.Contains(tourist) && t.Status == TourStatus.IN_PROGRESS);
        }

        public List<Tour> Search(string country, string city, string language, int minDuration, int maxDuration,
            int maxCapacity)
        {
            List<Tour> filteredTours = new List<Tour>();
            if (minDuration > 0 && maxDuration == 0)
            {
                return TourRepository.GetAll().Where(t =>
                        t.ShortLocation.Country.Contains(country) && t.ShortLocation.City.Contains(city) &&
                        t.Language.Contains(language) && t.Duration >= minDuration && t.MaxAttendants >= maxCapacity)
                    .ToList();
            }
            else
            {
                return TourRepository.GetAll().Where(t =>
                    t.ShortLocation.Country.Contains(country) && t.ShortLocation.City.Contains(city) &&
                    t.Language.Contains(language) && t.Duration >= minDuration && t.Duration <= maxDuration &&
                    t.MaxAttendants >= maxCapacity).ToList();
            }
        }

        public void RegisterTourist(int tourID, Tourist tourist, int numberOfReservations)
        {
            Tour tour = _app.TourRepository.GetById(tourID);
            tour.CurrentAttendants += numberOfReservations;
            if (!tour.RegisteredTourists.Contains(tourist))
            {
                tour.RegisteredTourists.Add(tourist);
            }
            _app.TourRepository.Update(tour, tourID);
        }

        public void ChangeTourStatus(int id, TourStatus status)
        {
            Tour selectedTour = _app.TourRepository.GetById(id);
            selectedTour.Status = status;
            _app.TourRepository.Update(selectedTour, selectedTour.ID);
        }



        public List<string> GetYearsForStatistics()
        {
            List<string> years = new List<string>
            {
                "All-time"
            };
            foreach (Tour tour in TourRepository.GetAll())
            {
                string tourStartYear = tour.StartDateTime.Year.ToString();
                if (!years.Contains(tourStartYear))
                {
                    years.Add(tourStartYear);
                }
            }
            years.Sort();
            years.Reverse();
            return years;
        }

        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            countries.Add("");

            foreach (Tour tour in TourRepository.GetAll())
            {
                if (!countries.Contains(tour.ShortLocation.Country) && tour.ShortLocation.Country != "")
                {
                    countries.Add(tour.ShortLocation.Country);
                }
            }

            return countries;
        }

        public List<string> GetAllCities()
        {
            List<string> cities = new List<string>();
            cities.Add("");

            foreach (Tour tour in TourRepository.GetAll())
            {
                if (!cities.Contains(tour.ShortLocation.City) && tour.ShortLocation.City != "")
                {
                    cities.Add(tour.ShortLocation.City);
                }
            }

            return cities;
        }

        public List<string> GetAllLanguages()
        {
            List<string> languages = new List<string>();
            languages.Add("");

            foreach (Tour tour in TourRepository.GetAll())
            {
                if (!languages.Contains(tour.Language) && tour.Language != "")
                {
                    languages.Add(tour.Language);
                }
            }

            return languages;
        }

        public Tour GetBestTourByYear(string year)
        {
            Tour bestTour;
            if (year == "All-time")
            {
                int maxTurists = TourRepository.GetAll().Max(t => t.CurrentAttendants);
                bestTour = TourRepository.GetAll().First(t => t.CurrentAttendants == maxTurists);
            }
            else
            {
                List<Tour> toursInYear = TourRepository.GetAll().FindAll(t => t.StartDateTime.Year.ToString() == year);
                int maxTurists = toursInYear.Max(t => t.CurrentAttendants);
                bestTour = toursInYear.First(t => t.CurrentAttendants == maxTurists);
            }
            return bestTour;
        }

        public int[] GetTourAgeStatistics(Tour tour)
        {
            int[] result = new int[3];
            // 0 young, 1 adult, 2 old
            foreach (Tourist tourist in tour.RegisteredTourists)
            {
                int ageCategory = tourist.GetAgeCategory();
                if (ageCategory == 0)
                    result[0] += 1;
                else if (ageCategory == 1)
                    result[1] += 1;
                else
                    result[2] += 1;
            }
            return result;
        }

        public List<Tourist> GetTouristsFromTour(int tourId)
        {
            return _app.TourRepository.GetAll().Find(t => t.ID == tourId).RegisteredTourists;
        }
    }
}
