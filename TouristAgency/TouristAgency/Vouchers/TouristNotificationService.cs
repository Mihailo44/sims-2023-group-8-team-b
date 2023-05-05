using System.Collections.Generic;

namespace TouristAgency.Vouchers
{
    public class TouristNotificationService
    {
        private readonly App _app;
        public TouristNotificationRepository TouristNotificationRepository { get; }

        public TouristNotificationService()
        {
            _app = (App)System.Windows.Application.Current;
            TouristNotificationRepository = _app.TouristNotificationRepository;
        }

        public List<TouristNotification> GetByTouristID(int TouristID)
        {
            return TouristNotificationRepository.GetAll().FindAll(t => t.TouristID == TouristID);
        }
    }
}
