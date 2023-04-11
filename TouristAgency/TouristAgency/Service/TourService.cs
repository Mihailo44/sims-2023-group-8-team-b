using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.Enums;
using TouristAgency.Storage;

namespace TouristAgency.Service
{
    public class TourService : ICrud<Tour>, ISubject
    {
        private readonly IStorage<Tour> _storage;
        private readonly List<Tour> _tours;
        private List<IObserver> _observers;

        public TourService(IStorage<Tour> storage)
        {
            _storage = storage;
            _tours = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if (_tours.Count == 0)
            {
                return 0;
            }
            return _tours.Max(t => t.ID) + 1;
        }

        public Tour FindById(int id)
        {
            return _tours.Find(t => t.ID == id);
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public List<Tour> GetTodayTours(int guideID)
        {
            List<Tour> todayTours = new List<Tour>();
            foreach (Tour tour in _tours)
            {
                if (tour.StartDateTime.Date == DateTime.Now.Date && tour.AssignedGuideID == guideID && tour.Status != STATUS.ENDED)
                {
                    todayTours.Add(tour);
                }
            }
            return todayTours;
        }

        public List<Tour> GetCancellabeTours()
        {
            return _tours.Where(t => (DateTime.Today.Date - t.StartDateTime.Date).Days <= -2).ToList();
        }

        public List<Tour> GetValidTours()
        {
            return GetAll().Where(t => t.StartDateTime.Date >= DateTime.Today.Date && t.Status == STATUS.NOT_STARTED).ToList();
        }

        public List<Tour> GetFinishedToursByTourist(Tourist tourist)
        {
            return GetAll().FindAll(t => t.RegisteredTourists.Contains(tourist) && t.Status == STATUS.ENDED);
        }

        public List<Tour> GetFinishedToursByGuide(Guide guide)
        {
            return _tours.FindAll(t => t.AssignedGuideID == guide.ID && t.Status == STATUS.ENDED);
        }

        public List<Tour> GetActiveTours(Tourist tourist)
        {
            return GetAll().FindAll(t => t.RegisteredTourists.Contains(tourist) && t.Status == STATUS.IN_PROGRESS);
        }

        public List<Tour> Search(string country, string city, string language, int minDuration, int maxDuration, int maxCapacity)
        {
            List<Tour> filteredTours = new List<Tour>();
            if (minDuration > 0 && maxDuration == 0)
            {
                return _tours.Where(t => t.ShortLocation.Country.Contains(country) && t.ShortLocation.City.Contains(city) && t.Language.Contains(language) && t.Duration >= minDuration && t.MaxAttendants >= maxCapacity).ToList();
            }
            else
            {
                return _tours.Where(t => t.ShortLocation.Country.Contains(country) && t.ShortLocation.City.Contains(city) && t.Language.Contains(language) && t.Duration >= minDuration && t.Duration <= maxDuration && t.MaxAttendants >= maxCapacity).ToList();
            }
        }

        public Tour Create(Tour newTour)
        {
            newTour.ID = GenerateId();
            _tours.Add(newTour);
            _storage.Save(_tours);
            NotifyObservers();
            return newTour;
        }

        public Tour Update(Tour newTour, int id)
        {
            Tour currentTour = FindById(id);
            if (currentTour == null)
            {
                return null;
            }
            currentTour.Name = newTour.Name;
            currentTour.Description = newTour.Description;
            currentTour.ShortLocation = newTour.ShortLocation;
            currentTour.Language = newTour.Language;
            currentTour.MaxAttendants = newTour.MaxAttendants;
            currentTour.CurrentAttendants = newTour.CurrentAttendants;
            currentTour.Duration = newTour.Duration;
            currentTour.StartDateTime = newTour.StartDateTime;
            currentTour.RemainingCapacity = currentTour.MaxAttendants - currentTour.CurrentAttendants;
            _storage.Save(_tours);
            NotifyObservers();
            return currentTour;
        }

        public void RegisterTourist(int tourID, Tourist tourist, int numberOfReservations)
        {
            Tour tour = FindById(tourID);
            tour.CurrentAttendants += numberOfReservations;
            if (!tour.RegisteredTourists.Contains(tourist))
            {
                tour.RegisteredTourists.Add(tourist);
            }
            Update(tour, tourID);
        }

        public void ChangeTourStatus(int id, STATUS status)
        {
            Tour selectedTour = FindById(id);
            selectedTour.Status = status;
            Update(selectedTour, selectedTour.ID);
        }

        public void Delete(int id)
        {
            Tour deletedTour = FindById(id);
            _tours.Remove(deletedTour);
            _storage.Save(_tours);
            NotifyObservers();
        }

        public List<String> GetYearsForStatistics()
        {
            List<String> years = new List<string>
            {
                "All-time"
            };
            foreach (Tour tour in _tours)
            {
                String tourStartYear = tour.StartDateTime.Year.ToString();
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

            foreach (Tour tour in _tours)
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

            foreach (Tour tour in _tours)
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

            foreach (Tour tour in _tours)
            {
                if (!languages.Contains(tour.Language) && tour.Language != "")
                {
                    languages.Add(tour.Language);
                }
            }

            return languages;
        }

        public Tour GetBestTourByYear(String year)
        {
            Tour bestTour;
            if (year == "All-time")
            {
                int maxTurists = _tours.Max(t => t.CurrentAttendants);
                bestTour = _tours.First(t => t.CurrentAttendants == maxTurists);
            }
            else
            {
                List<Tour> toursInYear = _tours.FindAll(t => t.StartDateTime.Year.ToString() == year);
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
            return _tours.Find(t => t.ID == tourId).RegisteredTourists;
        }

        public void LoadLocationsToTours(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Tour tour in _tours)
                {
                    if (tour.ShortLocationID == location.Id)
                    {
                        tour.ShortLocation = new Location(location);
                    }
                }
            }
        }

        public void LoadPhotosToTours(List<Photo> photos)
        {
            foreach (Tour tour in _tours)
            {
                foreach (Photo photo in photos)
                {
                    if (photo.ExternalID == tour.ID && photo.Type == 'T')
                    {
                        tour.Photos.Add(new Photo(photo));
                    }
                }
            }
        }

        public void LoadTouristsToTours(List<TourTourist> tourTourists, List<Tourist> tourists)
        {

            foreach (TourTourist tourtourist in tourTourists)
            {
                Tour selectedTour = FindById(tourtourist.TourID);
                foreach (Tourist tourist in tourists)
                {
                    if (tourist.ID == tourtourist.TouristID)
                    {
                        selectedTour.RegisteredTourists.Add(tourist);
                    }
                }
            }
        }

        public void LoadCheckpointsToTours(List<TourCheckpoint> tourCheckpoints, List<Checkpoint> checkpoints)
        {
            foreach (TourCheckpoint tourCheckpoint in tourCheckpoints)
            {
                foreach (Tour tour in _tours)
                {
                    if (tour.ID == tourCheckpoint.TourID)
                    {
                        tour.Checkpoints.Add(checkpoints.Find(c => c.ID == tourCheckpoint.CheckpointID));
                    }
                }
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
