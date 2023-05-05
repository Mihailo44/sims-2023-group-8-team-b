using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Users;

namespace TouristAgency.TourRequests
{
    public class TourRequestDisplayViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Guide _loggedInGuide;
        private ObservableCollection<TourRequest> _tourRequests;

        private TourRequestService _tourRequestService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand AcceptTourRequestCmd { get; set; }

        public TourRequestDisplayViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
        }

        private void InstantiateServices()
        {
            _tourRequestService = new TourRequestService();
        }

        private void InstantiateCollections()
        {
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetPendingTourRequests());
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            AcceptTourRequestCmd = new DelegateCommand(param => AcceptTourRequestExecute(), param => CanAcceptTourRequestExecute());
        }


        public ObservableCollection<TourRequest> TourRequests
        {
            get { return _tourRequests; }
            set
            {
                if(value != _tourRequests)
                {
                    _tourRequests = value;
                    OnPropertyChanged("TourRequests");
                }
            }
        }

        public TourRequest SelectedTourRequest
        {
            get;
            set;
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public bool CanAcceptTourRequestExecute()
        {
            return true;
        }

        public void AcceptTourRequestExecute()
        {
            if(SelectedTourRequest != null)
                _app.CurrentVM = new TourCreationViewModel(SelectedTourRequest);
        }
    }
}
