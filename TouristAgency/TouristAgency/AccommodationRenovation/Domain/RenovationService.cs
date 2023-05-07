using System;
using System.Collections.Generic;
using System.Linq;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Reservations.Domain;

namespace TouristAgency.AccommodationRenovation.Domain
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

        public List<Renovation> GeneratePotentionalRenovations(DateTime start, DateTime end, int estimatedDuration, Accommodation accommodation, ReservationService reservationService)
        {
            List<Renovation> renovations = new List<Renovation>();
            DateTime startInterval = start;
            DateTime endInterval = start.AddDays(estimatedDuration);

            int i = 0;
            while(!(reservationService.IsReserved(accommodation.Id, startInterval, endInterval)) && (startInterval.AddDays(estimatedDuration) < end))
            {
                renovations.Add(new Renovation(accommodation, startInterval, endInterval, estimatedDuration));
                startInterval = startInterval.AddDays(1);
                endInterval = startInterval.AddDays(estimatedDuration);
                i++;
            }

            return renovations;
        }

        public void SetRenovationStatus(List<Accommodation> ownersAccommodations,AccommodationService accommodationService)
        {
            DateTime today = DateTime.Today;

            foreach(var accommodation in ownersAccommodations)
            {
                List<Renovation> renovations = RenovationRepository.GetAll().Where(r => r.AccommodationId == accommodation.Id).OrderByDescending(r => r.End).ToList();
                if (renovations != null && renovations.Count > 0)
                {
                    Renovation latestRenovation = renovations[0];

                    if(today > latestRenovation.End)
                    {
                        accommodation.RecentlyRenovated = true;
                        accommodationService.AccommodationRepository.Update(accommodation, accommodation.Id);
                    }

                    if ((today - latestRenovation.End).TotalDays > 365)
                    {
                        accommodation.RecentlyRenovated = false;
                        accommodationService.AccommodationRepository.Update(accommodation, accommodation.Id);
                    }
                }
            }       
        }
    }
}
