using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork10.Model
{
    [Table("Ticket")]
    public class Ticket
    {
        public int TicketId { get; set; }
        public int SessionId { get; set; }
        public int VisitorId { get; set; }
        public byte Row { get; set; }
        public byte Seat { get; set; }

        public Visitor? Visitor { get; set; }
    }
}
