using FilmLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Frames
{
    public class DetailsModel : PageModel
    {
        private readonly FilmLibrary.Contexts.FilmDbContext _context;

        public DetailsModel(FilmLibrary.Contexts.FilmDbContext context)
        {
            _context = context;
        }

        public Frame Frame { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frame = await _context.Frames.FirstOrDefaultAsync(m => m.FrameId == id);

            if (frame is not null)
            {
                Frame = frame;

                return Page();
            }

            return NotFound();
        }
    }
}
