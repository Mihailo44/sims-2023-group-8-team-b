﻿using System;
using System.Collections.Generic;
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


        public List<TourRequest> Search(string country, string city, string language, int maxAttendants,
           DateTime startDate, DateTime endDate)
        {
            List<TourRequest> filteredRequests = new List<TourRequest>();
            return TourRequestRepository.GetAll().FindAll(t =>
                    t.ShortLocation.Country.Contains(country) && t.ShortLocation.City.Contains(city) &&
                    t.Language.Contains(language) && t.MaxAttendance < maxAttendants
                    && t.StartDate >= startDate && t.EndDate <= endDate && t.Status == TourRequestStatus.PENDING);
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
