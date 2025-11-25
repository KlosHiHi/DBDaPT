using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Services
{
    public class TicketService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<Ticket?> GetTicketByIdAsync(int id)
            => await _context.Tickets.FindAsync(id);

        public async Task<string> GetFilmNameByTicketIdAsync(int id)
            => await _context.Database
            .SqlQuery<string>($@"
SELECT Film.Name
FROM Session INNER JOIN
	Ticket ON Session.SessionId = Ticket.SessionId INNER JOIN
	Film ON Session.FilmId = Film.FilmId
WHERE Ticket.TicketId = {id}")
            .FirstOrDefaultAsync() ?? "";

        public async Task<DateTime> GetSessionStartTimeByTicketId(int id)
            => await _context.Database
                .SqlQuery<DateTime>($@"
SELECT Session.StartDate
FROM Session INNER JOIN
	Ticket ON Session.SessionId = Ticket.SessionId
WHERE Ticket.TicketId = {id}")
                .FirstOrDefaultAsync();

        public async Task<string> GetCinemaNameByTicketIdAsync(int id)
            => await _context.Database
            .SqlQuery<string>($@"
SELECT CinemaHall.CinemaName
FROM Session INNER JOIN
	Ticket ON Session.SessionId = Ticket.SessionId INNER JOIN
	CinemaHall ON Session.HallId = CinemaHall.HallId
WHERE Ticket.TicketId = {id}")
            .FirstOrDefaultAsync() ?? "";

        public async Task<int> GetHallNumByTicketIdAsync(int id)
            => await _context.Database
            .SqlQuery<int>($@"
SELECT CinemaHall.HallNumber
FROM Session INNER JOIN
	Ticket ON Session.SessionId = Ticket.SessionId INNER JOIN
	CinemaHall ON Session.HallId = CinemaHall.HallId
WHERE Ticket.TicketId = {id}")
            .FirstOrDefaultAsync();
    }
}
