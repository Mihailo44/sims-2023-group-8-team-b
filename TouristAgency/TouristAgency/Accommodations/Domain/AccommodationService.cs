using System.Collections.Generic;
using System.Linq;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;


namespace TouristAgency.Accommodations.Domain
{
    public class AccommodationService
    {
        private readonly App app = (App)System.Windows.Application.Current;

        public AccommodationRepository AccommodationRepository { get; }

        public AccommodationService()
        {
            AccommodationRepository = app.AccommodationRepository;
        }

        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            names.Add(string.Empty);

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!names.Contains(accommodation.Name) && accommodation.Name != "")
                {
                    names.Add(accommodation.Name);
                }
            }

            return names;
        }

        public List<string> GetCities()
        {
            List<string> citites = new List<string>();
            citites.Add(string.Empty);

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!citites.Contains(accommodation.Location.City) && accommodation.Location.City != "")
                {
                    citites.Add(accommodation.Location.City);
                }
            }

            return citites;
        }

        public List<string> GetCountries()
        {
            List<string> countries = new List<string>();
            countries.Add(string.Empty);

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!countries.Contains(accommodation.Location.Country) && accommodation.Location.Country != "")
                {
                    countries.Add(accommodation.Location.Country);
                }
            }

            return countries;
        }

        public List<string> GetTypes()
        {
            List<string> types = new List<string>();
            types.Add(string.Empty);

            List<Accommodation> _accommodations = AccommodationRepository.GetAll();

            foreach (Accommodation accommodation in _accommodations)
            {
                if (!types.Contains(accommodation.Type.ToString()) && accommodation.Type.ToString() != "")
                {
                    types.Add(accommodation.Type.ToString());
                }
            }

            return types;
        }

        public List<Accommodation> GetByOwnerId(int id)
        {
            return AccommodationRepository.GetAll().FindAll(a => a.OwnerId == id);
        }

        public List<Accommodation> Search(string country, string city, string name, string type, int maxGuest, int minDays)
        {
            return AccommodationRepository.GetAll().Where(a => a.Location.Country.Contains(country) && a.Location.City.Contains(city) && a.Name.Contains(name) && a.Type.ToString().Contains(type) && a.MaxGuestNum >= maxGuest && a.MinNumOfDays <= minDays).OrderByDescending(a => a.Owner.SuperOwner).ToList();
        }

        public List<int> GetAccommodationStatsByYear(ReservationService reservationService, PostponementRequestService postponementRequestService,RenovationRecommendationService renovationRecommendationService, Accommodation accommodation, int year)
        {
            List<int> results = new List<int>();
            int reservations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.IsCanceled == false).Count();
            int cancelations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.IsCanceled == true).Count();
            int postponations = postponementRequestService.PostponementRequestRepository.GetAll().FindAll(p => p.Reservation.Start.Year == year && p.Reservation.AccommodationId == accommodation.Id).Count();
            int reccommendations = renovationRecommendationService.RenovationRecommendationRepository.GetAll().FindAll(r => r.Reservation.AccommodationId == accommodation.Id && r.Reservation.Start.Year == year && r.Reservation.IsCanceled == false).Count();
            var monthGroups = reservationService.GetByAccommodationId(accommodation.Id).FindAll(r => r.Start.Year == year && r.IsCanceled == false).GroupBy(r => r.Start.Month);
            int maxNumOfRes = 0;
            int busiestMonth = 0;

            foreach (var month in monthGroups)
            {
                if (maxNumOfRes <= month.Count())
                {
                    maxNumOfRes = month.Count();
                    busiestMonth = month.Key;
                }
            }

            results.Add(reservations);
            results.Add(cancelations);
            results.Add(postponations);
            results.Add(reccommendations);
            results.Add(busiestMonth);

            return results;
        }

        public List<int> GetAccommodationStatsByMonth(ReservationService reservationService, PostponementRequestService postponementRequestService, RenovationRecommendationService renovationRecommendationService, Accommodation accommodation, int year,int monthNumber)
        {
            List<int> results = new List<int>();

            int reservations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.Start.Month == monthNumber && r.IsCanceled == false).Count();
            int cancelations = reservationService.GetByAccommodationId(accommodation.Id).Where(r => r.Start.Year == year && r.Start.Month == monthNumber && r.IsCanceled == true).Count();
            int postponements = postponementRequestService.PostponementRequestRepository.GetAll().FindAll(p => p.Reservation.Start.Year == year && p.Reservation.Start.Month == monthNumber && p.Reservation.AccommodationId == accommodation.Id).Count();
            int reccommendations = renovationRecommendationService.RenovationRecommendationRepository.GetAll().FindAll(r => r.Reservation.AccommodationId == accommodation.Id && r.Reservation.IsCanceled == false && r.Reservation.Start.Year == year && r.Reservation.Start.Month == monthNumber).Count();

            results.Add(reservations);
            results.Add(cancelations);
            results.Add(postponements);
            results.Add(reccommendations);

            return results;
        }
    }
}
