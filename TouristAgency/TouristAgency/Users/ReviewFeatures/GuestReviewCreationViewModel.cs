﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Base;
using System.Windows;
using TouristAgency.Util;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using System.ComponentModel;
using TouristAgency.Users.ReviewFeatures.Domain;

namespace TouristAgency.Users.ReviewFeatures
{
    public class GuestReviewCreationViewModel : ViewModelBase, ICreate, ICloseable, IDataErrorInfo
    {
        private readonly GuestReviewService _guestReviewService;
        private readonly ReservationService _reservationService;

        private DateTime _reviewDate;
        private int _cleanliness;
        private int _ruleAbiding;
        private int _communication;
        private int _overallImpression;
        private int _noiseLevel;
        private string _comment;
        private App app = (App)Application.Current;

        public List<int> ComboNumbers { get; set; }
        public GuestReview NewGuestReview { get; set; }
        public DelegateCommand CreateCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public GuestReviewCreationViewModel()
        {
            _guestReviewService = new();
        }

        public GuestReviewCreationViewModel(Reservation reservation)
        {
            _guestReviewService = new();
            _reservationService = new();
            NewGuestReview = new();
            ComboNumbers = new();
            FillCombos();
            NewGuestReview.Reservation = reservation;
            NewGuestReview.Reservation.Id = reservation.Id;
            CreateCmd = new DelegateCommand(param => CreateGuestReviewExecute(), param => CanCreateGuestReviewExecute());
            CloseCmd = new DelegateCommand(param => CloseWindowExecute(), param => CanCloseWindowExecute());
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
                    CreateCmd.OnCanExecuteChanged();
                }
            }
        }

        private void FillCombos()
        {
            for (int i = 1; i <= 5; i++)
            {
                ComboNumbers.Add(i);
            }
        }

        public bool CanCreateGuestReviewExecute()
        {
            if (NewGuestReview.Reservation.Status == ReviewStatus.UNREVIEWED && !string.IsNullOrEmpty(Comment))
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
                    NewGuestReview.Comment = Comment;
                    NewGuestReview.Reservation.Status = ReviewStatus.REVIEWED;
                    _guestReviewService.GuestReviewRepository.Create(NewGuestReview);
                    _reservationService.ReservationRepository.Update(NewGuestReview.Reservation, NewGuestReview.Reservation.Id);
                    MessageBox.Show("Guest review created successfully", "Guest Review Dialogue", MessageBoxButton.OK, MessageBoxImage.Information);
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
                app.CurrentVM = new OwnerHomeViewModel();
            }
        }

        public bool CanCloseWindowExecute()
        {
            return true;
        }

        public void CloseWindowExecute()
        {
            app.CurrentVM = new OwnerHomeViewModel();
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Required field";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Comment" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
