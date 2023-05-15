using System;
using System.Collections.Generic;
using System.Linq;
using TouristAgency.Statistics;
using System.Diagnostics.Metrics;
using System.Linq;
using TouristAgency.Base;
using TouristAgency.Tours;
using TouristAgency.Users;
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

        public TourRequest Create(TourRequest newTourRequest)
        {
            return TourRequestRepository.Create(newTourRequest);
        }

        public TourRequest Update(TourRequest newTourRequest, int ID)
        {
            return TourRequestRepository.Update(newTourRequest, ID);
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

        public List<Location> GetAllLocations()
        {
            List<Location> locations = new List<Location>();

            foreach (TourRequest tourRequest in TourRequestRepository.GetAll())
            {
                if (!locations.Contains(tourRequest.ShortLocation))
                {
                    locations.Add(tourRequest.ShortLocation);
                }
            }

            return locations;
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
                    t.Language.Contains(language) && t.MaxAttendants < maxAttendants
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

        public List<string> GetYearsForStatistics()
        {
            List<string> years = new List<string>
            {
                "All-time"
            };
            foreach (TourRequest tourRequest in TourRequestRepository.GetAll())
            {
                string tourRequestStartYear = tourRequest.StartDate.Year.ToString();
                if (!years.Contains(tourRequestStartYear))
                {
                    years.Add(tourRequestStartYear);
                }
            }
            years.Sort();
            years.Reverse();
            return years;
        }

        public List<string> GetTourRequestStatisticsByYear(int touristID, string year)
        {
            double accepted = 0.0;
            double avgNumOfPeople = 0.0;
            List<string> statistics = new List<string>();
            List<TourRequest> requestByTouristID = TourRequestRepository.GetAll().FindAll(t => t.TouristID == touristID);
            int countPeople;
            int countRequest;

            if(year == "All-time")
            {
                countPeople = requestByTouristID.FindAll(t => t.Status == TourRequestStatus.ACCEPTED).Count();
                countRequest = requestByTouristID.Count();

                foreach(TourRequest tourRequest in requestByTouristID)
                {
                    if(tourRequest.Status == TourRequestStatus.ACCEPTED)
                    {
                        accepted++;
                        avgNumOfPeople += tourRequest.MaxAttendants;
                    }
                }
            }
            else
            {
                countPeople = requestByTouristID.FindAll(t => t.StartDate.Year.ToString() == year && t.Status == TourRequestStatus.ACCEPTED).Count();
                countRequest= requestByTouristID.FindAll(t => t.StartDate.Year.ToString() == year).Count();
                
                foreach (TourRequest tourRequest in requestByTouristID.FindAll(t => t.StartDate.Year.ToString() == year))
                {
                    if (tourRequest.Status == TourRequestStatus.ACCEPTED)
                    {
                        accepted++;
                        avgNumOfPeople += tourRequest.MaxAttendants;
                    }
                }
            }

            accepted = Math.Round((accepted / countRequest) * 100);
            double deined = 100 - accepted;
            avgNumOfPeople = Math.Round(avgNumOfPeople / countPeople);
            if(countPeople == 0)
            {
                avgNumOfPeople = 0;
            }
            statistics.Add(accepted.ToString());
            statistics.Add(deined.ToString());
            statistics.Add(avgNumOfPeople.ToString());

            return statistics;
        }

        public List<TourRequestStatisticsData> GetAcceptedGraphData(int touristID)
        {
            List<string> allLanguages = GetAllLanguages();
            List<Location> allLocations = GetAllLocations();
            List<TourRequest> allTourRequests = TourRequestRepository.GetAll().FindAll(t => t.TouristID == touristID);
            List<TourRequestStatisticsData> graphData = new List<TourRequestStatisticsData>();
            
            foreach(TourRequest tourRequest in allTourRequests)
            {
                bool found = false;

                foreach(TourRequestStatisticsData data in graphData)
                {
                    if(data.Title == tourRequest.Language)
                    {
                        if(tourRequest.Status == TourRequestStatus.ACCEPTED)
                        {
                            data.Value++;
                        }
                        found = true;
                    }
                }

                if(found == false && tourRequest.Status == TourRequestStatus.ACCEPTED)
                {
                    graphData.Add(new TourRequestStatisticsData(tourRequest.Language, 1));
                }

                found = false;

                string location = tourRequest.ShortLocation.City + ", " + tourRequest.ShortLocation.Country;
                foreach (TourRequestStatisticsData data in graphData)
                {
                    if (data.Title == location)
                    {
                        if (tourRequest.Status == TourRequestStatus.ACCEPTED)
                        {
                            data.Value++;
                        }
                        found = true;
                    }
                }

                if (found == false && tourRequest.Status == TourRequestStatus.ACCEPTED)
                {
                    graphData.Add(new TourRequestStatisticsData(location, 1));
                }

            }

            return graphData;
        }

        public List<TourRequestStatisticsData> GetDeniedGraphData(int touristID)
        {
            List<string> allLanguages = GetAllLanguages();
            List<Location> allLocations = GetAllLocations();
            List<TourRequest> allTourRequests = TourRequestRepository.GetAll().FindAll(t => t.TouristID == touristID);
            List<TourRequestStatisticsData> graphData = new List<TourRequestStatisticsData>();

            foreach (TourRequest tourRequest in allTourRequests)
            {
                bool found = false;

                foreach (TourRequestStatisticsData data in graphData)
                {
                    if (data.Title == tourRequest.Language)
                    {
                        if (tourRequest.Status == TourRequestStatus.INVALID)
                        {
                            data.Value++;
                        }
                        found = true;
                    }
                }

                if (found == false && tourRequest.Status == TourRequestStatus.INVALID)
                {
                    graphData.Add(new TourRequestStatisticsData(tourRequest.Language, 1));
                }

                found = false;

                string location = tourRequest.ShortLocation.City + ", " + tourRequest.ShortLocation.Country;
                foreach (TourRequestStatisticsData data in graphData)
                {
                    if (data.Title == location)
                    {
                        if (tourRequest.Status == TourRequestStatus.INVALID)
                        {
                            data.Value++;
                        }
                        found = true;
                    }
                }

                if (found == false && tourRequest.Status == TourRequestStatus.INVALID)
                {
                    graphData.Add(new TourRequestStatisticsData(location, 1));
                }

            }

            return graphData;
        }
    }
}
