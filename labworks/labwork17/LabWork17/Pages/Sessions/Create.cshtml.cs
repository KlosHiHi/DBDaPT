using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaClassLibrary.Contexts;
using CinemaClassLibrary.Models;

namespace LabWork17.Pages.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly CinemaClassLibrary.Contexts.CinemaDbContext _context;

        public CreateModel(CinemaClassLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "Name");
        ViewData["HallId"] = new SelectList(_context.CinemaHalls, "HallId", "CinemaName");
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sessions.Add(Session);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
