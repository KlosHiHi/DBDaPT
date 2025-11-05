namespace CinemaClassLibrary.Models;

public partial class Genre
{
    public int GenreId { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
