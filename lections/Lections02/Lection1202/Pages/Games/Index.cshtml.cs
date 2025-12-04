using Lection1202.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lection1202.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly Lection1202.Contexts.GamesDbContext _context;

        public IndexModel(Lection1202.Contexts.GamesDbContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string GameName { get; set; }

        public async Task OnGetAsync()
        {
            Game = await _context.Games
                .Include(g => g.Category).ToListAsync();
        }
    }
}
