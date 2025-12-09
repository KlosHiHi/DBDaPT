using System.Text.Json.Serialization;

namespace CinemaClassLibrary.Models;

public partial class CinemaHall
{
    public byte HallId { get; set; }
    public string CinemaName { get; set; } = null!;
    public byte HallNumber { get; set; }
    public byte RowsCount { get; set; }
    public byte SeatsCount { get; set; }
    public bool IsVip { get; set; }

    public string? NumberName => $"{CinemaName}, зал №{HallNumber}";

    [JsonIgnore]
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
