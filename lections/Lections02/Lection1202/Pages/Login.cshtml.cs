using Lection1202.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lection1202.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Lection1202.Contexts.GamesDbContext _context;

        public LoginModel(Lection1202.Contexts.GamesDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users
                //.Include(u => u.Role)
                .FirstOrDefault(u => u.Login == User.Login);

            if (user is null || user.Password != User.Password) 
                return Page();

            HttpContext.Session.SetString("Role", user.Role);
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostGuestAsync()
        {
            HttpContext.Session.SetString("Role", "Гость");
            return RedirectToPage("/Games/Index");
        }
    }
}
