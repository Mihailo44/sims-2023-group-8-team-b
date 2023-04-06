using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using TouristAgency.Service;
using TouristAgency.Base;
using TouristAgency.Model.Enums;
using System.Windows;
using System.Windows.Input;

namespace TouristAgency.ViewModel
{
    public class GuestReviewViewModel : ViewModelBase,ICreate,ICloseable
    {
        private readonly GuestReviewService _guestReview;
        private readonly ReservationService _reservationService;

        private DateTime _reviewDate;
        private int _cleanliness;
        private int _ruleAbiding;
        private int _communication;
        private int _overallImpression;
        private int _noiseLevel;
        private string _comment;
        private readonly Window _window;
        private App app = (App)App.Current;

        public GuestReview NewGuestReview { get; set; }
        public DelegateCommand CreateCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public GuestReviewViewModel()
        {
            _guestReview = app.GuestReviewService;
        }

        public GuestReviewViewModel(Reservation reservation,Window window)
        {
            _guestReview = app.GuestReviewService;
            _reservationService = app.ReservationService;
            _window = window;
            NewGuestReview = new();
            NewGuestReview.Reservation = reservation;
            NewGuestReview.ReservationId = reservation.Id;
            CreateCmd = new DelegateCommand(param => CreateGuestReviewExecute(),param => CanCreateGuestReviewExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(),param=> CanCloseWindowExecute());
        }

        public DateTime ReviewDate
        {
            get => _reviewDate;
            set
            {
                if (value != _reviewDate)
                {
                    _reviewDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        public int RuleAbiding
        {
            get => _ruleAbiding;
            set
            {
                if (value != _ruleAbiding)
                {
                    _ruleAbiding = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Communication
        {
            get => _communication;
            set
            {
                if (value != _communication)
                {
                    _communication = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OverallImpression
        {
            get => _overallImpression;
            set
            {
                if (value != _overallImpression)
                {
                    _overallImpression = value;
                    OnPropertyChanged();
                }
            }
        }

        public int NoiseLevel
        {
            get => _noiseLevel;
            set
            {
                if (value != _noiseLevel)
                {
                    _noiseLevel = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CanCreateGuestReviewExecute()
        {
            if (NewGuestReview.Reservation.Status == ReviewStatus.UNREVIEWED)
            {
                return true;
            }
            else
            {
                return false;
            }   
        }

        public void CreateGuestReviewExecute()
        {
            try
            {
                if (NewGuestReview.Reservation.Status == ReviewStatus.UNREVIEWED)
                {
                    _guestReview.Create(NewGuestReview);
                    NewGuestReview.Reservation.Status = ReviewStatus.REVIEWED;
                    _reservationService.Update(NewGuestReview.Reservation, NewGuestReview.ReservationId);
                    MessageBox.Show("Guest review created successfully");
                }
                else
                {
                    MessageBox.Show("This guest has already been reviewed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _window.Close();
            }
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            _window.Close();
        }

        //-------------------------------------------------------------------------------------------------

        public void Subsribe(IObserver observer)
        {
            _guestReview.Subscribe(observer);
        }
    }
}
