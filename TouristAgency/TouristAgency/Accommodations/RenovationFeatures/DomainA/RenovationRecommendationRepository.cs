using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Interfaces;

namespace TouristAgency.Accommodations.RenovationFeatures.DomainA
{
    public class RenovationRecommendationRepository : ICrud<RenovationRecommendation>, ISubject
    {
        private readonly IStorage<RenovationRecommendation> _storage;
        private readonly List<RenovationRecommendation> _recommendations;
        private List<IObserver> _observers;

        public RenovationRecommendationRepository(IStorage<RenovationRecommendation> storage)
        {
            _storage = storage;
            _recommendations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _recommendations.Count() == 0 ? 0 : _recommendations.Max(g => g.Id) + 1;
        }

        public RenovationRecommendation GetById(int id)
        {
            return _recommendations.Find(g => g.Id == id);
        }

        public RenovationRecommendation Create(RenovationRecommendation newRecommendation)
        {
            newRecommendation.Id = GenerateId();
            _recommendations.Add(newRecommendation);
            _storage.Save(_recommendations);
            NotifyObservers();

            return newRecommendation;
        }

        public RenovationRecommendation Update(RenovationRecommendation updatedRecommendation, int id)
        {
            RenovationRecommendation currentRecommendation = GetById(id);

            if (currentRecommendation == null)
            {
                return null;
            }

            currentRecommendation.Comment = updatedRecommendation.Comment;
            currentRecommendation.UrgencyLevel = updatedRecommendation.UrgencyLevel;

            _storage.Save(_recommendations);
            NotifyObservers();

            return currentRecommendation;
        }

        public void Delete(int id)
        {
            RenovationRecommendation deletedRecommendation = GetById(id);
            _recommendations.Remove(deletedRecommendation);
            _storage.Save(_recommendations);
            NotifyObservers();
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _recommendations;
        }

        public void LoadReservationsToRenovationRecommendation(List<Reservation> reservations)
        {
            foreach (var recommendation in _recommendations)
            {
                Reservation reservation = reservations.Find(r => r.Id == recommendation.ReservationId);
                if (reservation != null)
                {
                    recommendation.Reservation = reservation;
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
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
