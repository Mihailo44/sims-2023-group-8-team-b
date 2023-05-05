namespace TouristAgency.Users
{
    public class UserService
    {
        private readonly App app = (App)System.Windows.Application.Current;
        public UserRepository UserRepository { get; }

        public UserService()
        {
            UserRepository = app.UserRepository;
        }
    }
}
