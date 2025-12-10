using CinemaClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabWork17.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly CinemaClassLibrary.Contexts.CinemaDbContext _context;

        public IndexModel(CinemaClassLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        private int _pageSize = 2;


        [BindProperty(SupportsGet = true)]
        public string FilmTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public byte Hall { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }

        public IList<Session> Session { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ViewData["Halls"] = new SelectList(_context.CinemaHalls.Distinct(), "HallId", "NumberName");

            var sessions = _context.Sessions
                .Include(s => s.Film)
                .Include(s => s.Hall)
                .AsQueryable();

            if (Hall > 0)
                sessions = sessions
                    .Where(s => s.Hall.HallId == Hall);

            if (!String.IsNullOrEmpty(FilmTitle))
                sessions = sessions
                    .Where(s => s.Film.Name.Contains(FilmTitle));

            sessions = SortColumn switch
            {
                "price" => sessions.OrderBy(s => s.Price),
                "price_desc" => sessions.OrderByDescending(s => s.Price),
                _ => sessions
            };

            TotalPages = sessions.Count() / _pageSize;

            sessions = sessions
                .Skip((PageIndex - 1) * _pageSize)
                .Take(_pageSize);


            Session = await sessions.ToListAsync();
        }
    }
}
