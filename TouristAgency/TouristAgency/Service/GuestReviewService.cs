using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Storage;
using TouristAgency.Interfaces;
using TouristAgency.Model;

namespace TouristAgency.Service
{
    public class GuestReviewService : ICrud<GuestReview>, ISubject
    {
        private readonly GuestReviewStorage _storage;
        private readonly List<GuestReview> _guestReviews;
        public List<IObserver> _observers;

        public GuestReviewService()
        {
            _storage = new GuestReviewStorage();
            _guestReviews = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _guestReviews.Count == 0 ? 0 : _guestReviews.Max(g => g.Id) + 1;
        }

        public GuestReview FindById(int id)
        {
            return _guestReviews.Find(g => g.Id == id);
        }

        public GuestReview Create(GuestReview newReview)
        {
            newReview.Id = GenerateId();
            _guestReviews.Add(newReview);
            _storage.Save(_guestReviews);
            NotifyObservers();

            return newReview;
        }

        public GuestReview Update(GuestReview updatedGuestReview, int id)
        {
            GuestReview currentGuestReview = FindById(id);
            if (currentGuestReview == null)
                return null;

            currentGuestReview.Cleanliness = updatedGuestReview.Cleanliness;
            currentGuestReview.RuleAbiding = updatedGuestReview.RuleAbiding;
            currentGuestReview.Comment = updatedGuestReview.Comment;
            currentGuestReview.ReviewDate = DateTime.Now;

            _storage.Save(_guestReviews);
            NotifyObservers();

            return currentGuestReview;
        }

        public void Delete(int id)
        {
            GuestReview guestReview = FindById(id);
            _guestReviews.Remove(guestReview);
            _storage.Save(_guestReviews);
            NotifyObservers();
        }

        public List<GuestReview> GetAll()
        {
            return _guestReviews;
        }

        public void LoadReservationsToGuestReviews(List<Reservation> reservations)
        {
            foreach(GuestReview guestReview in _guestReviews)
            {
                Reservation reservation = reservations.Find(r => r.Id == guestReview.ReservationId);
                if(reservation != null)
                {
                    guestReview.Reservation = reservation;
                }
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
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

    }
}
