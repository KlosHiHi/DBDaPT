using LabWork10.Contexts;
using LabWork10.Model;
using LabWork10.Pagination;
using LabWork10.Sorts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace LabWork10.Services
{
    public class TicketService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetAsync(Paginate pageInfo = null!, Sort sort = null!)
        {
            var tickets = _context.Tickets.AsQueryable();

            if (sort is not null)
                tickets = tickets.OrderBy($"{sort.ColumnName} {(sort.isDescending ? "desc" : "")}");

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
