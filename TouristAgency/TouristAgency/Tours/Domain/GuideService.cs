using System;
using System.Collections.Generic;
using System.Windows.Documents;
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

        public Guide Create(Guide guide)
        {
            return GuideRepository.Create(guide);
        }

        public List<Guide> GetAll()
        {
            return GuideRepository.GetAll();
        }

        public Guide Update(Guide guide, int id)
        {
            return GuideRepository.Update(guide, id);
        }

        public void Delete(int id)
        {
            GuideRepository.Delete(id);
        }
    }
}
