using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.CreationFeature;
using TouristAgency.TourRequests;
using TouristAgency.TourRequests.AcceptRequestFeature;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;
using TouristAgency.Users;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.AcceptRequestFeature
{
    public class ComplexTourRequestPartDisplayViewModel : BurgerMenuViewModelBase
    {
        private App _app;
        private Guide _loggedInGuide;
        private ComplexTourRequest _request;
        private ObservableCollection<TourRequest> _parts;

        private TourService _tourService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand AcceptTourRequestCmd { get; set; }

        public DelegateCommand SuggestDateCmd { get; set; }

        public ComplexTourRequestPartDisplayViewModel(ComplexTourRequest request)
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            MenuVisibility = "Hidden";
            _request = request;
            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
            InstantiateMenuCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
        }

        private void InstantiateCollections()
        {
            Parts = new ObservableCollection<TourRequest>(Request.Parts.FindAll(p => p.Status == Util.TourRequestStatus.PENDING));
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => AlwaysExecute());
            AcceptTourRequestCmd = new DelegateCommand(param => AcceptTourRequestExecute(), param => AlwaysExecute());
            SuggestDateCmd = new DelegateCommand(param => SuggestDateExecute(), param => AlwaysExecute());
        }

        public bool AlwaysExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new TourRequestDisplayViewModel();
        }

        public void AcceptTourRequestExecute()
        {
            foreach (TourRequest toureq in Request.Parts)
            {
                if(toureq.GuideID == _loggedInGuide.ID)
                {
                    MessageBox.Show("You have already signed up for a part of this tour!", "Error");
                    return;
                }
            }
            if (SelectedTourRequest!=null)
                _app.CurrentVM = new TourCreationViewModel(SelectedTourRequest, Util.TourCreationScenario.ACCEPT_TOURREQ);
        }

        public void SuggestDateExecute()
        {
            if(SelectedTourRequest != null)
            { 
                DateTime start = SelectedTourRequest.StartDate;
                while(start < SelectedTourRequest.EndDate)
                {
                    if (!_tourService.IsGuideBooked(_loggedInGuide, start))
                    {
                        MessageBox.Show("Suggested date: " + start, "Suggestion");
                        return;
                    }
                    start = start.AddDays(1);
                }
            }
        }

        public ComplexTourRequest Request
        {
            get => _request;
            set
            {
                if (value != _request)
                {
                    _request = value;
                    OnPropertyChanged("Request");
                }
            }
        }

        public TourRequest SelectedTourRequest
        {
            get;
            set;
        }

        public ObservableCollection<TourRequest> Parts
        {
            get => _parts;
            set
            {
                if(value != _parts)
                {
                    _parts = value;
                }
            }
        }

        public DateTime SelectedDate { get; set; }
    }
}
