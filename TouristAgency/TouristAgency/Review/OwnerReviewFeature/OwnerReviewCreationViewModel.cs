using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Review.Domain;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Review.OwnerReviewFeature
{
    public class OwnerReviewCreationViewModel : ViewModelBase, ICreate
    {
        private App _app;
        private Guest _loggedInGuest;
        private readonly Window _window;

        private ObservableCollection<Reservation> _unreviewedReservations;

        private OwnerReview _newOwnerReview;

        private ReservationService _reservationService;
        private OwnerReviewService _ownerReviewService;

        public DelegateCommand CreateCmd { get; set; }

        public OwnerReviewCreationViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = window;

            InstantiateServices();
            InstantiateCollections();
            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _ownerReviewService = new OwnerReviewService();
        }

        private void InstantiateCollections()
        {
            NewOwnerReview = new OwnerReview();

            UnreviewedReservations = new ObservableCollection<Reservation>(_reservationService.GetUnreviewedByGuestId(_loggedInGuest.ID));
        }

        private void InstantiateCommands()
        {
            CreateCmd = new DelegateCommand(param => CreateExecute(), param => CanCreateExecute());
        }

        public ObservableCollection<Reservation> UnreviewedReservations
        {
            get => _unreviewedReservations;
            set
            {
                if (value != _unreviewedReservations)
                {
                    _unreviewedReservations = value;
                    OnPropertyChanged("UnreviewedReservations");
                }
            }
        }

        public OwnerReview NewOwnerReview
        {
            get => _newOwnerReview;
            set
            {
                if (value != _newOwnerReview)
                {
                    _newOwnerReview = value;
                    OnPropertyChanged("NewOwnerReview");
                }
            }
        }

        public Reservation SelectedReservation
        {
            get;
            set;
        }

        public string PhotoLinks
        {
            get;
            set;
        }

        public bool CanCreateExecute()
        {
            return true;
        }

        public void CreateExecute()
        {
            if (SelectedReservation != null)
            {
                if (SelectedReservation.OStatus == ReviewStatus.UNREVIEWED)
                {
                    NewOwnerReview.ReviewDate = DateTime.Now;
                    NewOwnerReview.ReservationId = SelectedReservation.Id;
                    NewOwnerReview.Reservation = SelectedReservation;
                    NewOwnerReview.Reservation.OStatus = ReviewStatus.REVIEWED;

                    _reservationService.ReservationRepository.Update(NewOwnerReview.Reservation, NewOwnerReview.ReservationId);

                    AddPhotos();
                    _ownerReviewService.OwnerReviewRepository.Create(NewOwnerReview);
                    UnreviewedReservations.Remove(SelectedReservation);
                    MessageBox.Show("Owner review is submitted successfully");
                }
                else
                {
                    MessageBox.Show("Owner is already reviewed");
                }
            }
        }

        public void AddPhotos()
        {
            int ownerReviewID = _ownerReviewService.OwnerReviewRepository.GenerateId() - 1;
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'O', ownerReviewID);
                    NewOwnerReview.Photos.Add(photo);
                    _app.PhotoRepository.Create(photo);
                }
            }
        }
    }
}
