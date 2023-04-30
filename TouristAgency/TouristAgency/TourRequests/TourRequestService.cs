namespace TouristAgency.TourRequests
{
    public class TourRequestService
    {
        private readonly App _app;
        public TourRequestRepository TourRequestRepository { get; set; }

        public TourRequestService()
        {
            _app = (App)System.Windows.Application.Current;
            TourRequestRepository = _app.TourRequestRepository;
        }
    }
}
