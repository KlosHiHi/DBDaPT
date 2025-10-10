using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork10.Model
{
    [Table("Genre")]
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = null!;

        public IEnumerable<Film>? Films { get; set; }
    }
}
