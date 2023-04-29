namespace TouristAgency.Users
{
    public class GuideService
    {
        //TODO Logic for guide later...
        private readonly App _app;
        public GuideRepository GuideRepository { get; set; }
        public GuideService()
        {
            _app = (App)System.Windows.Application.Current;
            GuideRepository = _app.GuideRepository;
        }
    }
}
