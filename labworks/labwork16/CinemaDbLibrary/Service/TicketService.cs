using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using CinemaDbLibrary.Options;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Dynamic.Core;

namespace CinemaDbLibrary.Service
{
    public class TicketService(CinemaDbContext context)
    {
        private readonly CinemaDbContext _context = context;

        public async Task<Ticket> GetByIdAsync(int id)
            => await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == id) ?? null!;

        public async Task<IEnumerable<Ticket>> GetAsync(
            PaginationOptions pageOptions = null!,
            SortOptions sortOptions = null!)
        {
            var tickets = _context.Tickets.AsQueryable();

            if (sortOptions is not null)
            {
                try
                {
                    tickets = tickets
                        .OrderBy($"{sortOptions.ColumnName} {(sortOptions.IsDescending ? "desc" : "")}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            if (pageOptions is not null)
                tickets = tickets
                    .Skip(pageOptions.PageSize * (pageOptions.CurrentPage - 1))
                    .Take(pageOptions.PageSize);

            return await tickets.ToListAsync();
        }
    }
}
