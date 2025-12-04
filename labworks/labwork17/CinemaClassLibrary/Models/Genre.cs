using System.Text.Json.Serialization;

namespace CinemaClassLibrary.Models;

public partial class Genre
{
    public int GenreId { get; set; }
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
