using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;
using TouristAgency.Model.Enums;

namespace TouristAgency.ViewModel
{
    public class AccommodationCreationViewModel : ViewModelBase, ICloseable, ICreate
    {
        private readonly AccommodationService _accommodation;
        private readonly PhotoService _photoService;
        private readonly LocationService _locationService;
        private Owner _owner;
        private int _ownerId;
        private string _name;
        private TYPE _type;
        private int _maxGuestNum;
        private int _minNumOfDays;
        private int _allowedNumOfDaysForCancelation;
        private string _photoLinks;
        private readonly Window _window;
        private App app = (App)App.Current;

        public Accommodation NewAccommodation { get; set; }
        public Location NewLocation { get; set; }
        public Owner LoggedUser { get; }
        public DelegateCommand CreateCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public AccommodationCreationViewModel()
        {
            _accommodation = app.AccommodationService;
        }

        public AccommodationCreationViewModel(Owner owner, Window window)
        {
            _accommodation = app.AccommodationService;
            _photoService = new PhotoService();
            _locationService = new LocationService();
            LoggedUser = owner;
            _window = window;
            NewAccommodation = new();
            NewLocation = new();
            CreateCmd = new DelegateCommand(param => CreateAccommodationExecute(), param => CanCreateAccommodationExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(), param => CanCloseWindowExecute());
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
            NewAccommodation.Location = _locationService.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim());
            NewAccommodation.LocationId = _locationService.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim()).Id;
        }

        public bool CanCreateAccommodationExecute()
        {
            return true;
        }

        public void CreateAccommodationExecute()
        {
            PrepareAccommodationForCreation();
            try
            {
                _accommodation.Create(NewAccommodation);
                AddPhotos();
                MessageBox.Show("Accommodation created successfully");
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _window.Close();
            }
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            _window.Close();
        }

        //----------------------------------------------------------------------------------------------

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
