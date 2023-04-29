﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using TouristAgency.Storage;
using TouristAgency.Repository;
using TouristAgency.Base;

namespace TouristAgency.Service
{
    public class OwnerReviewService 
    {
        private readonly App _app;
        public OwnerReviewRepository OwnerReviewRepository { get; }

        public OwnerReviewService()
        {
            _app = (App)App.Current;
            OwnerReviewRepository = _app.OwnerReviewRepository;
        }

        public List<OwnerReview> GetByOwnerId(int id)
        {
            return OwnerReviewRepository.GetAll().FindAll(o => o.Reservation.Accommodation.OwnerId == id);
        }

        public List<OwnerReview> GetReviewedReservationsByOwnerId(int id)
        {
            List<OwnerReview> reviewedReservations = new List<OwnerReview>();

            foreach (var ownerReview in OwnerReviewRepository.GetAll())
            {
                int ownerId = ownerReview.Reservation.Accommodation.OwnerId;

                if (ownerId == id &&  ownerReview.Reservation.Status == ReviewStatus.REVIEWED)
                {
                    reviewedReservations.Add(ownerReview);
                }
            }
            return reviewedReservations;
        }

        
    }
}
