using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using CinemaDbLibrary.Options;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Dynamic.Core;

namespace CinemaDbLibrary.Service
{
    public class FilmService(CinemaDbContext context)
    {
        private readonly CinemaDbContext _context = context;

        public async Task<Film> GetByIdAsync(int id)
            => await _context.Films.FirstOrDefaultAsync(f => f.FilmId == id) ?? null!;

        public async Task<IEnumerable<Film>> GetAsync(
            PaginationOptions pageOptions = null!,
            SortOptions sortOptions = null!)
        {
            var films = _context.Films.AsQueryable();

            if (sortOptions is not null)
            {
                try
                {
                    films = films
                        .OrderBy($"{sortOptions.ColumnName} {(sortOptions.IsDescending ? "desc" : "")}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            if (pageOptions is not null)
                films = films
                    .Skip(pageOptions.PageSize * (pageOptions.CurrentPage - 1))
                    .Take(pageOptions.PageSize);

            return await films.ToListAsync();
        }
    }
}
