using TouristAgency.Users;

namespace TouristAgency.TourRequests
{
    internal class TourRequestViewModel
    {
        private Tourist _loggedInTourist;

        public TourRequestViewModel(Tourist tourist)
        {
            _loggedInTourist = tourist;
        }
    }
}