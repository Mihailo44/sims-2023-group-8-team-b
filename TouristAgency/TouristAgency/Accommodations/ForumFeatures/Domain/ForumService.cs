using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
