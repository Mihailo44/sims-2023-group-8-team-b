using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Requests.Domain;
using TouristAgency.Reservations.Domain;
using TouristAgency.Users;

namespace TouristAgency.Requests
{
    public class PostponementRequestDisplayViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Guest _loggedInGuest;

        private ObservableCollection<Reservation> _reservations;
        private ObservableCollection<PostponementRequest> _requests;

        private DateTime _start;
        private DateTime _end;

        private ReservationService _reservationService;
        private PostponementRequestService _postponementRequestService;

        public DelegateCommand CreateCmd { get; set; }
        public DelegateCommand CancelCmd { get; set; }
        public DelegateCommand NotificationCmd { get; set; }

        public PostponementRequestDisplayViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _postponementRequestService = new PostponementRequestService();
        }

        private void InstantiateCollections()
        {
            Start = DateTime.Now;
            End = DateTime.Now;

            Reservations = new ObservableCollection<Reservation>(_reservationService.GetByGuestId(_loggedInGuest.ID));
            Requests = new ObservableCollection<PostponementRequest>(_postponementRequestService.GetByGuestId(_loggedInGuest.ID));
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
            NotificationCmd = new DelegateCommand(param => NotificationExecute(), param => CanNotificationExecute());
            CancelCmd = new DelegateCommand(param => CancelExecute(), param => CanCancelExecute());
        }

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                if (value != _reservations)
                {
                    _reservations = value;
                    OnPropertyChanged("Reservations");
                }
            }
        }

        public ObservableCollection<PostponementRequest> Requests
        {
            get => _requests;
            set
            {
                if (value != _requests)
                {
                    _requests = value;
                    OnPropertyChanged("Requests");
                }
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                }
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                {
                    _end = value;
                    OnPropertyChanged("End");
                }
            }
        }

        public Reservation SelectedReservation
        {
            get;
            set;
        }

        bool CanCreateExecute()
        {
            return true;
        }

        void CreateExecute()
        {
            DateTime today = DateTime.Now;
            if (Start >= today && End >= today && End >= Start)
            {
                if (SelectedReservation != null)
                {
                    PostponementRequest request = new PostponementRequest(SelectedReservation, Start, End);
                    _postponementRequestService.PostponementRequestRepository.Create(request);
                    Requests.Add(request);
                }
            }
            else
            {
                MessageBox.Show("You have entered invalid dates. Try again.");
            }

        }

        bool CanNotificationExecute()
        {
            return true;
        }

        void NotificationExecute()
        {
            MessageBox.Show(_postponementRequestService.ShowNotifications(_loggedInGuest.ID));
        }

        bool CanCancelExecute()
        {
            return true;
        }

        void CancelExecute()
        {
            if (SelectedReservation != null)
            {
                bool result;
                result = _reservationService.CancelReservation(SelectedReservation);
                if (result == true)
                {
                    Reservations.Remove(SelectedReservation);
                    MessageBox.Show("Your reservation was successfully canceled");
                }
                else
                {
                    MessageBox.Show(
                        "Reservation couldn't be canceled. You can only cancel 24 hours before the start of the reservation");
                }
            }
        }

    }
}
