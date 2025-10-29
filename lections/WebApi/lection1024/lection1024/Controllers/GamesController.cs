using Lection1024.Contexts;
using Lection1024.DTOs;
using Lection1024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lection1024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(GamesDbContext context) : ControllerBase
    {
        private readonly GamesDbContext _context = context;

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            return game is null ? NotFound() : game;
        }
        #region specific get

        // GET: api/Games/?category=cat1,cat2,...
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByCategories([FromQuery] string? categories)
        {
            var values = categories.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            return await _context.Games
                .Include(g => g.Category) // опционально
                .Where(g => values.Contains(g.Category.Name))
                .ToListAsync();
        }

        // GET: api/Games/?category=category М:М (games-categories)
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByCategory([FromQuery] string name)
        {
            var category = await _context.Categories
                .Include(c => c.Games)
                .FirstOrDefaultAsync(c => c.Name == name);

            return category is null ? NotFound() : category.Games.ToList();
        }

        // GET: api/Games/?price=priceMin-priceMax
        [HttpGet("price")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByPrice([FromQuery] string price)
        {
            var values = price.Split('-');

            if (values.Length != 2)
                return BadRequest();

            int minPrice, maxPrice;
            if (!int.TryParse(values[0], out minPrice) ||
                !int.TryParse(values[1], out maxPrice))
                return BadRequest();

            return await _context.Games
                .Where(g => g.Price >= minPrice && g.Price <= maxPrice)
                .ToListAsync();
        }

        //filters?filters=price:_,title:_
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByFilters([FromQuery] string filters)
        {
            var values = filters.Split(',');
            Dictionary<string, string> pairs = new();
            foreach (var value in values)
            {
                var pair = value.Split(':');
                pairs[pair[0]] = pair[1];
            }
            //...

            return await _context.Games
                .ToListAsync();
        }

        /*
        //columns?col1,col2,..
        [HttpGet("columns")]
        public async Task<ActionResult> GetCOlumns([FromQuery] string columns)
        {
            return await _context.Database
                .
                .ToListAsync();
        }
        */

        // GET: api/Games/info
        [HttpGet("info")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesInfo()
        {
            return await _context.Games
                .Select(g => g.ToDto())
                .ToListAsync();
        }

        #endregion
        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.GameId)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.GameId }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}
