using LabWork10.Contexts;
using LabWork10.Model;
using LabWork10.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Services
{
    public class TicketService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetAsync(PageInfo pageInfo = null!, bool isDescending = false)
        {
            var tickets = _context.Tickets.AsQueryable();

            tickets = isDescending ? tickets.OrderByDescending(t => t.SessionId) : tickets.OrderBy(t => t.SessionId);

            return (pageInfo is not null) ?
                await tickets
                    .Skip(pageInfo.PageSize * (pageInfo.CurrentPage - 1))
                    .Take(pageInfo.PageSize)
                    .ToListAsync() :
                await tickets.ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
            => await _context.Tickets.FindAsync(id) ?? null!;
    }
}
