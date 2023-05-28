using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature;
using TouristAgency.Users;

namespace TouristAgency.Tours.TourRequestFeatures.CreationFeature
{
    public class ComplexTourRequestCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Tourist _loggedInTourist;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand AddNewPartCmd { get; set; }
        public DelegateCommand ListOfPartsCmd { get; set; }

        public ComplexTourRequestCreationViewModel(Tourist tourist)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;

            InstantiateCommands();
        }

        public void InstantiateCommands()
        {
            AddNewPartCmd = new DelegateCommand(param => AddNewPartExecute(), param => CanAddNewPartExecute());
            ListOfPartsCmd = new DelegateCommand(param =>  ListOfPartsExecute(), param => CanListOfPartsExecute());
        }

        public bool CanAddNewPartExecute()
        {
            return true;
        }

        public void AddNewPartExecute() 
        {
            NewPartOfComplexTourCreation creation = new NewPartOfComplexTourCreation(_loggedInTourist);
            creation.Show();
        }

        public bool CanListOfPartsExecute()
        {
            return true;
        }
         public void ListOfPartsExecute()
        {
            ComplexTourDetailsDisplay display = new ComplexTourDetailsDisplay(_loggedInTourist);
            display.Show();
        }
    }
}
