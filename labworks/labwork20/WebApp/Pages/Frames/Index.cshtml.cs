using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmLibrary.Contexts;
using FilmLibrary.Models;

namespace WebApp.Pages.Frames
{
    public class IndexModel : PageModel
    {
        private readonly FilmLibrary.Contexts.FilmDbContext _context;

        public IndexModel(FilmLibrary.Contexts.FilmDbContext context)
        {
            _context = context;
        }

        public IList<Frame> Frame { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Frame = await _context.Frames
                .Include(f => f.Film).ToListAsync();
        }
    }
}
