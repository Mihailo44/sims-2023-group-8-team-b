using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.TourRequests;
using TouristAgency.Tours.ComplexTourRequestFeatures.Domain;

namespace TouristAgency.Tours.TourRequestFeatures.Domain
{
    public class ComplexTourRequestService
    {
        private readonly App _app;
        public ComplexTourRequestRepository ComplexTourRequestRepository { get; set; }

        public ComplexTourRequestService()
        {
            _app = (App)System.Windows.Application.Current;
            ComplexTourRequestRepository = _app.ComplexTourRequestRepository;
        }

        public List<ComplexTourRequest> GetAll()
        {
            return ComplexTourRequestRepository.GetAll();
        }

        public void InvalidateOldTourRequests()
        {
            foreach(ComplexTourRequest crequest in GetAll())
            {
                foreach(TourRequest request in crequest.Parts)
                {
                    if(request.Status == Util.TourRequestStatus.INVALID)
                    {
                        crequest.Status = Util.ComplexTourRequestStatus.INVALID;
                        Update(crequest, crequest.ID);
                        continue;
                    }
                }
            }
        }

        public List<ComplexTourRequest> GetPending()
        {
            return ComplexTourRequestRepository.GetAll().FindAll(t => t.Status == Util.ComplexTourRequestStatus.PENDING);
        }

        public List<ComplexTourRequest> GetByTouristID(int touristID)
        {
            return ComplexTourRequestRepository.GetAll().FindAll(t => t.TouristID == touristID);
        }

        public ComplexTourRequest Create(ComplexTourRequest newComplexTourRequest)
        {
            return ComplexTourRequestRepository.Create(newComplexTourRequest);
        }

        public ComplexTourRequest Update(ComplexTourRequest newComplexTourRequest, int ID)
        {
            return ComplexTourRequestRepository.Update(newComplexTourRequest, ID);
        }

        public void Delete(int complexTourRequestID)
        {
            ComplexTourRequestRepository.Delete(complexTourRequestID);
        }
    }
}
