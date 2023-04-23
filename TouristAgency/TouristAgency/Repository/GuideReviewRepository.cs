using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;

namespace TouristAgency.Repository
{
    public class GuideReviewRepository : ICrud<GuideReview>, ISubject
    {
        private readonly IStorage<GuideReview> _storage;
        private readonly List<GuideReview> _guideReviews;
        private readonly List<IObserver> _observers;

        public GuideReviewRepository(IStorage<GuideReview> storage)
        {
            _storage = storage;
            _guideReviews = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _guideReviews.Count == 0 ? 0 : _guideReviews.Max(g => g.ID) + 1;
        }

        public GuideReview GetById(int id)
        {
            return _guideReviews.Find(o => o.ID == id);
        }

        public GuideReview Create(GuideReview newReview)
        {
            newReview.ID = GenerateId();
            _guideReviews.Add(newReview);
            _storage.Save(_guideReviews);
            NotifyObservers();

            return newReview;
        }

        public GuideReview Update(GuideReview updatedGuideReview, int id)
        {
            GuideReview currentGuideReview = GetById(id);
            if (currentGuideReview == null)
                return null;

            currentGuideReview.Tour = updatedGuideReview.Tour;
            currentGuideReview.ReviewDate = DateTime.Now;
            currentGuideReview.Quality = updatedGuideReview.Quality;
            currentGuideReview.TourOrganization = updatedGuideReview.TourOrganization;
            currentGuideReview.Attractions = updatedGuideReview.Attractions;
            currentGuideReview.Knowledge = updatedGuideReview.Knowledge;
            currentGuideReview.Language = updatedGuideReview.Language;
            currentGuideReview.SocialInteraction = updatedGuideReview.SocialInteraction;
            currentGuideReview.Comment = updatedGuideReview.Comment;

            _storage.Save(_guideReviews);
            NotifyObservers();

            return currentGuideReview;
        }

        public void Delete(int id)
        {
            GuideReview guideReview = GetById(id);
            if (guideReview == null)
                throw new Exception("Samo probavam");

            _guideReviews.Remove(guideReview);
            _storage.Save(_guideReviews);
            NotifyObservers();
        }

        public List<GuideReview> GetAll()
        {
            return _guideReviews;
        }

        public void LoadToursToGuideReviews(List<Tour> tours)
        {
            foreach (var guideReview in _guideReviews)
            {
                Tour tour = tours.Find(t => t.ID == guideReview.TourID);
                guideReview.Tour = tour;
            }
        }

        public void LoadTouristsToReviews(List<Tourist> tourists)
        {
            foreach (GuideReview guideReview in _guideReviews)
            {
                foreach (Tourist tourist in tourists)
                {
                    if (guideReview.TouristID == tourist.ID)
                    {
                        guideReview.Tourist = tourist;
                    }
                }
            }
        }

        public void LoadPhotosToReviews(List<Photo> photos)
        {
            foreach (GuideReview review in _guideReviews)
            {
                foreach (Photo photo in photos)
                {
                    if (photo.ExternalID == review.ID && photo.Type == 'G')
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
