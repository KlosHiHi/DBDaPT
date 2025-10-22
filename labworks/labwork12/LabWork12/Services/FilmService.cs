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
            => await _context.Films
                .FromSqlRaw($"select * from film order by {sort.ColumnName} {(sort.isDescending ? "desc" : "")}")
                .ToListAsync();

        public async Task<List<Film>> GetByNameAndReleaseYearAsync(string name, int minYear)
            => await _context.Films
                .FromSql($"select * from film where name = {name} and releaseYear >= {minYear}")
                .ToListAsync();

        public async Task<List<string>> GetFilmGenresByIdAsync(int id)
        {
            return await _context.Database
                .SqlQuery<string>($@"select g.name 
from genre as g
join filmGenre as fg on g.genreId = fg.genreId
where fg.filmId = {id}")
                .ToListAsync();
        }

        public async Task<List<Film>> GetFilmStartWithRangeAsync(char beginChar, char endChar)
            => await _context.Films
                    .Where(f => EF.Functions.Like(f.Name, $"[{beginChar}-{endChar}]%"))
                    .ToListAsync();
    }
}
