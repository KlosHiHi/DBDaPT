using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmLibrary.Contexts;
using FilmLibrary.Models;

namespace WebApp.Pages.Films
{
    public class IndexModel : PageModel
    {
        private readonly FilmLibrary.Contexts.FilmDbContext _context;

        public IndexModel(FilmLibrary.Contexts.FilmDbContext context)
        {
            _context = context;
        }

        public IList<Film> Film { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Film = await _context.Films.ToListAsync();
        }
    }
}
