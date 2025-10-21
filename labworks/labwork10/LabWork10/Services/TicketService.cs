using LabWork10.Contexts;
using LabWork10.Model;
using LabWork10.Pagination;
using LabWork10.Sorts;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Services
{
    public class TicketService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetAsync(Sort sort = null!, Paginate pageInfo = null!)
        {
            var tickets = _context.Tickets.AsQueryable();

            if(sort is not null)
            {
                var property = typeof(Ticket).GetProperty(sort.ColumnName);
                tickets = sort.isDescending ? tickets.OrderByDescending(t => property.GetValue(t)) : tickets.OrderBy(t => property.GetValue(t));
            }

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
