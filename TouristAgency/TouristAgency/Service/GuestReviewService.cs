using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Repository;

namespace TouristAgency.Service
{
    public class GuestReviewService
    {
        private readonly App app = (App)App.Current;
        public GuestReviewRepository GuestReviewRepository { get; }

        public GuestReviewService()
        {
            GuestReviewRepository = app.GuestReviewRepository;
        }
    }
}
