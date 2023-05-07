using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.AccommodationRenovation.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;

namespace TouristAgency.AccommodationRenovation.RenovationHistoryFeature
{
    public class RenovationHistoryViewModel : ViewModelBase,IObserver
    {
        private RenovationService _renovationService;
        private App app;
        public Renovation SelectedRenovation { get; set; }
        public ObservableCollection<Renovation> Renovations { get; set; }

        public DelegateCommand CancelRenovationCmd { get; set; }

        public RenovationHistoryViewModel()
        {
            app = (App)App.Current;
            _renovationService = new();
            _renovationService.RenovationRepository.Subscribe(this);
            SelectedRenovation = new();
            Renovations = new();
            LoadRenovations();

            CancelRenovationCmd = new DelegateCommand(param => CancelRenovationCmdExecute(),param => CanCancelRenovationCmdExecute());
        }

        private void LoadRenovations()
        {
            Renovations.Clear();
            foreach(var renovation in _renovationService.GetRenovationsByOwnerId(app.LoggedUser.ID))
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
            return SelectedRenovation != null;
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
                    MessageBox.Show("Renovation is already done");
                }
                else if(dateDiff > 5)
                {
                    SelectedRenovation.IsCanceled = true;
                    _renovationService.RenovationRepository.Update(SelectedRenovation, SelectedRenovation.Id);
                    MessageBox.Show("Renovation has been canceled");
                }
               // else
                //{
                //    MessageBox.Show($"Renovation is in {dateDiff:F0} days, hence it can't be canceled");
                //}
            }
        }

        private MessageBoxResult CancelRenovationDialogue()
        {
            string sMessageBoxText;
            string sCaption;

            sMessageBoxText = $"Do you want to cancel renovation?\nStart Date:\t{SelectedRenovation.Start}\nEnd Date:\t\t{SelectedRenovation.End}";
            sCaption = "Renovation Cancelation Dialog";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }
    }
}
