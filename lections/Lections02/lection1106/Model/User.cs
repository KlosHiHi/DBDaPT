namespace lection1106.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        // блокировка
        public int FailedLoginAttemps { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }
        public DateTime? LastAccess { get; set; }
    }
}
