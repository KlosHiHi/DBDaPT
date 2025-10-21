using LabWork12.Contexts;
using LabWork12.Models;
using LabWork12.Sorts;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class TicketService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<List<Ticket>> GetAllOrderedAsync(Sort sort)
            => sort.isDescending
                ? await _context.Tickets.FromSqlRaw($"select * from ticket order by {sort.ColumnName} desc").ToListAsync()
                : await _context.Tickets.FromSqlRaw($"select * from ticket order by {sort.ColumnName}").ToListAsync();

        public async Task<DateTime> GetSessionsDateByTicketIdAsync(int id)
            => await _context.Database
            .SqlQuery<DateTime>($@"select startdate as value from ticket
join session on ticket.sessionid = session.sessionid
where ticket.ticketid = {id}")
            .FirstOrDefaultAsync();

        public async Task<List<Ticket>> GetTicketByPhone(string phone)
            => await _context.Tickets
                    .FromSqlRaw($"EXEC GetVisitorTickets @p0", phone)
                    .ToListAsync();
    }
}
