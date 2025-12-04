using System.Text.Json.Serialization;

namespace CinemaClassLibrary.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }
    public string Phone { get; set; } = null!;
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }

    [JsonIgnore]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
