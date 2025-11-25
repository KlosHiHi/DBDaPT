namespace CinemaDbLibrary.DTOs
{
    public class TicketDto
    {
        public int TicketId { get; set; }
        public string FilmName { get; set; } = null!;
        public DateTime? SessionStartDate { get; set; }
        public string CinemaName { get; set; } = null!;
        public byte HallNumber { get; set; }
        public byte Row { get; set; }
        public byte Seat { get; set; }
    }
}
