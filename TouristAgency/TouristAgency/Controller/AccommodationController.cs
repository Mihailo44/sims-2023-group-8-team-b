using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model.DAO;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using System.Collections.ObjectModel;

namespace TouristAgency.Controller
{
    public class AccommodationController
    {
        private readonly AccommodationDAO _accommodation;

        public AccommodationController()
        {
            _accommodation = new AccommodationDAO();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodation.GetAll();
        }

        public ObservableCollection<string> GetNames()
        {
            return new ObservableCollection<string>(_accommodation.GetNames());
        }

        public ObservableCollection<string> GetCities()
        {
            return new ObservableCollection<string>(_accommodation.GetCities());
        }

        public ObservableCollection<string> GetCountries()
        {
            return new ObservableCollection<string>(_accommodation.GetCountries());
        }

        public ObservableCollection<string> GetTypes()
        {
            return new ObservableCollection<string>(_accommodation.GetTypes());
        }

        public void LoadLocationsToAccommodations(List<Location> locations)
        {
            _accommodation.LoadLocationsToAccommodations(locations);
        }

        public void LoadPhotosToAccommodations(List<Photo> photos)
        {
            _accommodation.LoadPhotosToAccommodations(photos);
        }

        public void Create(Accommodation newAccommodation)
        {
            _accommodation.Create(newAccommodation);
        }

        public void Update(Accommodation updatedAccommodation,int id)
        {
            _accommodation.Update(updatedAccommodation, id);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodation.Delete(accommodation.Id);
        }

        public List<Accommodation> GetByOwnerId(int id = 0)
        {
            return _accommodation.GetByOwnerId(id);
        }

        public List<Accommodation> Search(string country, string city, string name, string type, int maxGuest, int minDays)
        {
            return _accommodation.Search(country, city, name, type, maxGuest, minDays);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodation.Subscribe(observer);
        }
    }
}
