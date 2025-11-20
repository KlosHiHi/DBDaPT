using AuthLibrary.Models;

namespace ClientApp.Class
{
    public class UserSession
    {
        public CinemaUser CurrentUser { get; private set; }
        private static readonly UserSession _instance = new();
        private UserSession() { }
        public static UserSession Instance => _instance;

        public void SetCurrentUser(CinemaUser user)
        {
            CurrentUser = user;
        }

        public void Clear()
        {
            CurrentUser = null!;
        }
    }
}
