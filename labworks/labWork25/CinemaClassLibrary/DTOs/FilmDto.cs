namespace CinemaClassLibrary.DTOs
{
    public class FilmDto
    {
        public string Name { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public string CinemaName { get; set; } = null!;
        public byte HallNumber { get; set; }
        public decimal? Price { get; set; }
    }
}
