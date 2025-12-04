namespace AuthLibrary.Models;

public partial class CinemaUser
{
    public int UserId { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int FailTryAuthQuantity { get; set; }
    public DateTime? UnlockDate { get; set; }
    public int RoleId { get; set; }

    public virtual CinemaUserRole Role { get; set; } = null!;
}
