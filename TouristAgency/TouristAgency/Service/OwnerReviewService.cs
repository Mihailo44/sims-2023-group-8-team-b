﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using TouristAgency.Storage;
using TouristAgency.Model.Enums;

namespace TouristAgency.Service
{
    public class OwnerReviewService : ICrud<OwnerReview>,ISubject
    {
        private readonly OwnerReviewStorage _storage;
        private readonly List<OwnerReview> _ownerReviews;
        private readonly List<IObserver> _observers;

        public OwnerReviewService()
        {
            _storage = new OwnerReviewStorage();
            _ownerReviews = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _ownerReviews.Count == 0 ? 0 : _ownerReviews.Max(g => g.Id) + 1;
        }

        public OwnerReview FindById(int id)
        {
            return _ownerReviews.Find(o => o.Id == id);
        }

        public OwnerReview Create(OwnerReview newReview)
        {
            newReview.Id = GenerateId();
            _ownerReviews.Add(newReview);
            _storage.Save(_ownerReviews);
            NotifyObservers();

            return newReview;
        }

        public OwnerReview Update(OwnerReview updatedOwnerReview,int id)
        {
            OwnerReview currentOwnerReview = FindById(id);
            if (currentOwnerReview == null)
                return null;

            currentOwnerReview.Cleanliness = updatedOwnerReview.Cleanliness;
            currentOwnerReview.OwnerCorrectness = updatedOwnerReview.OwnerCorrectness;
            currentOwnerReview.Comfort = updatedOwnerReview.Comfort;
            currentOwnerReview.ReviewDate = DateTime.Now;

            _storage.Save(_ownerReviews);
            NotifyObservers();

            return currentOwnerReview;
        }

        public void Delete(int id)
        {
            OwnerReview ownerReview = FindById(id);
            if (ownerReview == null)
                throw new Exception("Samo probavam");

            _ownerReviews.Remove(ownerReview);
            _storage.Save(_ownerReviews);
            NotifyObservers();
        }

        public List<OwnerReview> GetAll()
        {
            return _ownerReviews;
        }

        public List<OwnerReview> GetByOwnerId(int id)
        {
            return _ownerReviews.FindAll(o => o.Reservation.Accommodation.OwnerId == id);
        }

        public List<OwnerReview> GetReviewedReservationsByOwnerId(int id) //kako da je nazovem
        {
            List<OwnerReview> reviewedReservations = new List<OwnerReview>();

            foreach (var ownerReview in _ownerReviews)
            {
                if(ownerReview.Reservation.Status == GuestReviewStatus.REVIEWED)
                {
                    reviewedReservations.Add(ownerReview);
                }
            }
            return reviewedReservations;
        }

        public void LoadReservationsToOwnerReviews(List<Reservation> reservations)
        {
            foreach(var ownerReview in _ownerReviews)
            {
                Reservation reservation = reservations.Find(r => r.Id == ownerReview.ReservationId);
                ownerReview.Reservation = reservation;
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}