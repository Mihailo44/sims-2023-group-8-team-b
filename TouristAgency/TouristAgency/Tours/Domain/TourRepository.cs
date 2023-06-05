using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours
{
    public class TourRepository : ICrud<Tour>, ISubject
    {
        private readonly IStorage<Tour> _storage;
        private readonly List<Tour> _tours;
        private List<IObserver> _observers;

        public TourRepository(IStorage<Tour> storage)
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

        public Tour Create(Tour newTour)
        {
            newTour.ID = GenerateId();
            _tours.Add(new Tour(newTour));
            _storage.Save(_tours);
            NotifyObservers();
            return newTour;
        }

        public Tour GetById(int id)
        {
            return _tours.Find(t => t.ID == id);
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour Update(Tour newTour, int id)
        {
            Tour currentTour = GetById(id);
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

        public void Delete(int id)
        {
            Tour deletedTour = GetById(id);
            _tours.Remove(deletedTour);
            _storage.Save(_tours);
            NotifyObservers();
        }

        public void LoadLocationsToTours(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Tour tour in _tours)
                {
                    if (tour.ShortLocationID == location.ID)
                    {
                        tour.ShortLocation = new Location(location);
                    }
                }
            }
        }

        public void LoadPhotosToTours(List<Photo> photos)
        {
            Photo defaultPhoto = new Photo(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\Image\\TourImage\\none.jpeg", 'T', -1);
            foreach (Tour tour in _tours)
            {
                foreach (Photo photo in photos)
                {
                    if (photo.ExternalID == tour.ID && photo.Type == 'T')
                    {
                        tour.Photos.Add(new Photo(photo));
                    }
                }
                if(tour.Photos.Count == 0)
                {
                    tour.Photos.Add(defaultPhoto);
                }
            }
        }

        public void LoadTouristsToTours(List<TourTourist> tourTourists, List<Tourist> tourists)
        {

            foreach (TourTourist tourtourist in tourTourists)
            {
                Tour selectedTour = GetById(tourtourist.TourID);
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

        public void LoadGuidesToTours(List<Guide> guides)
        {
            foreach(Guide guide in guides)
                foreach(Tour tour in _tours)
                {
                    if (tour.AssignedGuideID == guide.ID)
                        tour.AssignedGuide = guide;
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
