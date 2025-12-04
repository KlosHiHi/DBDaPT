using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CinemaClassLibrary.Contexts;
using CinemaClassLibrary.Models;

namespace LabWork17.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly CinemaClassLibrary.Contexts.CinemaDbContext _context;

        public IndexModel(CinemaClassLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ticket = await _context.Tickets
                .Include(t => t.Session)
                .Include(t => t.Visitor).ToListAsync();
        }
    }
}
