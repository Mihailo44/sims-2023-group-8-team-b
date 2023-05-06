using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TouristAgency.Review.Domain
{
    public class GuestReviewService
    {
        private readonly App app = (App)System.Windows.Application.Current;
        public GuestReviewRepository GuestReviewRepository { get; }

        public GuestReviewService()
        {
            GuestReviewRepository = app.GuestReviewRepository;
        }
    }
}
