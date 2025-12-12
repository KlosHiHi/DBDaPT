using FilmLibrary.Contexts;
using FilmLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.Frames
{
    public class CreateModel(FilmDbContext context) : PageModel
    {
        private readonly FilmLibrary.Contexts.FilmDbContext _context = context;
        private int _maxSize = 2_097_152;

        public IActionResult OnGet()
        {
            ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "Name");
            return Page();
        }

        [BindProperty]
        public Frame Frame { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string FileName { get; set; } = "template.jpeg";

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Frame.Film");

            var file = HttpContext.Request.Form.Files.FirstOrDefault();

            Frame.FileName = file.FileName;
            if (file?.Length < 0 || file?.Length > _maxSize)
                return Page();

            var path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "images", file.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);


            _context.Frames.Add(Frame);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
