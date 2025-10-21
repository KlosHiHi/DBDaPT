using LabWork12.Contexts;
using LabWork12.Models;
using LabWork12.Sorts;
using Microsoft.EntityFrameworkCore;

namespace LabWork12.Services
{
    public class SessionService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<List<Session>> GetAllOrderedAsync(Sort sort)
            => sort.isDescending
                ? await _context.Sessions.FromSqlRaw($"select * from session order by {sort.ColumnName} desc").ToListAsync()
                : await _context.Sessions.FromSqlRaw($"select * from session order by {sort.ColumnName}").ToListAsync();

        public async Task<int> IncreasePriceAsync(int quantity, int hallId)
            => await _context.Database.ExecuteSqlAsync($"update session set price += {quantity} where hallid = {hallId}");

        public async Task<decimal> GetMinFilmPriceAsync(int filmId)
            => await _context.Database
                    .SqlQuery<decimal>($"select min(price) as value from session where filmid = {filmId}")
                    .FirstOrDefaultAsync();

        public async Task<decimal> GetMaxFilmPriceAsync(int filmId)
            => await _context.Database
                    .SqlQuery<decimal>($"select max(price) as value from session where filmid = {filmId}")
                    .FirstOrDefaultAsync();

        public async Task<decimal> GetAverageFilmPriceAsync(int filmId)
            => await _context.Database
                    .SqlQuery<decimal>($"select avg(price) as value from session where filmid = {filmId}")
                    .FirstOrDefaultAsync();
    }
}
