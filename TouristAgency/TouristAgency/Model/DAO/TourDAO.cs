﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class TourDAO : ICrud<Tour>, ISubject
    {
        private readonly TourStorage _storage;
        private readonly List<Tour> _tours;
        private List<IObserver> _observers;

        public TourDAO()
        {
            _storage = new TourStorage();
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
            currentTour.Duration = newTour.Duration;
            currentTour.StartDateTime = newTour.StartDateTime; //! Mozda mora new!
            //TODO liste, kada napravis update formu
            //currentTour.GuideID = ..
            //slike...
            NotifyObservers();
            return currentTour;
        }

        public void Delete(int id)
        {
            Tour deletedTour = FindById(id);
            _tours.Remove(deletedTour);
            _storage.Save(_tours);
            NotifyObservers();
        }

        public List<Tour> GetAll()
        {
            return _tours;
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

        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();

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
            List<string> citites = new List<string>();

            foreach(Tour tour in _tours) 
            {
                if(!citites.Contains(tour.ShortLocation.City) && tour.ShortLocation.City != "")
                {
                    citites.Add(tour.ShortLocation.City);
                }
            }

            return citites;
        }

        public List<string> GetAllLanguages()
        {
            List<string> languages = new List<string>();

            foreach (Tour tour in _tours)
            {
                if (!languages.Contains(tour.Language) && tour.Language != "")
                {
                    languages.Add(tour.Language);
                }
            }

            return languages;
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
