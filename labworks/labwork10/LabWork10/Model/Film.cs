using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork10.Model
{
    [Table("Film")]
    public class Film
    {
        public int FilmId { get; set; }
        public string? Name { get; set; }
        public Int16 Duration { get; set; }
        public Int16 ReleaseYear { get; set; }
        public byte[]? Poster { get; set; }
        public string? AgeLimit { get; set; }
        public DateTime? RentalStart { get; set; }
        public DateTime? RentalFinish { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<Genre>? Genres { get; set; } = new List<Genre>();
    }
}
