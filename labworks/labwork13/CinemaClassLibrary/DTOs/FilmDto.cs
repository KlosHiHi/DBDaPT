using CinemaClassLibrary.Models;

namespace CinemaClassLibrary.DTOs
{
    public class FilmDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal? SalesProfit { get; set; }
        public int? TiketsCount { get; set; }
    }

    public static class FilmExtension
    {
        public static FilmDto TomDto(this Film film)
            => new()
            {
                Id = film.FilmId,
                Title = film.Name
            };
    }
}
