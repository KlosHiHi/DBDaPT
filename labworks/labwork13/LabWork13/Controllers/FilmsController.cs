using CinemaClassLibrary.Contexts;
using CinemaClassLibrary.DTOs;
using CinemaClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabWork13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController(CinemaDbContext context) : ControllerBase
    {
        private readonly CinemaDbContext _context = context;

        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
            => await _context.Films.ToListAsync();

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);

            return film is null ? NotFound() : film;
        }

        // ../films/pages?page={page}&sortBy={sortBy}
        [HttpGet("pages")]
        public async Task<ActionResult<IEnumerable<Film>>> GetPaginatedAndSortedFilms(
            [FromQuery] int? page = null,
            [FromQuery] string? sortBy = null)
        {
            var films = _context.Films.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                films = sortBy.ToLower() switch
                {
                    "name" => films.OrderBy(f => f.Name),
                    "releaseyear" => films.OrderBy(f => f.ReleaseYear),
                    "releaseyear_desc" => films.OrderByDescending(f => f.ReleaseYear),
                    _ => films
                };
            }

            if (page.HasValue)
            {
                var pageSize = 3;
                films = films
                    .Skip(pageSize * ((int)page - 1))
                    .Take(pageSize);
            }

            return await films.ToListAsync();
        }

        // ../films/filter?year={year}&title={title}
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilmsByYearAndTitle(
            [FromQuery] string? year = null,
            [FromQuery] string? title = null)
        {
            var films = _context.Films.AsQueryable();

            if (Int16.TryParse(year, out short filmYear))
                films = films.Where(f => f.ReleaseYear == filmYear);
            else
                BadRequest();

            if (!string.IsNullOrWhiteSpace(title))
                films = films.Where(f => f.Name.Contains(title));
            else
                BadRequest();

            return await films.ToListAsync();
        }

        // ../films/{id}/genres 
        [HttpGet("{id}/genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetFilmGenresById([FromRoute] int id)
        {
            var film = await _context.Films
                .Include(f => f.Genres)
                .FirstOrDefaultAsync(f => f.FilmId == id);

            return film is null ? NotFound() : film.Genres.ToList();
        }

        // ../films/{id}/sessions 
        [HttpGet("{id}/sessions")]
        public async Task<ActionResult<IEnumerable<Session>>> GetFilmSessionsById([FromRoute] int id)
        {
            var film = await _context.Films
                .Include(f => f.Sessions)
                .FirstOrDefaultAsync(f => f.FilmId == id);

            return film is null ? NotFound() : film.Sessions.Where(s => s.StartDate >= DateTime.Now).ToList();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilmByYearAndGenres(
            [FromQuery] string? year = null,
            [FromQuery] string? genres = null)
        {
            var years = year?.Split('-');

            var films = _context.Films
                .Include(f => f.Genres)
                .AsQueryable();

            if (years?.Length != 2)
                return BadRequest();

            int minYear, maxYear;
            if (Int32.TryParse(years[0], out minYear) &&
                Int32.TryParse(years[1], out maxYear))
                films = films.Where(f => f.ReleaseYear >= minYear && f.ReleaseYear <= maxYear);
            else
                return BadRequest();

            if (string.IsNullOrWhiteSpace(genres))
                BadRequest();
            else
            {
                var genreValues = genres?.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                films = films
                    .Where(f => f.Genres.Any(g => genreValues!.Contains(g.Name)));
            }

            return await films.ToListAsync();
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilmInfo()
        {
            return await _context.Films
                .Select(f => new FilmDto()
                {
                    Id = f.FilmId,
                    Title = f.Name,
                    TiketsCount = f.Sessions.Join(_context.Tickets,
                        s => s.SessionId,
                        t => t.SessionId,
                        (s, t) => new TicketDto { Price = s.Price }).Count(),
                    SalesProfit = f.Sessions.Join(_context.Tickets,
                        s => s.SessionId,
                        t => t.SessionId,
                        (s, t) => new { s.Price }).Sum(s => s.Price),
                })
                .ToListAsync();
        }

        [HttpGet("statistics/{id}")]
        public async Task<ActionResult<FilmDto>> GetFilmInfoById(int id)
        {
            return await _context.Films
                .Select(f => new FilmDto()
                {
                    Id = f.FilmId,
                    Title = f.Name,
                    TiketsCount = f.Sessions.Join(_context.Tickets,
                        s => s.SessionId,
                        t => t.SessionId,
                        (s, t) => new { }).Count(),
                    SalesProfit = f.Sessions.Join(_context.Tickets,
                        s => s.SessionId,
                        t => t.SessionId,
                        (s, t) => new { s.Price }).Sum(s => s.Price),
                }).FirstOrDefaultAsync(f => f.Id == id) ?? null!;
        }

        // PUT: api/Films/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.FilmId)
                return BadRequest();

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FilmExistsAsync(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Films
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFilm), new { id = film.FilmId }, film);
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film is null)
                return NotFound();

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> FilmExistsAsync(int id)
        {
            return await _context.Films.AnyAsync(e => e.FilmId == id);
        }

    }
}
