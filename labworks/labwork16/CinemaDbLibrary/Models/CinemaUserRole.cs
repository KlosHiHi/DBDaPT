using System;
using System.Collections.Generic;

namespace CinemaDbLibrary.Models;

public partial class CinemaUserRole
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;

    public virtual ICollection<CinemaUser> CinemaUsers { get; set; } = new List<CinemaUser>();
    public virtual ICollection<CinemaPrivilege> Privileges { get; set; } = new List<CinemaPrivilege>();
}
