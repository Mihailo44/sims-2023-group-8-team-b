using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using System.Collections.ObjectModel;
using TouristAgency.Service;
using TouristAgency.Base;

namespace TouristAgency.ViewModel
{
    public class AccommodationViewModel : ViewModelBase
    {
        private readonly AccommodationService _accommodation;
        private readonly PhotoService _photoService;
        private readonly LocationDAO _locationDAO;
        private Owner _owner;
        private int _ownerId;
        private string _name;
        private TYPE _type;
        private int _maxGuestNum;
        private int _minNumOfDays;
        private int _allowedNumOfDaysForCancelation;
        private string _photoLinks;

        public Accommodation NewAccommodation { get; set; }
        public Location NewLocation { get; set; }
        public Owner LoggedUser { get; set; }
        public DelegateCommand CreateCmd { get; set; }

        public AccommodationViewModel(Owner owner)
        {
            _accommodation = new AccommodationService();
            _photoService = new PhotoService();
            _locationDAO = new LocationDAO();
            LoggedUser = owner;
            NewAccommodation = new();
            NewLocation = new();
            CreateCmd = new DelegateCommand(param => CreateAccommodationExecute(),param => CanCreateAccommodationExecute());
        }

        public Owner Owner
        {
            get => _owner;
            set
            {
                if (value != _owner)
                {
                    _owner = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OwnerId
        {
            get => _ownerId;
            set
            {
                if (value != _ownerId)
                {
                    _ownerId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public TYPE Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MaxGuestNum
        {
            get => _maxGuestNum;
            set
            {
                if (value != _maxGuestNum)
                {
                    _maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MinNumOfDays
        {
            get => _minNumOfDays;
            set
            {
                if (value != _minNumOfDays)
                {
                    _minNumOfDays = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AllowedNumOfDaysForCancelation
        {
            get => _allowedNumOfDaysForCancelation;
            set
            {
                if (value != _allowedNumOfDaysForCancelation)
                {
                    _allowedNumOfDaysForCancelation = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhotoLinks
        {
            get => _photoLinks;
            set
            {
                if (_photoLinks != value)
                {
                    _photoLinks = value;
                    OnPropertyChanged();
                }
            }
        }

        public void AddPhotos()
        {
            PhotoLinks = PhotoLinks.Replace("\r\n", "|");
            string[] photoLinks = PhotoLinks.Split("|");
            foreach (string photoLink in photoLinks)
            {
                Photo photo = new Photo(photoLink, 'A', NewAccommodation.Id);
                NewAccommodation.Photos.Add(photo);
                _photoService.Create(photo);
            }
        }

        private void PrepareAccommodationForCreation()
        {
            NewAccommodation.OwnerId = LoggedUser.ID;
            NewAccommodation.Owner = LoggedUser;
            NewAccommodation.Location = _locationDAO.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim());
            NewAccommodation.LocationId = _locationDAO.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim()).Id;
            NewAccommodation.Type = TYPE.HOTEL; //prepraviti
        }

        public bool CanCreateAccommodationExecute()
        {
            return true;
        }

        public void CreateAccommodationExecute()
        {
            Create(NewAccommodation);   
        }

        //----------------------------------------------------------------------------------------------
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

        public void Update(Accommodation updatedAccommodation, int id)
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
