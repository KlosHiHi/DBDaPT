using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CinemaClassLibrary.Contexts;
using CinemaClassLibrary.Models;

namespace LabWork17.Pages.Visitors
{
    public class DetailsModel : PageModel
    {
        private readonly CinemaClassLibrary.Contexts.CinemaDbContext _context;

        public DetailsModel(CinemaClassLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public Visitor Visitor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors.FirstOrDefaultAsync(m => m.VisitorId == id);

            if (visitor is not null)
            {
                Visitor = visitor;

                return Page();
            }

            return NotFound();
        }
    }
}
