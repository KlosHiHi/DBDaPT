using CinemaClassLibrary.DTOs;
using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaClassLibrary.Services
{
    public class FilmService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<Film?> GetFilmByIdAsync(int id)
            => await _context.Films.FindAsync(id);

        public async Task<IEnumerable<FilmDto>> GetFilmsDtosAsync()
        {
            using (CinemaDbContext db = new CinemaDbContext())
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

        public async Task<IEnumerable<FilmDto>> GetFilmDtoByDate(DateTime date)
        {
            var filmsDtos = await GetFilmsDtosAsync();

            return filmsDtos.Where(f => f.StartDate == date);
        }
    }
}
