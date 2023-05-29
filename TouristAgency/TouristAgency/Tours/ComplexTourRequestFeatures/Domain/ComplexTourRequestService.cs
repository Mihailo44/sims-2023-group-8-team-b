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
