﻿using LabWork12.Contexts;
using LabWork12.Models;
using LabWork12.Sorts;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class TicketService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<List<Ticket>> GetAllOrderedAsync(Sort sort)
            => await _context.Tickets
                .FromSqlRaw($"select * from ticket order by {sort.ColumnName} {(sort.isDescending ? "desc" : "")}")
                .ToListAsync();

        public async Task<DateTime> GetSessionDateByTicketIdAsync(int id)
            => await _context.Database
                .SqlQuery<DateTime>($@"select startDate as value 
from ticket
join session on ticket.sessionId = session.sessionId
where ticket.ticketId = {id}")
                .FirstOrDefaultAsync();

        public async Task<List<Ticket>> GetTicketByPhone(string phone)
            => await _context.Tickets
                .FromSql($"dbo.GetVisitorTickets {phone}")
                .ToListAsync();
    }
}
