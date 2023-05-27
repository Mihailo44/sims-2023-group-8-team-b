using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.RenovationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.RenovationFeatures.RenovationHistoryFeature
{
    public class RenovationCancelationDialogueViewModel : ViewModelBase,ICloseable
    {
        private readonly Window _window;
        private RenovationService _renovationService;

        public Renovation SelectedRenovation { get; }

        public DelegateCommand CloseCmd { get; }
        public DelegateCommand CancelRenovationCmd { get; }

        public RenovationCancelationDialogueViewModel(Renovation renovation,Window window)
        {
            _window = window;
            _renovationService = new();
            SelectedRenovation = renovation;
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            CancelRenovationCmd = new DelegateCommand(param => CancelRenovationCmdExecute(),param => CanCloseCmdExecute());
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }

        public bool CanCancelRenovationCmdExecute()
        {
            return true;
        }

        public void CancelRenovationCmdExecute()
        {
            DateTime today = DateTime.Now;
            double dateDiff = (SelectedRenovation.Start - today).TotalDays;

            if (today > SelectedRenovation.End)
            {
                MessageBox.Show("Renovation is already done", "Renovation Cancelation Dialogue",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else if (dateDiff > 5)
            {
                _renovationService.CancelRenovation(SelectedRenovation);
                MessageBox.Show("Renovation has been canceled successfully", "Renovation Cancelation Dialogue",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Renovation is in less than 5 days, so it can't be canceled", "Renovation Cancelation Dialogue",MessageBoxButton.OK,MessageBoxImage.Stop);
            }
        }
    }
}
