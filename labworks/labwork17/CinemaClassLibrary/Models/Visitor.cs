using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CinemaClassLibrary.Models;

public partial class Visitor
{
    [Display(Name = "Идентификатор")]    
    public int VisitorId { get; set; }
    [Display(Name = "Телефон")]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона")]
    public string Phone { get; set; } = null!;
    [Display(Name = "Имя")]
    [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ\s]+$", ErrorMessage = "Только буквы и пробелы")]
    public string? Name { get; set; }
    [Display(Name = "Дата рождения")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }
    [Display(Name = "Электронная почта")]
    [EmailAddress(ErrorMessage = "Некорректный email адрес")]
    public string? Email { get; set; }

    [Display(Name = "Билеты")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
