using System;
using System.Collections.Generic;
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
            while((reservationService.IsReserved(accommodation.Id, startInterval.AddDays(i), endInterval.AddDays(i)) == false) && (startInterval < end))
            {
                renovations.Add(new Renovation(accommodation, startInterval.AddDays(i), endInterval.AddDays(i), estimatedDuration));
                startInterval = startInterval.AddDays(i);
                i++;
            }

            return renovations;
        }
    }
}
