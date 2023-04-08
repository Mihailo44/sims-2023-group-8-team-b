using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model.Enums;
using TouristAgency.Model;
using TouristAgency.Storage;

namespace TouristAgency.Service
{
    public class GuideReviewService : ICrud<GuideReview>, ISubject
    {
        private readonly GuideReviewStorage _storage;
        private readonly List<GuideReview> _guideReviews;
        private readonly List<IObserver> _observers;

        public GuideReviewService()
        {
            _storage = new GuideReviewStorage();
            _guideReviews = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _guideReviews.Count == 0 ? 0 : _guideReviews.Max(g => g.ID) + 1;
        }

        public GuideReview FindById(int id)
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
            GuideReview currentGuideReview = FindById(id);
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
            GuideReview guideReview = FindById(id);
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

        public List<GuideReview> GetByGuideId(int id)
        {
            return _guideReviews.FindAll(g => g.Tour.AssignedGuideID == id);
        }

        public List<GuideReview> GetReviewsForGuideTourID(int guideID, int tourID)
        {
            List<GuideReview> reviews= new List<GuideReview>();

            foreach (var guideReview in _guideReviews)
            {

                if (guideReview.Tour.AssignedGuideID == guideID &&  guideReview.TourID == tourID)
                {
                    reviews.Add(guideReview);
                }
            }
            return reviews;
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
                foreach(Tourist tourist in tourists)
                {
                    if(guideReview.TouristID == tourist.ID)
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
