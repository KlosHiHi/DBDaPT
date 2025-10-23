using LabWork11.Context;
using LabWork11.Model;
using LabWork11.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LabWork11.Services
{
    public class FilmService(Ispp3101Context context)
    {
        private readonly Ispp3101Context _context = context;

        public async Task<List<Film>> GetAsync(PageInfo pageInfo = null!, bool isDescending = false)
        {
            var films = _context.Films.AsQueryable();

            films = isDescending ? films.OrderByDescending(f => f.Name) : films.OrderBy(f => f.Name);

            if (pageInfo is not null)
                films = films
                        .Skip(pageInfo.PageSize * (pageInfo.CurrentPage - 1))
                        .Take(pageInfo.PageSize);

            return await films.ToListAsync();
        }

        public async Task<List<string>> GetAgeLimitsAsync()
            => await _context.Films
                .GroupBy(f => f.AgeLimit ?? "")
                .Select(f => f.Key)
                .ToListAsync();

        public async Task RemoveAsync(int id)
        {
            var film = await GetByIdAsync(id);

            if (film is not null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(List<Film> films)
        {
            if (films is not null)
            {
                _context.Films.RemoveRange(films);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Film> GetByIdAsync(int id)
            => await _context.Films.FindAsync(id) ?? null!;
    }
}
