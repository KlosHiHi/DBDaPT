using LabWork10.Contexts;
using LabWork10.Model;
using LabWork10.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Services
{
    public class GenreService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Genre>> GetAsync(PageInfo pageInfo = null!, bool isDescending = false)
        {
            var genres = _context.Genres.AsQueryable();

            genres = isDescending ? genres.OrderByDescending(g => g.Name) : genres.OrderBy(g => g.Name);

            return (pageInfo is not null) ?
                await genres
                    .Skip(pageInfo.PageSize * (pageInfo.CurrentPage - 1))
                    .Take(pageInfo.PageSize)
                    .ToListAsync() :
                await genres.ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
            => await _context.Genres.FindAsync(id) ?? null!;
    }
}
