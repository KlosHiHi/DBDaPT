using CinemaClassLibrary.Contexts;
using CinemaClassLibrary.Models;
using LabWork12.Sorts;
using Microsoft.EntityFrameworkCore;

namespace CinemaClassLibrary.Services
{
    public class SessionService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<List<Session>> GetAllOrderedAsync(Sort sort)
            => await _context.Sessions
                .FromSqlRaw($"select * from session order by {sort.ColumnName} {(sort.isDescending ? "desc" : "")}")
                .ToListAsync();

        public async Task<int> IncreasePriceAsync(int quantity, int hallId)
            => await _context.Database
                .ExecuteSqlAsync($"update session set price += {quantity} where hallId = {hallId}");

        public async Task<decimal> GetMinFilmPriceAsync(int filmId)
            => (decimal)await _context.Sessions
                .Where(s => s.FilmId == filmId)
                .MinAsync(s => s.Price);

        public async Task<decimal> GetMaxFilmPriceAsync(int filmId)
            => (decimal)await _context.Sessions
                .Where(s => s.FilmId == filmId)
                .MaxAsync(s => s.Price);

        public async Task<decimal> GetAverageFilmPriceAsync(int filmId)
            => (decimal)await _context.Sessions
                .Where(s => s.FilmId == filmId)
                .AverageAsync(s => s.Price);

        public async Task<List<Session>> GetSessionsByFilmIdAsync(int id)
            => await _context.Sessions
                .FromSql($"select * from dbo.GetSessionByFilmId({id})")
                .ToListAsync();
    }
}
