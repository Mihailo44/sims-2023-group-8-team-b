using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Util;

namespace TouristAgency.Repository
{
    public class OwnerReviewRepository : ICrud<OwnerReview>, ISubject
    {
        private readonly IStorage<OwnerReview> _storage;
        private readonly List<OwnerReview> _ownerReviews;
        private readonly List<IObserver> _observers;

        public OwnerReviewRepository(IStorage<OwnerReview> storage)
        {
            _storage = storage;
            _ownerReviews = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _ownerReviews.Count == 0 ? 0 : _ownerReviews.Max(g => g.Id) + 1;
        }

        public OwnerReview GetById(int id)
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

        public OwnerReview Update(OwnerReview updatedOwnerReview, int id)
        {
            OwnerReview currentOwnerReview = GetById(id);
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
            OwnerReview ownerReview = GetById(id);
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

        public void LoadReservationsToOwnerReviews(List<Reservation> reservations)
        {
            foreach (var ownerReview in _ownerReviews)
            {
                Reservation reservation = reservations.Find(r => r.Id == ownerReview.ReservationId);
                ownerReview.Reservation = reservation;
            }
        }

        public void LoadPhotosToReviews(List<Photo> photos)
        {
            foreach (OwnerReview review in _ownerReviews)
            {
                foreach (Photo photo in photos)
                {
                    if (photo.ExternalID == review.Id && photo.Type == 'O')
                    {
                        review.Photos.Add(new Photo(photo));
                    }
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
