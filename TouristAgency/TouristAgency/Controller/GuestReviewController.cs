using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model.DAO;
using TouristAgency.Model;
using TouristAgency.Interfaces;

namespace TouristAgency.Controller
{
    public class GuestReviewController
    {
        private readonly GuestReviewDAO _guestReview;

        public GuestReviewController()
        {
            _guestReview = new GuestReviewDAO();
        }

        public List<GuestReview> GetAll()
        {
           return _guestReview.GetAll();
        }

        public void Create(GuestReview newGuestReview)
        {
            _guestReview.Create(newGuestReview);
        }

        public void Update(GuestReview updatedGuestReview,int id)
        {
            _guestReview.Update(updatedGuestReview, id);
        }

        public void Delete(GuestReview guestReview)
        {
            _guestReview.Delete(guestReview.Id);
        }

        public void Subsribe(IObserver observer)
        {
            _guestReview.Subscribe(observer);
        }
    }
}
