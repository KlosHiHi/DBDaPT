using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CinemaClassLibrary.Models;

public partial class Session
{
    [Display(Name = "Идентификатор")]
    public int SessionId { get; set; }
    [Display(Name = "Фильм")]
    public int FilmId { get; set; }
    [Display(Name = "Зал")]
    public byte HallId { get; set; }
    [DataType(DataType.Currency)]
    [Range(0.01, 10000.00, ErrorMessage = "Цена должна быть от {1} до {2}")]
    [Display(Name = "Цена")]
    public decimal? Price { get; set; }
    [Display(Name = "Дата начала проката")]
    [DataType(DataType.DateTime)]
    public DateTime? StartDate { get; set; }
    [Display(Name = "Фильм в 3D")]
    public bool? IsFilm3d { get; set; }

    [Display(Name = "Фильм")]
    public virtual Film? Film { get; set; }
    [Display(Name = "Зал")]
    public virtual CinemaHall? Hall { get; set; }

    [Display(Name = "Билеты")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
