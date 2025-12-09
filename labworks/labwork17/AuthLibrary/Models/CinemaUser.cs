using System.ComponentModel.DataAnnotations;

namespace AuthLibrary.Models;

public partial class CinemaUser
{
    public int UserId { get; set; }
    [Display(Name = "Логин")]
    public string Login { get; set; } = null!;
    [Display(Name = "Пароль")]
    public string PasswordHash { get; set; } = null!;
    [Display(Name = "Количество ошибок ввода пароля")]
    public int FailTryAuthQuantity { get; set; }
    [Display(Name = "Дата разблокировки")]
    public DateTime? UnlockDate { get; set; }

    [Display(Name = "Роль")]
    public int RoleId { get; set; }

    public virtual CinemaUserRole Role { get; set; } = null!;
}
