namespace LabWork10.DTOs
{
    public class FilmGenreDto
    {
        public int FilmId { get; set; }
        public string FilmName { get; set; } = null!;

        public List<string> Genres { get; set; } = null!;
    }
}
