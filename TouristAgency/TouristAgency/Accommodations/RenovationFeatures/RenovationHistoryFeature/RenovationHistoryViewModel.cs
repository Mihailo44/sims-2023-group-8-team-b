﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.RenovationFeatures.RenovationHistoryFeature
{
    public class RenovationHistoryViewModel : ViewModelBase,IObserver
    {
        private RenovationService _renovationService;
        private readonly App _app;
        public Renovation SelectedRenovation { get; set; }
        public Accommodation SelectedAccommodation { get; } = new();
        public ObservableCollection<Renovation> Renovations { get; set; }

        public DelegateCommand CancelRenovationCmd { get; set; }

        public RenovationHistoryViewModel(Accommodation accommodation)
        {
            _app = (App)App.Current;
            _renovationService = new();
            _renovationService.RenovationRepository.Subscribe(this);
            SelectedRenovation = new();
            SelectedAccommodation = accommodation;
            Renovations = new();
            LoadRenovations();

            CancelRenovationCmd = new DelegateCommand(param => CancelRenovationCmdExecute(),param => CanCancelRenovationCmdExecute());
        }

        private void LoadRenovations()
        {
            Renovations.Clear();
            foreach(var renovation in _renovationService.GetRenovationsByAccommodationId(SelectedAccommodation.Id))
            {
                Renovations.Add(renovation);
            }
        }

        public void Update()
        {
            LoadRenovations();
        }

        public bool CanCancelRenovationCmdExecute()
        {
            return SelectedRenovation != null && SelectedRenovation.IsCanceled == false;
        }

        public void CancelRenovationCmdExecute()
        {
            MessageBoxResult result = CancelRenovationDialogue();
            DateTime today = DateTime.Now;
            double dateDiff = (SelectedRenovation.Start - today).TotalDays;

            if (result == MessageBoxResult.Yes)
            {
                if (today > SelectedRenovation.End)
                {
                    MessageBox.Show("Renovation is already done","Renovation Cancelation Dialogue");
                }
                else if(dateDiff > 5)
                {
                    _renovationService.CancelRenovation(SelectedRenovation);
                    MessageBox.Show("Renovation has been canceled successfully", "Renovation Cancelation Dialogue");
                }
                else
                {
                    MessageBox.Show($"Renovation is in less than 5 days, so it can't be canceled", "Renovation Cancelation Dialogue");
                }
            }
        }

        private MessageBoxResult CancelRenovationDialogue()
        {
            string sMessageBoxText;
            string sCaption;

            sMessageBoxText = $"Do you want to cancel renovation?\nStart Date:\t{SelectedRenovation.Start.ToShortDateString()}\nEnd Date:\t{SelectedRenovation.End.ToShortDateString()}";
            sCaption = "Renovation Cancelation Dialog";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }
    }
}
