using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Users.SuperGuestFeature.Domain;

namespace TouristAgency.Accommodations.ForumFeatures.Domain
{
    public class ForumService
    {
        private readonly App app = (App)System.Windows.Application.Current;
        public ForumRepository ForumRepository { get; }

        public ForumService()
        {
            ForumRepository = app.ForumRepository;
        }

        public List<Forum> GetAll()
        {
            return ForumRepository.GetAll();
        }

        public List<Forum> GetByLocation(List<Accommodation> ownersAccommodations)
        {
            List<Forum> result = new List<Forum>();

            foreach (var accommodation in ownersAccommodations)
            {
                result.AddRange(GetAll().FindAll(f => f.Location.Equals(accommodation.Location)));
            }

            return result;
        }
    }
}
