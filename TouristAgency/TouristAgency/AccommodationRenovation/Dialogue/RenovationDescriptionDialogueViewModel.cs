﻿using System.Windows;
using TouristAgency.AccommodationRenovation.Domain;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.AccommodationRenovation.Dialogue
{
    public class RenovationDescriptionDialogueViewModel : ViewModelBase, ICloseable, ICreate
    {
        private string _description;
        private readonly Window _window;
        private Renovation _renovation;
        private RenovationService _renovationService;
        private AccommodationService _accommodationService;

        public DelegateCommand CreateCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public RenovationDescriptionDialogueViewModel(Renovation renovation, Window window)
        {
            _renovationService = new();
            _accommodationService = new();
            _window = window;
            Renovation = renovation;

            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            CreateCmd = new DelegateCommand(param => CreateCmdExecute(), param => CanCreateCmdExecute());
        }

        public string Description 
        { 
            get => _description;
            set
            {
                if(_description != value)
                {
                    _description = value;
                    CreateCmd.OnCanExecuteChanged();
                }
            } 
        }

        public Renovation Renovation
        {
            get => _renovation;
            set
            {
                if (_renovation != value)
                {
                    _renovation = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }

        public bool CanCreateCmdExecute()
        {
            return !string.IsNullOrEmpty(Description);
        }

        public void CreateCmdExecute()
        {
            Renovation.Description = Description.Trim();
            _renovationService.RenovationRepository.Create(Renovation);
            MessageBox.Show("Renovation has been scheduled");
            _window.Close();
        }
    }
}
