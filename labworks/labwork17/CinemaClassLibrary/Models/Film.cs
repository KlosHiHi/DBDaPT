using System.Text.Json.Serialization;

namespace CinemaClassLibrary.Models;

public partial class Film
{
    public int FilmId { get; set; }
    public string Name { get; set; } = null!;
    public short Duration { get; set; }
    public short ReleaseYear { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public byte[]? Poster { get; set; }
    public string? AgeLimit { get; set; }
    public DateOnly? RentalStart { get; set; }
    public DateOnly? RentalFinish { get; set; }
    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    [JsonIgnore]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
