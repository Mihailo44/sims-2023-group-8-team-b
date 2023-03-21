using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model.DAO;
using TouristAgency.Model;
using System.Collections.ObjectModel;

namespace TouristAgency.Controller
{
    public class TourController
    {
        private readonly TourDAO _tour;

        public TourController()
        {
            _tour = new TourDAO();
        }

        public int GenerateID()
        {
            return _tour.GenerateId();
        }

        public Tour FindById(int id)
        {
            return _tour.FindById(id);
        }

        public List<Tour> Search(string country, string city, string language, int minDuration, int maxDuration, int maxCapacity)
        {
            return _tour.Search(country, city, language, minDuration, maxDuration, maxCapacity);
        }

        public ObservableCollection<Tourist> GetTouristsFromTour(int id)
        {
            return new ObservableCollection<Tourist>(GetAll().Find(t => t.ID == id).RegisteredTourists);
        }

        public List<Tour> GetAll()
        {
            return _tour.GetAll();
        }

        public ObservableCollection<Tour> GetTodayTours(int guideID)
        {
            return new ObservableCollection<Tour>(_tour.GetTodayTours(guideID));
        }

        public ObservableCollection<Tour> GetValidTours()
        {
            return new ObservableCollection<Tour>(_tour.GetAll().Where(t => t.StartDateTime.Date >= DateTime.Today.Date));
        }

        public void LoadLocationsToTours(List<Location> locations)
        {
            _tour.LoadLocationsToTours(locations);
        }

        public ObservableCollection<string> GetAllCountires()
        {
            return new ObservableCollection<string>(_tour.GetAllCountries());
        }

        public ObservableCollection<string> GetAllCitites()
        {
            return new ObservableCollection<string>(_tour.GetAllCities());
        }

        public ObservableCollection<string> GetAllLanguages()
        {
            return new ObservableCollection<string>(_tour.GetAllLanguages());
        }

        public void Create(Tour newTour)
        {
            _tour.Create(newTour);
        }

        public void Update(Tour updatedTour, int id)
        {
            _tour.Update(updatedTour, id);
        }

        public void Delete(Tour Tour)
        {
            _tour.Delete(Tour.ID);
        }


        public void LoadTouristsToTours(List<TourTourist> tourTourists, List<Tourist> tourists)
        {
            _tour.LoadTouristsToTours(tourTourists, tourists);
        }

        public void LoadCheckpointsToTours(List<TourCheckpoint> tourCheckpoints, List<Checkpoint> checkpoints)
        {
            _tour.LoadCheckpointsToTours(tourCheckpoints, checkpoints);
        }

        public void LoadPhotosToTours(List<Photo> photos)
        {
            _tour.LoadPhotosToTours(photos);
        }

        public void Subscribe(IObserver observer)
        {
            _tour.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tour.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _tour.NotifyObservers();
        }
    }
}
