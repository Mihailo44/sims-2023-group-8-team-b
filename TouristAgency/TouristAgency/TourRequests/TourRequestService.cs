using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using TouristAgency.Base;
using TouristAgency.Tours;
using TouristAgency.Util;

namespace TouristAgency.TourRequests
{
    public class TourRequestService
    {
        private readonly App _app;
        public TourRequestRepository TourRequestRepository { get; set; }

        public TourRequestService()
        {
            _app = (App)System.Windows.Application.Current;
            TourRequestRepository = _app.TourRequestRepository;
        }

        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            countries.Add("");

            foreach (TourRequest tourRequest in TourRequestRepository.GetAll())
            {
                if (!countries.Contains(tourRequest.ShortLocation.Country) && tourRequest.ShortLocation.Country != "")
                {
                    countries.Add(tourRequest.ShortLocation.Country);
                }
            }

            return countries;
        }

        public List<string> GetAllCities()
        {
            List<string> cities = new List<string>();
            cities.Add("");

            foreach (TourRequest tourRequest in TourRequestRepository.GetAll())
            {
                if (!cities.Contains(tourRequest.ShortLocation.City) && tourRequest.ShortLocation.City != "")
                {
                    cities.Add(tourRequest.ShortLocation.City);
                }
            }

            return cities;
        }

        public List<string> GetAllLanguages()
        {
            List<string> languages = new List<string>();
            languages.Add("");

            foreach (TourRequest tourRequest in TourRequestRepository.GetAll())
            {
                if (!languages.Contains(tourRequest.Language) && tourRequest.Language != "")
                {
                    languages.Add(tourRequest.Language);
                }
            }

            return languages;
        }

        public List<TourRequest> GetByTouristID(int touristID)
        {
            return TourRequestRepository.GetAll().FindAll(t => t.TouristID == touristID);
        }

        public List<TourRequest> GetPendingTourRequests()
        {
            return TourRequestRepository.GetAll().FindAll(t => t.Status == TourRequestStatus.PENDING);
        }

        //Country, City, Language, MaxAttendants, StartDate, EndDate
        public List<TourRequest> Search(string country, string city, string language, int maxAttendants,
           DateTime startDate, DateTime endDate)
        {
            List<TourRequest> filteredRequests = new List<TourRequest>();
            return TourRequestRepository.GetAll().FindAll(t =>
                    t.ShortLocation.Country.Contains(country) && t.ShortLocation.City.Contains(city) &&
                    t.Language.Contains(language) && t.MaxAttendance < maxAttendants
                    && t.StartDate >= startDate && t.EndDate <= endDate && t.Status == TourRequestStatus.PENDING);
        }

        public List<TourRequest> Search(string country, string city, string language,
   DateTime startDate, DateTime endDate)
        {
            List<TourRequest> filteredRequests = new List<TourRequest>();
            return TourRequestRepository.GetAll().FindAll(t =>
                    t.ShortLocation.Country.Contains(country) && t.ShortLocation.City.Contains(city) &&
                    t.Language.Contains(language) && t.StartDate >= startDate 
                    && t.EndDate <= endDate);
        }

        public int GetRequestNum(string country, string city, string language, DateTime startDate,
            DateTime endDate)
        {
            List<TourRequest> filteredRequests = Search(country, city, language, startDate, endDate);
            var group = filteredRequests.GroupBy(t => t.ShortLocationID);
            int max = 0;
            foreach(var element in group)
            {
                if(element.Count() > max)
                {
                    max = element.Count();
                }
            }
            return max;
        }

        public (TourRequest, int) GetMostRequested()
        {
            var group = TourRequestRepository.GetAll().GroupBy(t => t.ShortLocationID);
            int max = 0;
            TourRequest tourRequest = new TourRequest();
            foreach (var element in group)
            {
                if (element.Count() > max)
                {
                    max = element.Count();
                    tourRequest = element.ToList()[0];
                }
            }
            return (tourRequest, max);
        }

        public void InvalidateOldTourRequests()
        {
            DateTime today = DateTime.Now;
            TimeSpan ts;

            foreach(TourRequest tourRequest in TourRequestRepository.GetAll())
            {
                ts = tourRequest.StartDate - today;
                if(ts.TotalHours < 48)
                {
                    tourRequest.Status = TourRequestStatus.INVALID;
                    TourRequestRepository.Update(tourRequest, tourRequest.ID);
                }
            }
        }
    }
}
