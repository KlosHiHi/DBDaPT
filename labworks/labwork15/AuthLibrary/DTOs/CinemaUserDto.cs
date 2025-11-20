namespace AuthLibrary.DTOs
{
    public class CinemaUserDto
    {
        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
