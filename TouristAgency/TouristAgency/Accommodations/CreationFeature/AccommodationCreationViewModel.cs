﻿using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using System.ComponentModel;
using TouristAgency.Util;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Users.HomeDisplayFeature;
using System.Collections.Generic;

namespace TouristAgency.Accommodations.CreationFeature
{
    public class AccommodationCreationViewModel : ViewModelBase, ICloseable, ICreate, IDataErrorInfo
    {
        private AccommodationService _accommodationService;
        private PhotoRepository _photoRepository;
        private LocationService _locationService;
        private Owner _owner;
        private int _ownerId;
        private string _photoLinks;
        private App _app = (App)Application.Current;

        public List<string> TypeComboValues { get; set; } = new();
 
        public Accommodation NewAccommodation { get; set; }
        public Location NewLocation { get; set; }
        public Owner LoggedUser { get; }

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }

        public AccommodationCreationViewModel()
        {
            LoggedUser = _app.LoggedUser;
            InstantiateServices();
            NewAccommodation = new();
            NewLocation = new();
            InstantiateCommands();
            FillCombo();
        }

        private void InstantiateServices()
        {
            _accommodationService = new();
            _photoRepository = _app.PhotoRepository;
            _locationService = new();
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateAccommodationExecute(), param => CanCreateAccommodationExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(), param => CanCloseWindowExecute());
        }

        private void FillCombo()
        {
            TypeComboValues.Add(TYPE.APARTMENT.ToString());
            TypeComboValues.Add(TYPE.HOTEL.ToString());
            TypeComboValues.Add(TYPE.HUT.ToString());
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

        public string PhotoLinks
        {
            get => _photoLinks;
            set
            {
                if (_photoLinks != value)
                {
                    _photoLinks = value;
                    CreateCmd.OnCanExecuteChanged();
                }
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "PhotoLinks")
                {
                    if (string.IsNullOrEmpty(PhotoLinks))
                        return "Required field";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "PhotoLinks" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        public void AddPhotos()
        {
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'A', NewAccommodation.Id);
                    NewAccommodation.Photos.Add(photo);
                    _photoRepository.Create(photo);
                }
            }
        }

        private void PrepareAccommodationForCreation()
        {
            NewAccommodation.Owner.ID = LoggedUser.ID;
            NewAccommodation.Owner = LoggedUser;
            NewAccommodation.Location = _locationService.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim());
            NewAccommodation.Location.ID = _locationService.FindByCountryAndCity(NewLocation.Country.Trim(), NewLocation.City.Trim()).ID;
        }

        public bool CanCreateAccommodationExecute()
        {
            return !string.IsNullOrEmpty(PhotoLinks);
        }
   
        public void CreateAccommodationExecute()
        {
            PrepareAccommodationForCreation();
            try
            {
                _accommodationService.AccommodationRepository.Create(NewAccommodation);
                AddPhotos();
                MessageBox.Show("Accommodation created successfully");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _app.CurrentVM = new OwnerHomeViewModel();
            }
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            _app.CurrentVM = new OwnerHomeViewModel();
        }
    }
}
