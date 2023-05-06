using System;
using TouristAgency.Tours;

namespace TouristAgency.Users
{
    public class GuideService
    {
        //TODO Logic for guide later...
        private readonly App _app;
        public GuideRepository GuideRepository { get; set; }
        public GuideService()
        {
            _app = (App)System.Windows.Application.Current;
            GuideRepository = _app.GuideRepository;
        }

        public Tour IsGuideOccupied(Guide guide, DateTime startDate, DateTime endDate)
        {
            foreach(Tour tour in guide.AssignedTours)
            {
                //TODO: Kada budu kompleksne ture, ovaj deo se mora razraditi
                if(startDate <= tour.StartDateTime && endDate >= tour.StartDateTime)
                {
                    return tour;
                }
            }
            return null;
        }
    }
}
