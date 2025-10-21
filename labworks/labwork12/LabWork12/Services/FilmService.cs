using LabWork12.Contexts;
using LabWork12.Models;
using LabWork12.Sorts;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class FilmService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<List<Film>> GetAllOrderedAsync(Sort sort)
            => sort.isDescending
                ? await _context.Films
                        .FromSqlRaw($"select * from film order by {sort.ColumnName} desc").ToListAsync()
                : await _context.Films
                        .FromSqlRaw($"select * from film order by {sort.ColumnName}").ToListAsync();

        public async Task<List<Film>> GetByNameAndReleaseYearAsync(string name, int minYear)
            => await _context.Films
                    .FromSql($"select * from film where name = {name} and releaseyear >= {minYear} ").ToListAsync();

        public async Task<List<string>> GetFilmGenresByIdAsync(int id)
        {
            return await _context.Database
                .SqlQuery<string>($@"select g.name from film as f
join filmgenre as fg on f.filmid = fg.filmid
join genre as g on fg.genreid = g.genreid
where f.filmid = {id}").ToListAsync();
        }

        public async Task<List<Film>> GetFilmStartWithRangeAsync(string range)
            => await _context.Films
                    .Where(f => EF.Functions.Like(f.Name, $"{range}%"))
                    .ToListAsync();


    }
}
