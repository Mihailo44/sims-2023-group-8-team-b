using System;
using System.Collections.Generic;
using System.Linq;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;

namespace TouristAgency.Accommodations.RenovationFeatures.DomainA
{
    public class RenovationService
    {
        private readonly App _app;
        public RenovationRepository RenovationRepository { get; }

        public RenovationService()
        {
            _app = (App)App.Current;
            RenovationRepository = _app.RenovationRepository;
        }

        public Renovation Create(Renovation newRenovation)
        {
            return RenovationRepository.Create(newRenovation);
        }

        public List<Renovation> GeneratePotentialRenovations(DateTime start, DateTime end, int estimatedDuration, Accommodation accommodation, ReservationService reservationService)
        {
            List<Renovation> renovations = new List<Renovation>();
            DateTime startInterval = start;
            DateTime endInterval = start.AddDays(estimatedDuration);

            bool isReserved = reservationService.IsReserved(accommodation.Id, startInterval, endInterval);
            
            while(startInterval.AddDays(estimatedDuration) <= end)
            {
                if (!isReserved)
                {
                    renovations.Add(new Renovation(accommodation, startInterval, endInterval, estimatedDuration));
                }
                startInterval = startInterval.AddDays(1);
                endInterval = startInterval.AddDays(estimatedDuration);
                
                isReserved = reservationService.IsReserved(accommodation.Id, startInterval, endInterval);
            }

            return renovations;
        }

        public void CheckAccommodationRenovationStatus(AccommodationService accommodationService)
        {
            DateTime today = DateTime.Today;
            List<Accommodation> ownersAccommodations = accommodationService.GetByOwnerId(_app.LoggedUser.ID);

            foreach (var accommodation in ownersAccommodations)
            {
                List<Renovation> renovations = RenovationRepository.GetAll().Where(r => r.AccommodationId == accommodation.Id && r.IsCanceled == false && r.End <= today).OrderByDescending(r => r.End).ToList();
                if (renovations != null && renovations.Count() > 0)
                {
                    Renovation latestRenovation = renovations[0];
                    if ((today - latestRenovation.End).TotalDays > 365)
                    {
                        accommodation.RecentlyRenovated = false;
                        accommodationService.AccommodationRepository.Update(accommodation, accommodation.Id);
                    }
                }
            }       
        }

        public void CancelRenovation(Renovation renovation)
        {
            renovation.IsCanceled = true;
            RenovationRepository.Update(renovation, renovation.Id);
        }

        public void SetRenovationProgress(AccommodationService accommodationService)
        {
            DateTime today = DateTime.Today;

            foreach(Renovation renovation in GetRenovationsByOwnerId(_app.LoggedUser.ID))
            {
                if (renovation.IsCanceled)
                {
                    continue;
                }
                else
                {
                    if (today > renovation.End)
                    {
                        Accommodation renovatedAccommodation = accommodationService.AccommodationRepository.GetById(renovation.AccommodationId);
                        if ((renovatedAccommodation != null) && (renovatedAccommodation.CurrentlyRenovating == true))
                        {
                            renovatedAccommodation.CurrentlyRenovating = false;//ne moze ovako
                            renovatedAccommodation.RecentlyRenovated = true;
                            accommodationService.AccommodationRepository.Update(renovatedAccommodation, renovatedAccommodation.Id);
                        }
                    }
                    if (today == renovation.Start)
                    {
                        Accommodation renovatedAccommodation = accommodationService.AccommodationRepository.GetById(renovation.AccommodationId);
                        if (renovatedAccommodation != null)
                        {
                            renovatedAccommodation.CurrentlyRenovating = true;//ne moze ovako
                            accommodationService.AccommodationRepository.Update(renovatedAccommodation, renovatedAccommodation.Id);
                        }
                    }
                }
            }
        }

        public List<Renovation> GetRenovationsByAccommodationId(int accommodationId) // da li da ucitavam i otkazane
        {
            List<Renovation> renovations = new();

            renovations = RenovationRepository.GetAll().FindAll(r => r.AccommodationId == accommodationId);
            return renovations;
        }

        public List<Renovation> GetRenovationsByOwnerId(int ownerId) // da li da ucitavam i otkazane
        {
            List<Renovation> renovations = new();

            renovations = RenovationRepository.GetAll().FindAll(r => r.Accommodation.OwnerId == ownerId);
            return renovations;
        }
    }
}
