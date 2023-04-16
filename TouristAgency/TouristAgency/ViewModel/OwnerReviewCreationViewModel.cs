using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.Enums;

namespace TouristAgency.ViewModel
{
    public class OwnerReviewCreationViewModel : ViewModelBase, ICreate
    {
        private readonly Window _window;
        private App _app;
        private Guest _loggedInGuest;
        private ObservableCollection<Reservation> _unreviewedReservations;
        private OwnerReview _newOwnerReview;
        
        public DelegateCommand CreateCmd { get; }

        public OwnerReviewCreationViewModel(Guest guest, Window window)
        {
            _window = window;
            _app = (App)App.Current;
            _loggedInGuest = guest;
            UnreviewedReservations = new ObservableCollection<Reservation>(_app.ReservationService.GetUnreviewedByGuestId(guest.ID));
            NewOwnerReview = new OwnerReview();

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

                    _app.ReservationService.Update(NewOwnerReview.Reservation, NewOwnerReview.ReservationId);

                    AddPhotos();
                    _app.OwnerReviewService.Create(NewOwnerReview);
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
            int ownerReviewID = _app.OwnerReviewService.GenerateId() - 1;
            if (PhotoLinks != null)
            {
                PhotoLinks = PhotoLinks.Replace("\r\n", "|");
                string[] photoLinks = PhotoLinks.Split("|");
                foreach (string photoLink in photoLinks)
                {
                    Photo photo = new Photo(photoLink, 'O', ownerReviewID);
                    NewOwnerReview.Photos.Add(photo);
                    _app.PhotoService.Create(photo);
                }
            }
        }
    }
}
