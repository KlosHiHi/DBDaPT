using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.DTOs;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services
{
    public class TicketService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<Ticket?> GetTicketByIdAsync(int id)
            => await _context.Tickets.FindAsync(id);

        public async Task<TicketDto?> GetTicketDtoByIdAsync(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Session)
                    .ThenInclude(s => s.Hall)
                .Include(t => t.Session)
                    .ThenInclude(s => s.Film)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            TicketDto result = new()
            {
                TicketId = ticket.TicketId,
                FilmName = ticket.Session.Film.Name,
                CinemaName = ticket.Session.Hall.CinemaName,
                HallNumber = ticket.Session.Hall.HallNumber,
                SessionStartDate = ticket.Session.StartDate,
                Row = ticket.Row,
                Seat = ticket.Seat
            };

            return result;
        }
    }
}
