using LabWork10.Contexts;
using LabWork10.DTOs;
using LabWork10.Extensions;
using LabWork10.Filters;
using LabWork10.Model;
using LabWork10.Pagination;
using LabWork10.Sorts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LabWork10.Services
{
    public class FilmService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        private int _firstFilmReleaseYear = 1887;
        public async Task<List<Film>> GetAsync(Paginate pageInfo = null!, 
            FilmFilter filmFilter = null!,
            Sort sort = default!)
        {
            var films = _context.Films.AsQueryable();

            if (sort is not null)
            {
                var property = typeof(Film).GetProperty(sort.ColumnName);
                films = sort.isDescending ? films.OrderByDescending(f => property.GetValue(f)) : films.OrderBy(f => property.GetValue(f));
            }

            if (filmFilter.Name is not null)
                films = films.Where(f => f.Name == filmFilter.Name);
            if (filmFilter.NamePart is not null)
                films = films.Where(f => f.Name.Contains(filmFilter.NamePart));
            if (filmFilter.MinReleaseYear > _firstFilmReleaseYear)
                films = films.Where(f => f.ReleaseYear >= filmFilter.MinReleaseYear);
            if (filmFilter.Date is not null)
                films = films.Where(f => f.RentalFinish < filmFilter.Date);

            if (pageInfo is not null)
                films = films
                        .Skip(pageInfo.PageSize * (pageInfo.CurrentPage - 1))
                        .Take(pageInfo.PageSize);

            return await films.ToListAsync();
        }

        public async Task<List<FilmDto?>> GetDtoAsync()
        {
            return await _context.Films
                    .Include(f => f.Genres)
                    .Select(f => f.ToDto())
                    .ToListAsync();
        }

        public async Task<List<FilmGenreDto?>> GetFilmGenreDtoAsync()
        {
            return await _context.Films
                    .Include(f => f.Genres)
                    .Select(f => f.ToFilmGenreDto())
                    .ToListAsync();
        }

        public async Task<Film> GetByIdAsync(int id)
            => await _context.Films.FindAsync(id) ?? null!;
    }
}
