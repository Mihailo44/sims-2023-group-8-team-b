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

        public List<Tour> GetAll()
        {
            return _tour.GetAll();
        }

        public ObservableCollection<Tour> GetTodayTours()
        {
            return new ObservableCollection<Tour>(_tour.GetTodayTours());
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
