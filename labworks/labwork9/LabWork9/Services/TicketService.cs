using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services
{
    public class TicketService(AppDbContext context) : IService<Ticket>
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetAsync()
            => await _context.Tickets.Include(t => t.Visitor).ToListAsync();

        public async Task AddAsync(Ticket entity)
        {
            await _context.Tickets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket is not null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Ticket entity)
        {
            //var ticket = await _context.Tickets.FindAsync(entity.TicketId);

            //if (ticket is null)
            //    throw new ArgumentException("ticket isn't found");

            //ticket.Row = entity.Row;
            //ticket.Seat = entity.Seat;
            //ticket.SessionId = entity.SessionId;
            //ticket.VisitorId = entity.VisitorId;

            _context.Tickets.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
