﻿using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.ViewModel
{
    public class GuideHomeViewModel : ViewModelBase, ICloseable
    {
        private App _app;
        private Window _window;
        private Guide _loggedInGuide;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand CreateTourCmd { get; set; }
        public DelegateCommand ActiveTourCmd { get; set; }
        public DelegateCommand CancelTourCmd { get; set; }
        public DelegateCommand TourStatisticsCmd { get; set; }
        public DelegateCommand GuideProfileCmd { get; set; }

        public GuideHomeViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuide = guide;
            _window = window;
            InstantiateCommands();
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            CreateTourCmd = new DelegateCommand(param => CreateTourExecute(), param => CanCreateTourExecute());
            ActiveTourCmd = new DelegateCommand(param => ActiveTourExecute(), param => CanActiveTourExecute());
            CancelTourCmd = new DelegateCommand(param => CancelTourExecute(), param => CanCancelTourExecute());
            TourStatisticsCmd = new DelegateCommand(param => TourStatisticsExecute(), param => CanTourStatisticsExecute());
            GuideProfileCmd = new DelegateCommand(param => GuideProfileExecute(), param => CanGuideProfileExecute());
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }

        public bool CanCreateTourExecute()
        {
            return true;
        }

        public void CreateTourExecute()
        {
            TourCreation creation = new TourCreation(_loggedInGuide);
            creation.Show();
        }

        public bool CanActiveTourExecute()
        {
            return true;
        }

        public void ActiveTourExecute()
        {
            ActiveTourDisplay activeTour = new ActiveTourDisplay(_loggedInGuide);
            activeTour.Show();
        }

        public bool CanCancelTourExecute()
        {
            return true;
        }

        public void CancelTourExecute()
        {
            CancelTourDisplay cancelTour = new CancelTourDisplay(_loggedInGuide);
            cancelTour.Show();
        }

        public bool CanTourStatisticsExecute()
        {
            return true;
        }

        public void TourStatisticsExecute()
        {
            TourStatisticsDisplay tourStatistics = new TourStatisticsDisplay(_loggedInGuide);
            tourStatistics.Show();
        }

        public bool CanGuideProfileExecute()
        {
            return true;
        }

        public void GuideProfileExecute()
        {
            GuideProfileDisplay profileDisplay = new GuideProfileDisplay(_loggedInGuide);
            profileDisplay.Show();
        }
    }
}
