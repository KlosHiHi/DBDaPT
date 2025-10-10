using LabWork10.Contexts;
using LabWork10.Model;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Services
{
    public class TicketService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetAsync(bool isDescending = false)
        {
            if (isDescending)
                return await _context.Tickets
                        .OrderByDescending(t => t.SessionId)
                        .Skip(pageSize * (currentPage - 1))
                        .Take(pageSize)
                        .ToListAsync();
            
            return await _context.Tickets
                        .OrderBy(t => t.SessionId)
                        .Skip(pageSize * (currentPage - 1))
                        .Take(pageSize)
                        .ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
            => await _context.Tickets.FindAsync(id) ?? null!;
    }
}
