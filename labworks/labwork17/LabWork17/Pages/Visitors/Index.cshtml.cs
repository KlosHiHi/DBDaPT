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
    public class IndexModel : PageModel
    {
        private readonly CinemaClassLibrary.Contexts.CinemaDbContext _context;

        public IndexModel(CinemaClassLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IList<Visitor> Visitor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Visitor = await _context.Visitors.ToListAsync();
        }
    }
}
