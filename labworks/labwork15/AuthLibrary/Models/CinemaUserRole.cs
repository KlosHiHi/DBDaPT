namespace AuthLibrary.Models;

public partial class CinemaUserRole
{
    public int RoleId { get; set; }
    public string? RoleName { get; set; }

    public virtual ICollection<CinemaUser> CinemaUsers { get; set; } = new List<CinemaUser>();

    public virtual ICollection<CinemaPrivilege> Privileges { get; set; } = new List<CinemaPrivilege>();
}
