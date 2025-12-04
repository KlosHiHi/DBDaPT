using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthLibrary.Contexts;
using AuthLibrary.Models;

namespace LabWork17.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AuthLibrary.Contexts.CinemaUserDbContext _context;

        public LoginModel(AuthLibrary.Contexts.CinemaUserDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CinemaUser CinemaUser { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostLoginAsync()
        {  
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostGuest()
        {
            return RedirectToPage("/Films/Index");
        }
    }
}
