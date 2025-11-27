using CinemaClassLibrary.DTOs;
using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaClassLibrary.Services
{
    public class FilmService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;
        private string _baseItem = "---";

        public async Task<Film?> GetFilmByIdAsync(int id)
            => await _context.Films.FindAsync(id);

        public async Task<IEnumerable<FilmDto>> GetFilmsDtosAsync()
        {
            using (CinemaDbContext db = new())
            {
                var filmsDtos = from f in db.Films
                                join s in db.Sessions on f.FilmId equals s.FilmId
                                join c in db.CinemaHalls on s.HallId equals c.HallId
                                select new FilmDto
                                {
                                    Name = f.Name,
                                    StartDate = s.StartDate,
                                    CinemaName = c.CinemaName,
                                    HallNumber = c.HallNumber,
                                    Price = s.Price
                                };

                filmsDtos = filmsDtos
                    .OrderBy(f => f.Name)
                    .ThenBy(f => f.StartDate);
                return await filmsDtos.ToListAsync();
            }
        }

        public async Task<IEnumerable<string>> GetCinemaNamesAsync()
        {
            var names = await _context.CinemaHalls
                        .GroupBy(h => h.CinemaName)
                        .Select(h => h.Key)
                        .ToListAsync();
            names.Add(_baseItem);

            return names;
        }

        public async Task<IEnumerable<FilmDto>> GetFilteredFilmDto(DateTime date, string cinemaName)
        {
            var filmsDtos = await GetFilmsDtosAsync();

            if(cinemaName != _baseItem)
                filmsDtos = filmsDtos.Where(f => f.CinemaName == cinemaName);

            filmsDtos = filmsDtos.Where(f => f.StartDate?.Date == date.Date );

            return filmsDtos.ToList();
        }
    }
}
