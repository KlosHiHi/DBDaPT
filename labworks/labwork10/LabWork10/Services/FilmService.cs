using LabWork10.Contexts;
using LabWork10.Filters;
using LabWork10.Model;
using LabWork10.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Services
{
    public class FilmService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Film>> GetAsync(FilmFilter filter, bool isDescending = false)
        {
            var films = _context.Films.AsQueryable();

            if (isDescending)
                films = films.OrderByDescending(f => f.Name);
            else
                films = films.OrderBy(f => f.Name);

            if(filter.Name is not null)
                films = films.Where(f => f.Name == filter.Name);
            if (filter.NamePart is not null)
                films = films.Where(f => f.Name.Contains(filter.NamePart));

            return await _context.Films
                    .Skip(pageSize * (currentPage - 1))
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<Film> GetByIdAsync(int id)
            => await _context.Films.FindAsync(id) ?? null!;
    }
}
