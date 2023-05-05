using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Model.Enums;
using TouristAgency.Repository;

namespace TouristAgency.Service
{
    public class PostponementRequestService 
    {
        private readonly App app = (App)App.Current;
        public PostponementRequestRepository PostponementRequestRepository { get;}

        public PostponementRequestService()
        {
            PostponementRequestRepository = app.PostponementRequestRepository;
        }

        public List<PostponementRequest> GetByOwnerId(int ownerId)
        {
            return PostponementRequestRepository.GetAll().FindAll(r => r.Reservation.Accommodation.OwnerId == ownerId && r.Status == PostponementRequestStatus.PENDING);
        }

        public List<PostponementRequest> GetByGuestId(int guestId)
        {
            return PostponementRequestRepository.GetAll().FindAll(r => r.Reservation.GuestId == guestId);
        }

        public String ShowNotifications(int id)
        {
            string result = "No new notifications";
            foreach (PostponementRequest request in PostponementRequestRepository.GetAll())
            {
                if (request.Seen == false && request.Status != PostponementRequestStatus.PENDING &&
                    request.Reservation.GuestId == id)
                {
                    result = "One of your requests has changed";
                    request.Seen = true;
                }
                
            }
            return result;
        }
    }
}
